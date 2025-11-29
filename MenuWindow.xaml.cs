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
        private void CatProd_Click(object sender, RoutedEventArgs e)
        {
            Catalogo cataprod = new Catalogo();
            cataprod.ShowDialog();
        }

        private void Clientes_Clic(object sender, RoutedEventArgs e)
        {
            Frm_clientes clientes = new Frm_clientes();
            clientes.ShowDialog();
        }

        private void HistorVtn_Click(object sender, RoutedEventArgs e)
        {
            HisVentas hisVentas = new HisVentas();
            hisVentas.ShowDialog();
        }

        private void Inventarios_Click2(object sender, RoutedEventArgs e)
        {
            Inventarios inventarios = new Inventarios();    
            inventarios.ShowDialog();   
        }

        private void GestiVenta_Click(object sender, RoutedEventArgs e)
        {
            GestionVentas gestionVentas = new GestionVentas();
            gestionVentas.ShowDialog();
        }

        private void Entregas_Click(object sender, RoutedEventArgs e)
        {
            CalcularFecha win = new CalcularFecha();
            win.ShowDialog();
        }

        private void CerrarSe_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
