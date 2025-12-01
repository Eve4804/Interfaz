using System;
using System.Windows;
using Npgsql;

namespace Interfaz
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Conexion conexion = new Conexion();
            conexion.ProbarConexion();
        }

        private void BtnIniciarSesion_Click(object sender, RoutedEventArgs e)
        {
            string usuario = UsuarioTextBox.Text.Trim();
            string contrasena = ContrasenaBox.Password;

            // CP1: Ambos campos vacíos
            if (string.IsNullOrWhiteSpace(usuario) && string.IsNullOrWhiteSpace(contrasena))
            {
                MessageBox.Show("Complete los campos, por favor.",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                UsuarioTextBox.Focus();
                return;
            }

            // CP2: Usuario vacío y contraseña completa
            if (string.IsNullOrWhiteSpace(usuario) && !string.IsNullOrWhiteSpace(contrasena))
            {
                MessageBox.Show("Por favor, ingrese su usuario.",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                UsuarioTextBox.Focus();
                return;
            }

            // CP3: Usuario completo y contraseña vacía
            if (!string.IsNullOrWhiteSpace(usuario) && string.IsNullOrWhiteSpace(contrasena))
            {
                MessageBox.Show("Por favor ingrese su contraseña.",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                ContrasenaBox.Focus();
                return;
            }

            // CP5: Validar longitud de contraseña
            if (contrasena.Length != 8)
            {
                MessageBox.Show("La contraseña debe ser exactamente 8 caracteres.",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                ContrasenaBox.Focus();
                return;
            }

            // Validación contra la base de datos
            if (ValidarCredenciales(usuario, contrasena))
            {
                MenuWindow menuWindow = new MenuWindow(usuario);
                menuWindow.Show();
                this.Close();
            }
            else
            {
                // CP4: Credenciales incorrectas
                MessageBox.Show("Usuario o contraseña incorrectos.",
                    "Error de autenticación",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                ContrasenaBox.Clear();
                ContrasenaBox.Focus();
            }
        }

        private bool ValidarCredenciales(string usuario, string contrasena)
        {
            bool valido = false;

            try
            {
                Conexion conexion = new Conexion();
                using (var conn = conexion.GetConnection())
                {
                    string query = @"SELECT COUNT(*) 
                                     FROM administradores 
                                     WHERE nombre_usuario = @usuario 
                                       AND contrasena = @contrasena;";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("usuario", usuario);
                        cmd.Parameters.AddWithValue("contrasena", contrasena);

                        var result = cmd.ExecuteScalar();
                        int count = Convert.ToInt32(result);

                        valido = (count > 0);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al validar credenciales: {ex.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

            return valido;
        }

        private void OlvideContrasena_Click(object sender, RoutedEventArgs e)
        {
            RecuperarCont recuperarWindow = new RecuperarCont();
            recuperarWindow.ShowDialog();
        }
    }
}
