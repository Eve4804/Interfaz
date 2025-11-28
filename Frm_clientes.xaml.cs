using System;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Interfaz
{
    public partial class Frm_clientes : Window
    {
        public Frm_clientes()
        {
            InitializeComponent();
        }

        // ----------------------------------------------------
        // MÉTODO PARA MOSTRAR UN CLIENTE EN LOS TEXTBOX
        // ----------------------------------------------------
        private void MostrarClienteEnFormulario(Cliente c)
        {
            if (c == null) return;

            txtIdCliente.Text = c.IdCliente.ToString();
            txtRFC.Text = c.RFC ?? "";
            txtNombre.Text = c.Nombre ?? "";
            txtEmail.Text = c.Email ?? "";
            txtTelefono.Text = c.Telefono ?? "";
            txtDireccionFiscal.Text = c.DireccionFiscal ?? "";
            txtDireccionEnvio.Text = c.DireccionEnvio ?? "";

            // Selección de tipo en ComboBox
            cmbTipo.SelectedIndex = -1;
            for (int i = 0; i < cmbTipo.Items.Count; i++)
            {
                if (cmbTipo.Items[i] is ComboBoxItem item &&
                    (string)item.Content == c.Tipo)
                {
                    cmbTipo.SelectedIndex = i;
                    break;
                }
            }
        }

        // ----------------------------------------------------
        // BOTÓN: BUSCAR POR NOMBRE O RFC
        // ----------------------------------------------------
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string filtro = txtBuscar.Text.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(filtro))
            {
                MessageBox.Show("Escribe un nombre o RFC para buscar.");
                return;
            }

            var cliente = FakeDatabase.Clientes
                .FirstOrDefault(c =>
                    c.Nombre.ToLower().Contains(filtro) ||
                    c.RFC.ToLower().Contains(filtro));

            if (cliente == null)
            {
                MessageBox.Show("No se encontró ningún cliente.");
                return;
            }

            MostrarClienteEnFormulario(cliente);
        }

        // ----------------------------------------------------
        // BOTÓN: ACTIVAR CLIENTE
        // ----------------------------------------------------
        private void btnActivar_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtIdCliente.Text, out int id))
            {
                MessageBox.Show("Primero selecciona un cliente.");
                return;
            }

            var cliente = FakeDatabase.Clientes.FirstOrDefault(c => c.IdCliente == id);
            if (cliente == null) return;

            cliente.Activo = true;

            MessageBox.Show("Cliente activado correctamente.");
        }

        // ----------------------------------------------------
        // BOTÓN: DESACTIVAR CLIENTE
        // ----------------------------------------------------
        private void btnDesactivar_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtIdCliente.Text, out int id))
            {
                MessageBox.Show("Primero selecciona un cliente.");
                return;
            }

            var cliente = FakeDatabase.Clientes.FirstOrDefault(c => c.IdCliente == id);
            if (cliente == null) return;

            cliente.Activo = false;

            MessageBox.Show("Cliente desactivado correctamente.");
        }

        // ----------------------------------------------------
        // BOTÓN: LIMPIAR CAMPOS
        // ----------------------------------------------------
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtIdCliente.Text = "";
            txtRFC.Text = "";
            txtNombre.Text = "";
            txtEmail.Text = "";
            txtTelefono.Text = "";
            txtDireccionFiscal.Text = "";
            txtDireccionEnvio.Text = "";
            cmbTipo.SelectedIndex = -1;
            txtBuscar.Text = "";
        }

        // ----------------------------------------------------
        // FUNCIÓN PARA ESCAPAR TEXTO CSV
        // ----------------------------------------------------
        private string EscapeCsv(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return "";

            return value.Replace("\"", "\"\"");
        }

        // ----------------------------------------------------
        // BOTÓN: GENERAR REPORTE CSV
        // ----------------------------------------------------
        private void btnGenerarReporte_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var header = "IdCliente,Nombre,RFC,Tipo,Estado,Email,Telefono,DireccionFiscal,DireccionEnvio";

                var lines = FakeDatabase.Clientes.Select(c =>
                    $"{c.IdCliente}," +
                    $"\"{EscapeCsv(c.Nombre)}\"," +
                    $"\"{EscapeCsv(c.RFC)}\"," +
                    $"\"{EscapeCsv(c.Tipo)}\"," +
                    $"\"{(c.Activo ? "Activo" : "Inactivo")}\"," +
                    $"\"{EscapeCsv(c.Email)}\"," +
                    $"\"{EscapeCsv(c.Telefono)}\"," +
                    $"\"{EscapeCsv(c.DireccionFiscal)}\"," +
                    $"\"{EscapeCsv(c.DireccionEnvio)}\""
                );

                string ruta = Path.Combine(
                    Path.GetTempPath(),
                    $"reporte_clientes_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
                );

                File.WriteAllLines(ruta, (new[] { header }).Concat(lines));

                Process.Start(new ProcessStartInfo(ruta) { UseShellExecute = true });

                MessageBox.Show("Reporte generado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generando reporte:\n" + ex.Message);
            }
        }
    }
}
