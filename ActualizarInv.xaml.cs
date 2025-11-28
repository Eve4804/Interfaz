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
    /// Lógica de interacción para ActualizarInv.xaml
    /// </summary>
    public partial class ActualizarInv : Window
    {
        public ActualizarInv()
        {
            InitializeComponent();
        }

        private void Cancelar_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validar campos obligatorios
                if (string.IsNullOrWhiteSpace(txtId.Text) ||
                    string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    cmbEstado.SelectedItem == null ||
                    cmbCategoria.SelectedItem == null ||
                    string.IsNullOrWhiteSpace(txtPrecio.Text))
                {
                    MessageBox.Show("Por favor completa todos los campos.",
                                    "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Intentar convertir precio
                if (!double.TryParse(txtPrecio.Text, out double precio))
                {
                    MessageBox.Show("El precio debe ser un número válido.",
                                    "Error de formato", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Obtener valores
                string id = txtId.Text.Trim();
                string nombre = txtNombre.Text.Trim();
                string estado = ((ComboBoxItem)cmbEstado.SelectedItem).Content.ToString();
                string categoria = ((ComboBoxItem)cmbCategoria.SelectedItem).Content.ToString();

                // Aquí podrías guardar en base de datos o lista en memoria
                MessageBox.Show(
                    $"Producto guardado:\n\n" +
                    $"ID: {id}\n" +
                    $"Nombre: {nombre}\n" +
                    $"Estado: {estado}\n" +
                    $"Categoría: {categoria}\n" +
                    $"Precio: {precio:C}",
                    "Éxito", MessageBoxButton.OK, MessageBoxImage.Information
                );

                // Limpiar campos después de guardar
                txtId.Clear();
                txtNombre.Clear();
                txtPrecio.Clear();
                cmbEstado.SelectedIndex = -1;
                cmbCategoria.SelectedIndex = -1;
            }
            catch (FormatException fex)
            {
                MessageBox.Show($"Formato inválido: {fex.Message}",
                                "Error de formato", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (NullReferenceException nrex)
            {
                MessageBox.Show($"Referencia nula: {nrex.Message}",
                                "Error de referencia", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (InvalidOperationException ioex)
            {
                MessageBox.Show($"Operación inválida: {ioex.Message}",
                                "Error de operación", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (System.Data.DataException dex)
            {
                MessageBox.Show($"Error en acceso a datos: {dex.Message}",
                                "Error de datos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                // Captura cualquier error inesperado
                MessageBox.Show($"Ocurrió un error al guardar: {ex.Message}",
                                "Error inesperado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
