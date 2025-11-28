using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Interfaz
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string usuario = UsuarioTextBox.Text;
            string contrasena = ContrasenaBox.Password;

            try
            {
                // Validar campos vacíos
                if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(contrasena))
                {
                    throw new ArgumentException("Debe llenar todos los campos.");
                }

                // Validar formato de contraseña (exactamente 8 números)
                if (!Regex.IsMatch(contrasena, @"^\d{8}$"))
                {
                    throw new FormatException("La contraseña debe contener exactamente 8 números.");
                }

                // Usuario y contraseña correctos
                if (usuario == "admin" && contrasena == "12345678")
                {
                    MessageBox.Show("Login correcto. Bienvenido " + usuario);

                    // Abrir ventana principal
                    MenuWindow menu = new MenuWindow();
                    menu.Show();

                    // Cerrar login
                    this.Close();
                }
                else
                {
                    // Validaciones específicas
                    bool usuarioCorrecto = usuario == "admin";
                    bool contrasenaCorrecta = contrasena == "12345678";

                    if (!usuarioCorrecto && !contrasenaCorrecta)
                    {
                        throw new UnauthorizedAccessException("Usuario y contraseña incorrectos.");
                    }
                    else if (!usuarioCorrecto)
                    {
                        throw new UnauthorizedAccessException("Usuario incorrecto.");
                    }
                    else if (!contrasenaCorrecta)
                    {
                        throw new UnauthorizedAccessException("Contraseña incorrecta.");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Campos vacíos", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Formato inválido", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Error de autenticación", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado: " + ex.Message,
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            RecuperarCont recupcont = new RecuperarCont();
            recupcont.ShowDialog();

        }
    }
}
