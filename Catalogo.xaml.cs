using Npgsql;
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
        private Conexion conexion;

        public Catalogo()
        {
            InitializeComponent();
            conexion = new Conexion();
            CargarCatalogo();
        }

        // Método para cargar todos los productos del catálogo
        public void CargarCatalogo()
        {
            var listaProductos = new List<ProductoViewModel>();

            try
            {
                using (var conn = conexion.GetConnection())
                {
                    string query = @"
                        SELECT 
                            p.id_producto,
                            p.nombre,
                            p.descripcion,
                            p.precio,
                            p.id_categoria,
                            p.estado,
                            c.nombre as nombre_categoria
                        FROM productos p
                        LEFT JOIN categorias c ON p.id_categoria = c.id_categoria
                        WHERE p.estado = 'Activo'
                        ORDER BY p.id_categoria, p.nombre";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listaProductos.Add(new ProductoViewModel
                            {
                                IdProducto = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Descripcion = reader.IsDBNull(2) ? "Sin descripción" : reader.GetString(2),
                                Precio = reader.GetDecimal(3),
                                IdCategoria = reader.GetInt32(4),
                                Estado = reader.GetString(5),
                                NombreCategoria = reader.IsDBNull(6) ? "Sin categoría" : reader.GetString(6)
                            });
                        }
                    }
                }

                // Filtrar por categoría y asignar a los DataGrids
                CartonDataGrid.ItemsSource = listaProductos.Where(p => p.IdCategoria == 1).ToList();
                PlasticosDataGrid.ItemsSource = listaProductos.Where(p => p.IdCategoria == 2).ToList();
                VehiculosDataGrid.ItemsSource = listaProductos.Where(p => p.IdCategoria == 3).ToList();

                if (listaProductos.Count == 0)
                {
                    MessageBox.Show("No hay productos activos en el catálogo.",
                        "Información",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el catálogo:\n{ex.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        // Botón de búsqueda
        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string textoBusqueda = TxtBuscar.Text?.Trim() ?? string.Empty;

            if (string.IsNullOrEmpty(textoBusqueda))
            {
                MessageBox.Show("Por favor, ingrese un término de búsqueda.",
                    "Campo vacío",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            var listaProductos = new List<ProductoViewModel>();

            try
            {
                using (var conn = conexion.GetConnection())
                {
                    string query = @"
                        SELECT 
                            p.id_producto,
                            p.nombre,
                            p.descripcion,
                            p.precio,
                            p.id_categoria,
                            p.estado,
                            c.nombre as nombre_categoria
                        FROM productos p
                        LEFT JOIN categorias c ON p.id_categoria = c.id_categoria
                        WHERE p.estado = 'Activo'
                        AND (
                            (@esNumero = TRUE AND p.id_producto = @idProd)
                            OR (@esNumero = FALSE AND (
                                p.nombre ILIKE @patron 
                                OR p.descripcion ILIKE @patron
                                OR c.nombre ILIKE @patron
                            ))
                        )
                        ORDER BY p.id_categoria, p.nombre";

                    bool esNumero = int.TryParse(textoBusqueda, out int idProd);
                    string patron = $"%{textoBusqueda}%";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@esNumero", esNumero);
                        cmd.Parameters.AddWithValue("@idProd", idProd);
                        cmd.Parameters.AddWithValue("@patron", patron);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listaProductos.Add(new ProductoViewModel
                                {
                                    IdProducto = reader.GetInt32(0),
                                    Nombre = reader.GetString(1),
                                    Descripcion = reader.IsDBNull(2) ? "Sin descripción" : reader.GetString(2),
                                    Precio = reader.GetDecimal(3),
                                    IdCategoria = reader.GetInt32(4),
                                    Estado = reader.GetString(5),
                                    NombreCategoria = reader.IsDBNull(6) ? "Sin categoría" : reader.GetString(6)
                                });
                            }
                        }
                    }
                }

                // Filtrar por categoría y asignar a los DataGrids
                CartonDataGrid.ItemsSource = listaProductos.Where(p => p.IdCategoria == 1).ToList();
                PlasticosDataGrid.ItemsSource = listaProductos.Where(p => p.IdCategoria == 2).ToList();
                VehiculosDataGrid.ItemsSource = listaProductos.Where(p => p.IdCategoria == 3).ToList();

                if (listaProductos.Count == 0)
                {
                    MessageBox.Show($"No se encontraron productos con: '{textoBusqueda}'",
                        "Sin resultados",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
                else
                {
                    // Cambiar a la pestaña del primer resultado
                    if (listaProductos.Count > 0)
                    {
                        int primeraCategoria = listaProductos[0].IdCategoria;
                        TabControlCategorias.SelectedIndex = primeraCategoria - 1;
                    }

                    MessageBox.Show($"Se encontraron {listaProductos.Count} producto(s)",
                        "Resultados",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    
                    // Limpiar barra de búsqueda después de mostrar resultados
                    TxtBuscar.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar productos:\n{ex.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        // Botón Actualizar Catálogo
        private void BtnActualizarCatalogo_Click(object sender, RoutedEventArgs e)
        {
            var resultado = MessageBox.Show(
                "¿Está seguro de que desea actualizar el catálogo?\n\nEsto recargará todos los productos activos.",
                "Confirmar actualización",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (resultado == MessageBoxResult.Yes)
            {
                try
                {
                    // Limpiar campo de búsqueda
                    TxtBuscar.Clear();

                    // Volver a la primera pestaña
                    TabControlCategorias.SelectedIndex = 0;

                    // Recargar el catálogo
                    CargarCatalogo();

                    // 👉 En lugar de mostrar un MessageBox, abrir la ventana ActualizarCat
                    ActualizarCat ventanaActualizar = new ActualizarCat();
                    ventanaActualizar.Show();   // o ShowDialog() si quieres que sea modal
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al actualizar el catálogo:\n{ex.Message}",
                        "Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
        }

        // Botón Ir a Menú
        private void BtnIrMenu_Click(object sender, RoutedEventArgs e)
        {
            var resultado = MessageBox.Show(
                "¿Desea volver al menú principal?",
                "Volver al menú",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (resultado == MessageBoxResult.Yes)
            {
                this.Close();
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
                    // Aquí puedes agregar lógica adicional si es necesario
                }
            }
        }

        // Evento para el TextBox de búsqueda (presionar Enter)
        private void TxtBuscar_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                BtnBuscar_Click(sender, e);
            }
        }
    }

    // Clase ViewModel para los productos
    public class ProductoViewModel
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int IdCategoria { get; set; }
        public string Estado { get; set; }
        public string NombreCategoria { get; set; }
    }
}