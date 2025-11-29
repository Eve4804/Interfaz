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
using Interfaz;

namespace Interfaz
{
    /// <summary>
    /// Lógica de interacción para HisVentas.xaml
    /// </summary>
    public partial class HisVentas : Window
    {
        public HisVentas()
        {
            InitializeComponent();
            CargarVentas();
        }

        private void CargarVentas()
        {
            // TODO: Implementar carga desde PostgreSQL
            // dgVentas.ItemsSource = await CargarVentasDesdeDB();
            dgVentas.ItemsSource = new List<Venta>();
        }

        private void dgVentas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgVentas.SelectedItem is Venta ventaSeleccionada)
            {
                // Mostrar datos en los TextBox:
                txtIdVenta.Text = ventaSeleccionada.IdVenta.ToString();
                txtFecha.Text = ventaSeleccionada.Fecha.ToString("yyyy-MM-dd");
                txtClienteDetalle.Text = ventaSeleccionada.ClienteNombre;
                txtEstado.Text = ventaSeleccionada.Estado;
                txtMetodoPago.Text = ventaSeleccionada.MetodoPago;

                // Cargar productos de la venta
                dgItems.ItemsSource = ventaSeleccionada.Items;
            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string cliente = txtCliente.Text.Trim();
            string producto = txtProducto.Text.Trim();

            // TODO: Implementar búsqueda desde PostgreSQL
            // var resultados = await BuscarVentasDesdeDB(cliente, producto);
            // dgVentas.ItemsSource = resultados;
            MessageBox.Show("Función de búsqueda pendiente de implementar con PostgreSQL", "Información");
        }
        

    }
}
