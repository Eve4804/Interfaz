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
    /// Lógica de interacción para RecuperarCont.xaml
    /// </summary>
    public partial class RecuperarCont : Window
    {
        public RecuperarCont()
        {
            InitializeComponent();
        }

        private void Enviar_Click(object sender, RoutedEventArgs e)
        {
            string correo = CorreoTextBox.Text;
            //validación 
            if (string.IsNullOrWhiteSpace(correo) || !correo.Contains("@"))
            {
                MessageBox.Show("Por favor ingrese un correo válido que contenga '@'.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            else
            {
                MessageBox.Show("Se ha enviado un correo de recuperación",
                         "Recuperar contraseña",
                         MessageBoxButton.OK,
                         MessageBoxImage.Information);
            }

            this.Close();

        }
    }
}
