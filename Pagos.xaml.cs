using Npgsql;
using System;
using System.Linq;
using System.Windows;

namespace Interfaz
{
    public partial class Pagos : Window
    {
        public Pagos()
        {
            InitializeComponent();
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validar campos obligatorios
                if (string.IsNullOrWhiteSpace(txtProveedor.Text) ||
                    string.IsNullOrWhiteSpace(txtSolicitud.Text) ||
                    string.IsNullOrWhiteSpace(txtMonto.Text) ||
                    string.IsNullOrWhiteSpace(txtNumeroCuenta.Text) ||
                    string.IsNullOrWhiteSpace(txtClabe.Text) ||
                    dtpFechaPago.SelectedDate == null)
                {
                    MessageBox.Show("Por favor completa todos los campos antes de guardar el pago.",
                                    "Campos incompletos", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Validar monto
                if (!decimal.TryParse(txtMonto.Text.Trim(), out decimal monto) || monto <= 0)
                {
                    MessageBox.Show("El monto debe ser un número positivo.",
                                    "Monto inválido", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Validar número de cuenta
                string cuenta = txtNumeroCuenta.Text.Trim();
                if (cuenta.Length < 10 || cuenta.Length > 20 || !cuenta.All(char.IsDigit))
                {
                    MessageBox.Show("El número de cuenta debe tener entre 10 y 20 dígitos numéricos.",
                                    "Cuenta inválida", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Validar CLABE
                string clabe = txtClabe.Text.Trim();
                if (clabe.Length != 18 || !clabe.All(char.IsDigit))
                {
                    MessageBox.Show("La CLABE interbancaria debe tener exactamente 18 dígitos numéricos.",
                                    "CLABE inválida", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Obtener valores
                int idProveedor = txtProveedor.Tag != null ? (int)txtProveedor.Tag : 0;
                int idSolicitud = int.Parse(txtSolicitud.Text.Trim());
                
                if (idProveedor == 0)
                {
                    MessageBox.Show("Por favor ingresa un ID de solicitud válido primero.",
                                    "Proveedor no cargado", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                DateTime fechaPago = dtpFechaPago.SelectedDate.Value;

                // Forma de pago fija
                string formaPago = "Transferencia";

                // Insertar en la base de datos
                Conexion conexion = new Conexion();
                using (var conn = conexion.GetConnection())
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    string insertQuery = @"
                        INSERT INTO pagos 
                        (id_proveedor, id_solicitud, monto_total, numero_cuenta, clabe_interbancaria, forma_pago, fecha_pago) 
                        VALUES 
                        (@idProveedor, @idSolicitud, @monto, @cuenta, @clabe, @formaPago, @fechaPago)";

                    using (var cmd = new NpgsqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@idProveedor", idProveedor);
                        cmd.Parameters.AddWithValue("@idSolicitud", idSolicitud);
                        cmd.Parameters.AddWithValue("@monto", monto);
                        cmd.Parameters.AddWithValue("@cuenta", cuenta);
                        cmd.Parameters.AddWithValue("@clabe", clabe);
                        cmd.Parameters.AddWithValue("@formaPago", formaPago);
                        cmd.Parameters.AddWithValue("@fechaPago", fechaPago);

                        cmd.ExecuteNonQuery();
                    }
                }

                // Confirmación
                MessageBox.Show($"Pago registrado correctamente:\n\n" +
                                $"Proveedor ID: {idProveedor}\n" +
                                $"Solicitud ID: {idSolicitud}\n" +
                                $"Monto: ${monto:N2}\n" +
                                $"Forma de pago: {formaPago}\n" +
                                $"Fecha: {fechaPago:dd/MM/yyyy}",
                                "Pago registrado", MessageBoxButton.OK, MessageBoxImage.Information);

                // Notificación de pago a proveedor
                string titulo = $"Pago registrado - Proveedor {idProveedor}";
                string mensaje = $"Monto: ${monto:N2} | Solicitud: {idSolicitud} | Fecha: {fechaPago:dd/MM/yyyy} | Forma: {formaPago}";
                NotificacionesService.AgregarSolicitud(titulo, mensaje, idSolicitud);

                // Limpiar campos
                txtProveedor.Clear();
                txtSolicitud.Clear();
                txtMonto.Clear();
                txtNumeroCuenta.Clear();
                txtClabe.Clear();
                dtpFechaPago.SelectedDate = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al registrar el pago:\n{ex.Message}",
                                "Error inesperado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void txtSolicitud_LostFocus(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtSolicitud.Text.Trim(), out int idSolicitud))
            {
                try
                {
                    Conexion conexion = new Conexion();
                    using (var conn = conexion.GetConnection())
                    {
                        if (conn.State != System.Data.ConnectionState.Open)
                            conn.Open();

                        // Obtener datos de la solicitud incluyendo nombre del proveedor
                        string query = @"
                    SELECT s.id_proveedor, s.cantidad, s.fecha_solicitud, 
                           p.precio, p.nombre AS producto,
                           prov.nombre AS nombre_proveedor
                    FROM solicitudes_proveedor s
                    JOIN productos p ON s.id_producto = p.id_producto
                    JOIN proveedores prov ON s.id_proveedor = prov.id_proveedor
                    WHERE s.id_solicitud = @idSolicitud;";

                        using (var cmd = new NpgsqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@idSolicitud", idSolicitud);
                            using (var reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    // Mostrar nombre del proveedor en lugar del ID
                                    string nombreProveedor = reader["nombre_proveedor"].ToString();
                                    txtProveedor.Text = nombreProveedor;
                                    txtProveedor.IsReadOnly = true; // Hacer readonly para que no se edite
                                    txtProveedor.Tag = reader["id_proveedor"]; // Guardar el ID en Tag para usarlo después

                                    int cantidad = Convert.ToInt32(reader["cantidad"]);
                                    decimal precio = Convert.ToDecimal(reader["precio"]);
                                    decimal monto = cantidad * precio;

                                    txtMonto.Text = monto.ToString("N2");
                                    txtMonto.IsReadOnly = true; // Hacer readonly ya que se calcula automáticamente

                                    dtpFechaPago.SelectedDate = DateTime.Now;
                                }
                                else
                                {
                                    MessageBox.Show("No se encontró la solicitud con ese ID.",
                                                    "Solicitud no encontrada", MessageBoxButton.OK, MessageBoxImage.Warning);
                                    txtProveedor.Clear();
                                    txtMonto.Clear();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al buscar solicitud:\n{ex.Message}",
                                    "Error inesperado", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnHistorialPagos_Click(object sender, RoutedEventArgs e)
        {
            HistorialPagos histps = new HistorialPagos();
            histps.ShowDialog();
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}