using System;
using System.Windows;
using System.Windows.Controls;
using static Interfaz.Notificaciones;

namespace Interfaz
{
    /// <summary>
    /// Lógica de interacción para ContactarProv.xaml
    /// </summary>
    public partial class ContactarProv : Window
    {
        public ContactarProv()
        {
            InitializeComponent();
        }

        private void EnvSolicitud_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validar campos obligatorios
                if (cmbProveedor.SelectedItem == null ||
                    string.IsNullOrWhiteSpace(txtId.Text) ||
                    string.IsNullOrWhiteSpace(txtProducto.Text) ||
                    string.IsNullOrWhiteSpace(txtDescripcion.Text) ||
                    string.IsNullOrWhiteSpace(txtCantidad.Text) ||
                    dpFecha.SelectedDate == null)
                {
                    MessageBox.Show("Por favor completa todos los campos antes de enviar la solicitud.",
                                    "Campos incompletos", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Obtener valores
                string tipoProveedor = ((ComboBoxItem)cmbProveedor.SelectedItem).Content.ToString();
                string idSolicitud = txtId.Text.Trim();
                string idProducto = txtProducto.Text.Trim();
                string descripcion = txtDescripcion.Text.Trim();
                string cantidad = txtCantidad.Text.Trim();
                DateTime fechaSolicitud = dpFecha.SelectedDate.Value;

                // Validar formato de cantidad (opcional)
                if (!cantidad.ToLower().Contains("unidad") && !int.TryParse(cantidad, out _))
                {
                    MessageBox.Show("La cantidad debe ser un número o incluir 'unidades'.",
                                    "Formato inválido", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Simular envío
                MessageBox.Show(
                    $"Solicitud enviada correctamente:\n\n" +
                    $"Tipo de proveedor: {tipoProveedor}\n" +
                    $"ID Solicitud: {idSolicitud}\n" +
                    $"ID Producto: {idProducto}\n" +
                    $"Descripción: {descripcion}\n" +
                    $"Cantidad: {cantidad}\n" +
                    $"Fecha: {fechaSolicitud:dd/MM/yyyy}",
                    "Solicitud enviada", MessageBoxButton.OK, MessageBoxImage.Information
                );

                // Crear mensaje de notificación
                string mensajeNotificacion = $"Solicitud {idSolicitud} enviada al proveedor {tipoProveedor} el {fechaSolicitud:dd/MM/yyyy}";
                NotificacionesService.AgregarSolicitud(mensajeNotificacion);

                // Limpiar campos
                cmbProveedor.SelectedIndex = -1;
                txtId.Clear();
                txtProducto.Clear();
                txtDescripcion.Clear();
                txtCantidad.Clear();
                dpFecha.SelectedDate = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al enviar la solicitud:\n{ex.Message}",
                        "Error inesperado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void IrApp_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Te estamos dirigiendo a la app de proveedor",
                            "Redirección",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void IrMenu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Notificaciones_Click(object sender, RoutedEventArgs e)
        {
            Notificaciones notificaciones = new Notificaciones();
            notificaciones.Show();
        }
    }
}
