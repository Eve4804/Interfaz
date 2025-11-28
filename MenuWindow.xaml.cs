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
    /// Lógica de interacción para MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public MenuWindow()
        {
            InitializeComponent();
        
        
        }
        private void Inventarios_Click(object sender, RoutedEventArgs e)
        {
            Inventarios win = new Inventarios();
            win.Show();
        }

        private void Compras_Click(object sender, RoutedEventArgs e)
        {
            HisVentas win = new HisVentas();
            win.Show();
        }

        private void Pagos_Click(object sender, RoutedEventArgs e)
        {
            Pagos win = new Pagos();
            win.Show();
        }

        private void Productos_Click(object sender, RoutedEventArgs e)
        {
            Catalogo win = new Catalogo();
            win.Show();
        }

        private void Usuarios_Click(object sender, RoutedEventArgs e)
        {
            Frm_clientes win = new Frm_clientes();
            win.Show();
        }

        private void Ventas_Click(object sender, RoutedEventArgs e)
        {
            GestionVentas win = new GestionVentas();
            win.Show();
        }

        private void Entregas_Click(object sender, RoutedEventArgs e)
        {
            CalcularFecha win = new CalcularFecha();
            win.Show();
        }

        private void Cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
