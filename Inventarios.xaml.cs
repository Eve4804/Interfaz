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
    /// Lógica de interacción para Inventarios.xaml
    /// </summary>
    public partial class Inventarios : Window
    {
        public Inventarios()
        {
            InitializeComponent();
            //carga datos de cartón, vehículos y plásticos
            CargarCarton();
            CargarVehiculos();
            CargarPlasticos();
        }

        private void CargarCarton()
        {
            var productosCarton = new List<Producto>
            {
                new Producto { Id = "C001", Nombre = "Caja pequeña", Precio = 10, Cantidad = 120, Estado = "Disponible" },
                new Producto { Id = "C002", Nombre = "Caja mediana", Precio = 15, Cantidad = 85, Estado = "Disponible" },
                new Producto { Id = "C003", Nombre = "Caja grande", Precio = 20, Cantidad = 60, Estado = "Disponible" },
                new Producto { Id = "C004", Nombre = "Cartón corrugado", Precio = 25, Cantidad = 200, Estado = "Disponible" },
                new Producto { Id = "C005", Nombre = "Tubos de cartón", Precio = 5, Cantidad = 1, Estado = "Insuficiente" }

            };
            CartonDataGrid.ItemsSource = productosCarton;

        }

        private void CargarVehiculos()
        {
            var productosVehiculos = new List<Producto>
            {
                new Producto { Id = "V001", Nombre = "Auto compacto", Precio = 15000, Cantidad = 10, Estado = "Disponible" },
                new Producto { Id = "V002", Nombre = "Camioneta", Precio = 25000, Cantidad = 5, Estado = "Disponible" },
                new Producto { Id = "V003", Nombre = "Motocicleta", Precio = 8000, Cantidad = 15, Estado = "Disponible" },
                new Producto { Id = "V004", Nombre = "SUV", Precio = 30000, Cantidad = 3, Estado = "Disponible" },
                new Producto { Id = "V005", Nombre = "Camión de carga", Precio = 50000, Cantidad = 1, Estado = "Insuficiente" }
            };
            VehiculosDataGrid.ItemsSource = productosVehiculos;
        }

        private void CargarPlasticos()
        {
            var productosPlasticos = new List<Producto>
            {
                new Producto { Id = "P001", Nombre = "Botella de agua", Precio = 2, Cantidad = 500, Estado = "Disponible" },
                new Producto { Id = "P002", Nombre = "Envase de comida", Precio = 3, Cantidad = 300, Estado = "Disponible" },
                new Producto { Id = "P003", Nombre = "Bolsa de plástico", Precio = 1, Cantidad = 1000, Estado = "Disponible" },
                new Producto { Id = "P004", Nombre = "Contenedor de plástico", Precio = 5, Cantidad = 150, Estado = "Disponible" },
                new Producto { Id = "P005", Nombre = "Película plástica", Precio = 4, Cantidad = 2, Estado = "Insuficiente" }
            };
            PlasticosDataGrid.ItemsSource = productosPlasticos;
        }


        // Clase auxiliar para productos
        public class Producto
        {
            public string Id { get; set; }
            public string Nombre { get; set; }
            public double Precio { get; set; }
            public int Cantidad { get; set; }
            public string Estado { get; set; }
        }

        private void Buscar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ActInve_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ContacProv_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            MenuWindow menu = new MenuWindow();
            menu.Show();
        }
    }
}
