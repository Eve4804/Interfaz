using System;
using System.Windows;
using System.Windows.Input;

namespace Interfaz
{
    public partial class MenuWindow : Window
    {
        private string usuarioActual;

        public MenuWindow(string usuario)
        {
            InitializeComponent();
            usuarioActual = usuario;
            TxtUsuario.Text = $"Usuario: {usuario}";
        }

        // ============================================
        // VENTAS Y CLIENTES
        // ============================================
        private void BtnGestionVentas_Click(object sender, MouseButtonEventArgs e)
        {
            GestionVentas ventana = new GestionVentas();
            ventana.ShowDialog();
        }

        private void BtnHistorialVentas_Click(object sender, MouseButtonEventArgs e)
        {
            HisVentas ventana = new HisVentas();
            ventana.ShowDialog();
        }

        private void BtnClientes_Click(object sender, MouseButtonEventArgs e)
        {
            Frm_clientes ventana = new Frm_clientes();
            ventana.ShowDialog();
        }

        // ============================================
        // INVENTARIO Y PRODUCTOS
        // ============================================
        private void BtnInventarios_Click(object sender, MouseButtonEventArgs e)
        {
            Inventarios ventana = new Inventarios();
            ventana.ShowDialog();
        }

        private void BtnCatalogo_Click(object sender, MouseButtonEventArgs e)
        {
            Catalogo ventana = new Catalogo();
            ventana.ShowDialog();
        }

        private void BtnCalcularFecha_Click(object sender, MouseButtonEventArgs e)
        {
            CalcularFecha ventana = new CalcularFecha();
            ventana.ShowDialog();
        }

        // ============================================
        // PROVEEDORES Y PAGOS
        // ============================================
        private void BtnContactarProveedor_Click(object sender, MouseButtonEventArgs e)
        {
            ContactarProv ventana = new ContactarProv();
            ventana.ShowDialog();
        }

        private void BtnPagos_Click(object sender, MouseButtonEventArgs e)
        {
            Pagos ventana = new Pagos();
            ventana.ShowDialog();
        }

        // ============================================
        // ACCIONES GENERALES
        // ============================================
        private void BtnNotificaciones_Click(object sender, RoutedEventArgs e)
        {
            Notificaciones ventana = new Notificaciones();
            ventana.ShowDialog();
        }

        private void BtnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            var resultado = MessageBox.Show(
                "¿Está seguro que desea cerrar sesión?",
                "Cerrar Sesión",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (resultado == MessageBoxResult.Yes)
            {
                MainWindow loginWindow = new MainWindow();
                loginWindow.Show();
                this.Close();
            }
        }
    }
}
