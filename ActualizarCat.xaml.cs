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
    /// Lógica de interacción para ActualizarCat.xaml
    /// </summary>
    public partial class ActualizarCat : Window
    {
        public ActualizarCat()
        {
            InitializeComponent();
        }
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            // Validar campos básicos
            if (string.IsNullOrWhiteSpace(txtId.Text) ||
                string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtPrecio.Text))
            {
                MessageBox.Show("Por favor, complete los campos obligatorios",
                                "Advertencia",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            // Validar precio numérico
            if (!EsPrecioValido(txtPrecio.Text))
            {
                MessageBox.Show("El precio debe ser un número válido (ejemplo: 10.50)",
                                "Error en precio",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return;
            }

            // Validar URL de imagen
            if (!string.IsNullOrWhiteSpace(txtImagen.Text) &&
                !EsURLValida(txtImagen.Text))
            {
                MessageBox.Show("La URL de la imagen no es válida",
                                "URL Inválida",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return;
            }

            // Si todo esta bien
            MessageBox.Show("El producto se ha guardado correctamente",
                            "Guardado",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
        }

        //      MÉTODOS DE VALIDACIÓN
        
        private bool EsPrecioValido(string precio)
        {
            // Elimina el símbolo $ si lo trae
            precio = precio.Replace("$", "").Trim();

            // Verificar formato numérico decimal
            return decimal.TryParse(precio, out decimal resultado) && resultado >= 0;
        }

        private bool EsURLValida(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult) &&
                   (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }


        //        CANCELAR CON CONFIRMACIÓN

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "¿Seguro que deseas cancelar? Los cambios no se guardarán",
                "Confirmar cancelación",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

    }
}
