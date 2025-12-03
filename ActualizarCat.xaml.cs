using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Interfaz
{
    /// <summary>
    /// Lógica de interacción para ActualizarCat.xaml
    /// </summary>
    public partial class ActualizarCat : Window
    {
        private Conexion conexion;
        private int? idProductoActual = null; // null si es nuevo, número si es actualización

        public ActualizarCat()
        {
            InitializeComponent();
            conexion = new Conexion();

            // Seleccionar primera opción por defecto
            cmbCategoria.SelectedIndex = 0;
            cmbEstado.SelectedIndex = 0;
        }

        // Buscar producto existente
        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string criterio = txtBuscar.Text?.Trim() ?? string.Empty;

            if (string.IsNullOrEmpty(criterio))
            {
                MessageBox.Show("Por favor, ingrese un ID o nombre de producto para buscar.",
                    "Campo vacío",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (var conn = conexion.GetConnection())
                {
                    string query = @"
                        SELECT 
                            p.id_producto,
                            p.nombre,
                            p.descripcion,
                            p.precio,
                            p.id_categoria,
                            p.estado
                        FROM productos p
                        WHERE p.id_producto = @idProd OR p.nombre ILIKE @patron
                        LIMIT 1";

                    bool esNumero = int.TryParse(criterio, out int idProd);
                    string patron = $"%{criterio}%";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idProd", esNumero ? idProd : 0);
                        cmd.Parameters.AddWithValue("@patron", patron);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Producto encontrado - cargar datos en el formulario
                                idProductoActual = reader.GetInt32(0);
                                txtId.Text = idProductoActual.ToString();
                                txtNombre.Text = reader.GetString(1);
                                txtDescripcion.Text = reader.IsDBNull(2) ? "" : reader.GetString(2);
                                txtPrecio.Text = reader.GetDecimal(3).ToString("F2");

                                int idCategoria = reader.GetInt32(4);
                                cmbCategoria.SelectedIndex = idCategoria - 1;

                                string estado = reader.GetString(5);
                                cmbEstado.SelectedIndex = estado == "Activo" ? 0 : 1;

                                MessageBox.Show($"Producto encontrado: {reader.GetString(1)}\n\nAhora puede modificar los datos.",
                                    "Producto Encontrado",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                                
                                // Limpiar barra de búsqueda después de encontrar
                                txtBuscar.Clear();
                            }
                            else
                            {
                                MessageBox.Show($"No se encontró ningún producto con: '{criterio}'",
                                    "Sin resultados",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar producto:\n{ex.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        // Guardar producto (crear o actualizar)
        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            // Validaciones
            if (!ValidarCampos())
                return;

            // Obtener valores del formulario
            string nombre = txtNombre.Text.Trim();
            string descripcion = string.IsNullOrWhiteSpace(txtDescripcion.Text) ? null : txtDescripcion.Text.Trim();
            decimal precio = decimal.Parse(txtPrecio.Text.Trim());
            int idCategoria = int.Parse(((ComboBoxItem)cmbCategoria.SelectedItem).Tag.ToString());
            string estado = ((ComboBoxItem)cmbEstado.SelectedItem).Content.ToString();

            try
            {
                using (var conn = conexion.GetConnection())
                {
                    string query;
                    string mensaje;

                    if (idProductoActual.HasValue)
                    {
                        // ACTUALIZAR producto existente
                        query = @"
                            UPDATE productos 
                            SET nombre = @nombre,
                                descripcion = @descripcion,
                                precio = @precio,
                                id_categoria = @idCategoria,
                                estado = @estado,
                                fecha_modificacion = CURRENT_TIMESTAMP
                            WHERE id_producto = @idProducto";

                        mensaje = "Producto actualizado correctamente";
                    }
                    else
                    {
                        // CREAR nuevo producto
                        query = @"
                            INSERT INTO productos (nombre, descripcion, precio, id_categoria, estado)
                            VALUES (@nombre, @descripcion, @precio, @idCategoria, @estado)";

                        mensaje = "Producto creado correctamente";
                    }
                    

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@descripcion", (object)descripcion ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@precio", precio);
                        cmd.Parameters.AddWithValue("@idCategoria", idCategoria);
                        cmd.Parameters.AddWithValue("@estado", estado);

                        if (idProductoActual.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@idProducto", idProductoActual.Value);
                        }

                        int filasAfectadas = cmd.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            MessageBox.Show(mensaje,
                                "Éxito",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);

                            // Refrescar catálogo si la ventana está abierta
                            foreach (Window window in Application.Current.Windows)
                            {
                                if (window is Catalogo catalogoWindow)
                                {
                                    catalogoWindow.CargarCatalogo();
                                    break;
                                }
                            }

                            // Preguntar si desea agregar otro producto
                            var resultado = MessageBox.Show("¿Desea agregar o modificar otro producto?",
                                "Continuar",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Question);

                            if (resultado == MessageBoxResult.Yes)
                            {
                                LimpiarFormulario();
                            }
                            else
                            {
                                this.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el producto:\n{ex.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        // Validar campos del formulario
        private bool ValidarCampos()
        {
            // Validar nombre
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre del producto es obligatorio.",
                    "Campo requerido",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                txtNombre.Focus();
                return false;
            }

            // Validar precio
            if (string.IsNullOrWhiteSpace(txtPrecio.Text))
            {
                MessageBox.Show("El precio es obligatorio.",
                    "Campo requerido",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                txtPrecio.Focus();
                return false;
            }

            if (!decimal.TryParse(txtPrecio.Text.Trim(), out decimal precio) || precio < 0)
            {
                MessageBox.Show("El precio debe ser un número válido mayor o igual a 0.\n\nEjemplo: 10.50",
                    "Precio inválido",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                txtPrecio.Focus();
                return false;
            }

            // Validar categoría
            if (cmbCategoria.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar una categoría.",
                    "Campo requerido",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                cmbCategoria.Focus();
                return false;
            }

            // Validar estado
            if (cmbEstado.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un estado.",
                    "Campo requerido",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                cmbEstado.Focus();
                return false;
            }

            return true;
        }

        // Limpiar formulario
        private void LimpiarFormulario()
        {
            idProductoActual = null;
            txtBuscar.Clear();
            txtId.Clear();
            txtNombre.Clear();
            txtDescripcion.Clear();
            txtPrecio.Clear();
            cmbCategoria.SelectedIndex = 0;
            cmbEstado.SelectedIndex = 0;
            txtBuscar.Focus();
        }

        // Cancelar con confirmación
        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            var resultado = MessageBox.Show(
                "¿Está seguro que desea cancelar?\n\nLos cambios no guardados se perderán.",
                "Confirmar cancelación",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (resultado == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
    }
}
