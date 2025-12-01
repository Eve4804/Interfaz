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
    /// Lógica de interacción para CalcularFecha.xaml
    /// </summary>
    public partial class CalcularFecha : Window
    {
        public CalcularFecha()
        {
            InitializeComponent();
        }

        // Evento para el botón Notificar a proveedor
        private void BtnNotificarProveedor_Click(object sender, RoutedEventArgs e)
        {
            // Validar que la disponibilidad esté seleccionada como "Contactar proveedor"
            if (CmbDisponibilidad.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar el estado de disponibilidad del producto.",
                    "Campo requerido",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                CmbDisponibilidad.Focus();
                return;
            }

            ComboBoxItem itemSeleccionado = (ComboBoxItem)CmbDisponibilidad.SelectedItem;
            string disponibilidad = itemSeleccionado.Content.ToString();

            if (disponibilidad != "Contactar proveedor")
            {
                MessageBox.Show("Esta opción solo está disponible cuando se necesita contactar al proveedor.\n\n" +
                              "Disponibilidad actual: " + disponibilidad,
                    "Acción no disponible",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }

            // Validar campos obligatorios
            if (string.IsNullOrWhiteSpace(TxtProducto.Text))
            {
                MessageBox.Show("Debe ingresar el producto o servicio para notificar al proveedor.",
                    "Campo requerido",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                TxtProducto.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(TxtCantidad.Text))
            {
                MessageBox.Show("Debe ingresar la cantidad requerida.",
                    "Campo requerido",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                TxtCantidad.Focus();
                return;
            }

            var resultado = MessageBox.Show($"¿Confirmar notificación al proveedor?\n\n" +
                                          $"Producto: {TxtProducto.Text}\n" +
                                          $"Cantidad: {TxtCantidad.Text}\n\n" +
                                          $"Se enviará una solicitud de cotización al proveedor.",
                "Confirmar notificación",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (resultado == MessageBoxResult.Yes)
            {
                MessageBox.Show("✉️ Notificación enviada al proveedor exitosamente.\n\n" +
                              $"Producto: {TxtProducto.Text}\n" +
                              $"Cantidad: {TxtCantidad.Text}\n\n" +
                              "Recibirá una respuesta en las próximas 24-48 horas.",
                    "Notificación enviada",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }

        // Evento para el botón Enviar pedido
        private void BtnEnviarPedido_Click(object sender, RoutedEventArgs e)
        {
            // Validar todos los campos obligatorios
            if (string.IsNullOrWhiteSpace(TxtIdPedido.Text))
            {
                MessageBox.Show("El ID del pedido es obligatorio.",
                    "Campo requerido",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                TxtIdPedido.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(TxtIdCliente.Text))
            {
                MessageBox.Show("El ID del cliente es obligatorio.",
                    "Campo requerido",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                TxtIdCliente.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(TxtClienteSolicita.Text))
            {
                MessageBox.Show("El nombre del cliente es obligatorio.",
                    "Campo requerido",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                TxtClienteSolicita.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(TxtProducto.Text))
            {
                MessageBox.Show("Debe especificar el producto o servicio.",
                    "Campo requerido",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                TxtProducto.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(TxtCantidad.Text))
            {
                MessageBox.Show("La cantidad es obligatoria.",
                    "Campo requerido",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                TxtCantidad.Focus();
                return;
            }

            // Validar que cantidad sea numérica
            if (!int.TryParse(TxtCantidad.Text, out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show("La cantidad debe ser un número válido mayor a 0.",
                    "Cantidad inválida",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                TxtCantidad.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(TxtTotal.Text))
            {
                MessageBox.Show("El total a pagar es obligatorio.",
                    "Campo requerido",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                TxtTotal.Focus();
                return;
            }

            // Validar que total sea numérico
            if (!decimal.TryParse(TxtTotal.Text, out decimal total) || total <= 0)
            {
                MessageBox.Show("El total debe ser un monto válido mayor a 0.",
                    "Total inválido",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                TxtTotal.Focus();
                return;
            }

            if (CmbDisponibilidad.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar el estado de disponibilidad.",
                    "Campo requerido",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                CmbDisponibilidad.Focus();
                return;
            }

            ComboBoxItem itemSeleccionado = (ComboBoxItem)CmbDisponibilidad.SelectedItem;
            string disponibilidad = itemSeleccionado.Content.ToString();

            // Calcular fecha de entrega
            DateTime fechaEntrega;
            int diasEntrega;

            if (disponibilidad == "En inventario")
            {
                diasEntrega = 3; // Entrega rápida
                fechaEntrega = DateTime.Now.AddDays(diasEntrega);
            }
            else // Contactar proveedor
            {
                diasEntrega = 10; // Más tiempo de espera
                fechaEntrega = DateTime.Now.AddDays(diasEntrega);
            }

            // Mostrar confirmación
            var resultado = MessageBox.Show($"¿Confirmar envío del pedido?\n\n" +
                                          $"📋 ID Pedido: {TxtIdPedido.Text}\n" +
                                          $"👤 Cliente: {TxtClienteSolicita.Text}\n" +
                                          $"📦 Producto: {TxtProducto.Text}\n" +
                                          $"🔢 Cantidad: {cantidad}\n" +
                                          $"💰 Total: ${total:F2}\n" +
                                          $"📊 Disponibilidad: {disponibilidad}\n" +
                                          $"📅 Fecha estimada de entrega: {fechaEntrega:dd/MM/yyyy}\n" +
                                          $"⏱️ Tiempo de entrega: {diasEntrega} días",
                "Confirmar pedido",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (resultado == MessageBoxResult.Yes)
            {
                MessageBox.Show($"✅ ¡Pedido enviado exitosamente!\n\n" +
                              $"ID Pedido: {TxtIdPedido.Text}\n" +
                              $"Cliente: {TxtClienteSolicita.Text}\n" +
                              $"Fecha de entrega: {fechaEntrega:dd/MM/yyyy}\n\n" +
                              $"El pedido ha sido registrado y se notificará al cliente.",
                    "Pedido exitoso",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                // Aquí guardarías en la base de datos
                // LimpiarCampos();
            }
        }

        // Evento para el botón Ir a menú
        private void BtnIrMenu_Click(object sender, RoutedEventArgs e)
        {
            var resultado = MessageBox.Show("¿Desea volver al menú principal?\n\n" +
                                          "Asegúrese de haber enviado o guardado el pedido si es necesario.",
                "Volver al menú",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (resultado == MessageBoxResult.Yes)
            {
                MessageBox.Show("Redirigiendo al menú principal...",
                    "Navegación",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                // Aquí abrirías tu ventana de menú principal
                // MenuPrincipal menu = new MenuPrincipal();
                // menu.Show();
                // this.Close();
            }
        }

        // Validar entrada numérica en Cantidad
        private void TxtCantidad_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            // Solo permitir números
            e.Handled = !int.TryParse(e.Text, out _);
        }

        // Validar entrada numérica en Total
        private void TxtTotal_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            // Permitir números y punto decimal
            if (!(char.IsDigit(e.Text[0]) || e.Text == "."))
            {
                e.Handled = true;
            }
        }

        // Evento cuando cambia la disponibilidad
        private void CmbDisponibilidad_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbDisponibilidad.SelectedItem != null)
            {
                ComboBoxItem itemSeleccionado = (ComboBoxItem)CmbDisponibilidad.SelectedItem;
                string disponibilidad = itemSeleccionado.Content.ToString();

                if (disponibilidad == "En inventario")
                {
                    MessageBox.Show("✅ Producto disponible en inventario.\n\n" +
                                  "Tiempo estimado de entrega: 3 días hábiles.",
                        "Disponibilidad confirmada",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
                else if (disponibilidad == "Contactar proveedor")
                {
                    MessageBox.Show("⚠️ Producto no disponible en inventario.\n\n" +
                                  "Será necesario contactar al proveedor.\n" +
                                  "Tiempo estimado de entrega: 10 días hábiles.\n\n" +
                                  "Use el botón 'Notificar a proveedor' para enviar la solicitud.",
                        "Contacto con proveedor necesario",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }
            }
        }

        // TODO: Implementar cuando los controles XAML estén definidos
        /*
        // Método auxiliar para limpiar campos
        private void LimpiarCampos()
        {
            TxtIdPedido.Clear();
            TxtIdCliente.Clear();
            TxtClienteSolicita.Clear();
            TxtProducto.Clear();
            TxtCantidad.Clear();
            TxtTotal.Clear();
            CmbDisponibilidad.SelectedIndex = -1;

            MessageBox.Show("Formulario limpiado correctamente.",
                "Campos limpiados",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
        */
    }
}
