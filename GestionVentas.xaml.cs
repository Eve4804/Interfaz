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
    /// Lógica de interacción para GestionVentas.xaml
    /// </summary>
    public partial class GestionVentas : Window
    {
        public GestionVentas()
        {
            InitializeComponent();
        }

        // Evento para el botón Buscar
        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string textoBusqueda = TxtBuscar.Text.Trim();

            if (string.IsNullOrEmpty(textoBusqueda))
            {
                MessageBox.Show("Por favor, ingrese un ID de pedido o cliente para buscar.",
                    "Campo vacío",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            MessageBox.Show($"Buscando pedido o cliente: '{textoBusqueda}'...\n\nSe mostrarán los resultados coincidentes.",
                "Búsqueda en proceso",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            // Aquí podrías buscar en la base de datos
        }

        // Evento para el botón Calcular fecha de entrega
        private void BtnCalcularFechaEntrega_Click(object sender, RoutedEventArgs e)
        {
            // Validar que los campos necesarios estén llenos
            if (string.IsNullOrWhiteSpace(TxtIdPedidoNuevo.Text))
            {
                MessageBox.Show("Debe ingresar el ID del pedido.",
                    "Campo requerido",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                TxtIdPedidoNuevo.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(TxtCantidadNuevo.Text))
            {
                MessageBox.Show("Debe ingresar la cantidad del producto.",
                    "Campo requerido",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                TxtCantidadNuevo.Focus();
                return;
            }

            // Validar que la cantidad sea un número válido
            if (!int.TryParse(TxtCantidadNuevo.Text, out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show("La cantidad debe ser un número mayor a 0.",
                    "Cantidad inválida",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                TxtCantidadNuevo.Focus();
                return;
            }

            // Calcular fecha estimada (ejemplo: 7 días desde hoy)
            DateTime fechaEntrega = DateTime.Now.AddDays(7);

            MessageBox.Show($"Fecha de entrega calculada:\n\n" +
                          $"📅 {fechaEntrega:dd/MM/yyyy}\n\n" +
                          $"Tiempo estimado: 7 días hábiles\n" +
                          $"Cantidad de productos: {cantidad}",
                "Cálculo exitoso",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        // Evento para validar entrada numérica en Cantidad
        private void TxtCantidad_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            // Solo permitir números
            e.Handled = !int.TryParse(e.Text, out _);
        }

        // Evento para validar entrada numérica en Total
        private void TxtTotal_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            // Permitir números y punto decimal
            if (!(char.IsDigit(e.Text[0]) || e.Text == "."))
            {
                e.Handled = true;
            }
        }

        // Evento cuando cambia el estado de la orden en Pedidos Realizados
        private void CmbEstadoOrden_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbEstadoOrden.SelectedItem != null)
            {
                ComboBoxItem selectedItem = (ComboBoxItem)CmbEstadoOrden.SelectedItem;
                string estado = selectedItem.Content.ToString();

                // Validar que haya un ID de pedido antes de cambiar estado
                if (!string.IsNullOrWhiteSpace(TxtIdPedidoRealizado.Text))
                {
                    MessageBox.Show($"Estado del pedido #{TxtIdPedidoRealizado.Text} actualizado a:\n\n" +
                                  $"📋 {estado}\n\n" +
                                  $"Los cambios se guardarán automáticamente.",
                        "Estado actualizado",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
            }
        }

        // Evento para el botón Ir a Menú
        private void BtnIrMenu_Click(object sender, RoutedEventArgs e)
        {
            var resultado = MessageBox.Show("¿Desea volver al menú principal?\n\n" +
                                          "Asegúrese de haber guardado todos los cambios.",
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

        // Evento para el TextBox de búsqueda (Enter)
        private void TxtBuscar_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                BtnBuscar_Click(sender, e);
            }
        }

        // Método auxiliar para limpiar campos de Pedidos Nuevos
        private void LimpiarCamposNuevos()
        {
            TxtIdPedidoNuevo.Clear();
            TxtIdClienteNuevo.Clear();
            TxtClienteSolicitaNuevo.Clear();
            TxtProductoNuevo.Clear();
            TxtCantidadNuevo.Clear();
            TxtTotalNuevo.Clear();

            MessageBox.Show("Campos limpiados correctamente.",
                "Formulario limpio",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        // Evento para validar y guardar pedido nuevo (opcional, si quisieras agregar un botón)
        private void GuardarPedidoNuevo()
        {
            // Validaciones
            if (string.IsNullOrWhiteSpace(TxtIdPedidoNuevo.Text) ||
                string.IsNullOrWhiteSpace(TxtIdClienteNuevo.Text) ||
                string.IsNullOrWhiteSpace(TxtClienteSolicitaNuevo.Text) ||
                string.IsNullOrWhiteSpace(TxtProductoNuevo.Text) ||
                string.IsNullOrWhiteSpace(TxtCantidadNuevo.Text) ||
                string.IsNullOrWhiteSpace(TxtTotalNuevo.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios para crear un pedido.",
                    "Campos incompletos",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            // Validar cantidad
            if (!int.TryParse(TxtCantidadNuevo.Text, out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show("La cantidad debe ser un número mayor a 0.",
                    "Cantidad inválida",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            // Validar total
            if (!decimal.TryParse(TxtTotalNuevo.Text, out decimal total) || total <= 0)
            {
                MessageBox.Show("El total debe ser un monto válido mayor a 0.",
                    "Total inválido",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            var resultado = MessageBox.Show($"¿Confirmar creación del pedido?\n\n" +
                                          $"ID Pedido: {TxtIdPedidoNuevo.Text}\n" +
                                          $"Cliente: {TxtClienteSolicitaNuevo.Text}\n" +
                                          $"Producto: {TxtProductoNuevo.Text}\n" +
                                          $"Cantidad: {cantidad}\n" +
                                          $"Total: ${total:F2}",
                "Confirmar pedido",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (resultado == MessageBoxResult.Yes)
            {
                MessageBox.Show("¡Pedido creado exitosamente!\n\n" +
                              $"ID: {TxtIdPedidoNuevo.Text}\n" +
                              $"El pedido ha sido registrado en el sistema.",
                    "Éxito",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                // Aquí guardarías en la base de datos
                // LimpiarCamposNuevos();
            }
        }




    }
}
