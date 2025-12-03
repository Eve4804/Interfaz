using System;
using System.Windows;
using System.Windows.Controls;
using Npgsql;

namespace Interfaz
{
    public partial class ActualizarInv : Window
    {
        public ActualizarInv()
        {
            InitializeComponent();
            txtId.LostFocus += TxtId_LostFocus;
        }

        // Método para calcular el estado según la cantidad
        private string CalcularEstadoInventario(int cantidad)
        {
            if (cantidad > 200)
                return "Suficiente";
            else if (cantidad >= 100 && cantidad <= 200)
                return "En pedido";
            else if (cantidad >= 50 && cantidad < 100)
                return "Agotado";
            else // cantidad < 50
                return "Descontinuado";
        }

        // Evento para actualizar el estado en tiempo real cuando cambia la cantidad
        private void txtCantidad_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(txtCantidad.Text.Trim(), out int cantidad) && cantidad >= 0)
            {
                string estadoCalculado = CalcularEstadoInventario(cantidad);
                txtEstado.Text = estadoCalculado;
                
                // Cambiar color según el estado
                switch (estadoCalculado)
                {
                    case "Suficiente":
                        txtEstado.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(232, 245, 233)); // Verde claro
                        txtEstado.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(46, 125, 50)); // Verde oscuro
                        break;
                    case "En pedido":
                        txtEstado.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 243, 224)); // Naranja claro
                        txtEstado.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(230, 81, 0)); // Naranja oscuro
                        break;
                    case "Agotado":
                        txtEstado.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 235, 238)); // Rojo claro
                        txtEstado.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(198, 40, 40)); // Rojo oscuro
                        break;
                    case "Descontinuado":
                        txtEstado.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(224, 224, 224)); // Gris claro
                        txtEstado.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(97, 97, 97)); // Gris oscuro
                        break;
                }
            }
            else
            {
                txtEstado.Text = "";
            }
        }

        private void Cancelar_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TxtId_LostFocus(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtId.Text.Trim(), out int idProd))
            {
                try
                {
                    Conexion conexion = new Conexion();
                    using (var conn = conexion.GetConnection())
                    {
                        if (conn.State != System.Data.ConnectionState.Open)
                            conn.Open();

                        string query = @"
                            SELECT p.nombre, p.precio, c.nombre AS categoria
                            FROM productos p
                            JOIN categorias c ON p.id_categoria = c.id_categoria
                            WHERE p.id_producto = @idProd";

                        using (var cmd = new NpgsqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@idProd", idProd);
                            using (var reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    txtNombre.Text = reader["nombre"].ToString();
                                    txtPrecio.Text = reader["precio"].ToString();

                                    string categoria = reader["categoria"].ToString();
                                    foreach (ComboBoxItem item in cmbCategoria.Items)
                                    {
                                        if (item.Content.ToString() == categoria)
                                        {
                                            cmbCategoria.SelectedItem = item;
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    txtNombre.Text = "";
                                    txtPrecio.Text = "";
                                    cmbCategoria.SelectedIndex = -1;

                                    MessageBox.Show("No se encontró un producto con ese ID.",
                                                    "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al buscar producto: {ex.Message}",
                                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtId.Text) ||
                    string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    cmbCategoria.SelectedItem == null ||
                    string.IsNullOrWhiteSpace(txtPrecio.Text) ||
                    string.IsNullOrWhiteSpace(txtCantidad.Text))
                {
                    MessageBox.Show("Por favor completa todos los campos.",
                                    "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!double.TryParse(txtPrecio.Text, out double precio))
                {
                    MessageBox.Show("El precio debe ser un número válido.",
                                    "Error de formato", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!int.TryParse(txtCantidad.Text, out int cantidad) || cantidad < 0)
                {
                    MessageBox.Show("La cantidad debe ser un número entero positivo.",
                                    "Error de formato", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                int idProd = int.Parse(txtId.Text.Trim());
                
                // Calcular estado automáticamente según la cantidad
                string estado = CalcularEstadoInventario(cantidad);

                Conexion conexion = new Conexion();
                using (var conn = conexion.GetConnection())
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    string checkQuery = "SELECT id_inventario FROM inventarios WHERE id_producto = @idProd";
                    using (var checkCmd = new NpgsqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@idProd", idProd);
                        var result = checkCmd.ExecuteScalar();

                        if (result != null)
                        {
                            string updateQuery = @"
                                UPDATE inventarios 
                                SET cantidad = @cantidad,
                                    estado = @estado,
                                    fecha_actualizacion = CURRENT_TIMESTAMP
                                WHERE id_producto = @idProd";

                            using (var updateCmd = new NpgsqlCommand(updateQuery, conn))
                            {
                                updateCmd.Parameters.AddWithValue("@cantidad", cantidad);
                                updateCmd.Parameters.AddWithValue("@estado", estado);
                                updateCmd.Parameters.AddWithValue("@idProd", idProd);
                                updateCmd.ExecuteNonQuery();
                            }

                            MessageBox.Show($"Inventario actualizado correctamente.\n\n" +
                                            $"Cantidad: {cantidad}\n" +
                                            $"Estado asignado: {estado}",
                                            "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            string insertQuery = @"
                                INSERT INTO inventarios (id_producto, cantidad, estado) 
                                VALUES (@idProd, @cantidad, @estado)";

                            using (var insertCmd = new NpgsqlCommand(insertQuery, conn))
                            {
                                insertCmd.Parameters.AddWithValue("@idProd", idProd);
                                insertCmd.Parameters.AddWithValue("@cantidad", cantidad);
                                insertCmd.Parameters.AddWithValue("@estado", estado);
                                insertCmd.ExecuteNonQuery();
                            }

                            MessageBox.Show($"Producto agregado al inventario.\n\n" +
                                            $"Cantidad: {cantidad}\n" +
                                            $"Estado asignado: {estado}",
                                            "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        // Refrescar inventario si la ventana está abierta
                        foreach (Window window in Application.Current.Windows)
                        {
                            if (window is Inventarios inventarioWindow)
                            {
                                  inventarioWindow.CargarInventario();
                                break;
                            }
                        }
                    }
                }

                txtId.Clear();
                txtNombre.Clear();
                txtPrecio.Clear();
                txtCantidad.Clear();
                txtEstado.Clear();
                cmbCategoria.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al guardar: {ex.Message}",
                                "Error inesperado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}