using System;
using System.Text.RegularExpressions;
using System.Windows;
using Npgsql;

namespace Interfaz
{
    public partial class RecuperarCont : Window
    {
        private string usuarioNombreUsuario = "";
        private string usuarioNombreReal = "";
        private int usuarioId = -1;

        public RecuperarCont()
        {
            InitializeComponent();
            Conexion conexion = new Conexion();
            conexion.ProbarConexion();
        }

        private void Confirmar_Click(object sender, RoutedEventArgs e)
        {
            string idTexto = UsuarioIdTextBox.Text.Trim();
            string nuevaContrasena = NuevaContrasenaBox.Password;
            string confirmarContrasena = ConfirmarContrasenaBox.Password;

            // CP1: Campos vacíos
            if (string.IsNullOrWhiteSpace(idTexto) &&
                string.IsNullOrWhiteSpace(nuevaContrasena) &&
                string.IsNullOrWhiteSpace(confirmarContrasena))
            {
                MessageBox.Show("Llene los campos por favor.",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            // CP5: Validar que el ID sea numérico
            if (!int.TryParse(idTexto, out int idUsuario))
            {
                MessageBox.Show("Por favor ingrese un Id válido (número).",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            // CP2: ID correcto pero contraseñas vacías
            if (string.IsNullOrWhiteSpace(nuevaContrasena) ||
                string.IsNullOrWhiteSpace(confirmarContrasena))
            {
                MessageBox.Show("La contraseña debe de tener letras y números y no pasar de los 8 caracteres.",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            // CP3: Contraseñas no coinciden
            if (nuevaContrasena != confirmarContrasena)
            {
                MessageBox.Show("Las contraseñas no coinciden.",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            // Validar letras y números, máximo 8 caracteres
            if (!Regex.IsMatch(nuevaContrasena, @"^[a-zA-Z0-9]{1,8}$"))
            {
                MessageBox.Show("La contraseña debe contener solo letras y números y no pasar de 8 caracteres.",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            try
            {
                Conexion conexion = new Conexion();
                using (var conn = conexion.GetConnection())
                {
                    // Verificar si el usuario existe y obtener ambos campos
                    string query = "SELECT nombre_usuario, nombre FROM administradores WHERE id_usuario = @id;";
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("id", idUsuario);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                // CP4: ID incorrecto
                                MessageBox.Show("El Id de usuario no existe.",
                                    "Error",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                                return;
                            }

                            usuarioNombreUsuario = reader.GetString(0);
                            usuarioNombreReal = reader.GetString(1);
                            usuarioId = idUsuario;
                        }
                    }

                    // Actualizar contraseña
                    string update = "UPDATE administradores SET contrasena = @pass WHERE id_usuario = @id;";
                    using (var cmd = new NpgsqlCommand(update, conn))
                    {
                        cmd.Parameters.AddWithValue("pass", nuevaContrasena);
                        cmd.Parameters.AddWithValue("id", usuarioId);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show(
                                $"Contraseña actualizada correctamente para el usuario '{usuarioNombreUsuario}' ({usuarioNombreReal}).",
                                "Éxito",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);

                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo actualizar la contraseña.",
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar contraseña: {ex.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}