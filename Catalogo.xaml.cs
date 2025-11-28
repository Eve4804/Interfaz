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
    /// Lógica de interacción para Catalogo.xaml
    /// </summary>
    public partial class Catalogo : Window
    {
        public Catalogo()
        {
            InitializeComponent();
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string textoBusqueda = TxtBuscar.Text.Trim();

            if (string.IsNullOrEmpty(textoBusqueda))
            {
                MessageBox.Show("Por favor, ingrese un término de búsqueda.",
                    "Campo vacío",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            MessageBox.Show($"Buscando: '{textoBusqueda}'...\n\nEsta funcionalidad se implementará próximamente",
                "Búsqueda en proceso",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            // Aquí podrías agregar lógica para filtrar productos

        }

        private void BtnActualizarCatalogo_Click(object sender, RoutedEventArgs e)
        {
            var resultado = MessageBox.Show("¿Está seguro de que desea actualizar el catálogo?\n\nEsto recargará todos los productos",
                "Confirmar actualización",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (resultado == MessageBoxResult.Yes)
            {
                MessageBox.Show("Catálogo actualizado correctamente.\n\nSe han cargado 15 productos",
                    "Actualización exitosa",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                // Aquí podrías recargar los datos desde una base de datos
            }
        }

        private void BtnIrMenu_Click(object sender, RoutedEventArgs e)
        {
            var resultado = MessageBox.Show("¿Desea volver al menú principal?\n\nSe cerrará el catálogo actual",
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

        // Evento cuando cambia de pestaña
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                TabItem selectedTab = (TabItem)((TabControl)sender).SelectedItem;
                if (selectedTab != null)
                {
                    string categoria = selectedTab.Header.ToString();
                    // Puedes mostrar un mensaje o cargar datos específicos
                    // MessageBox.Show($"Categoría seleccionada: {categoria}");
                }
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
    }
}
