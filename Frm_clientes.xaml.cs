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

            // TODO: Implementar búsqueda desde PostgreSQL
            // var cliente = await BuscarClienteDesdeDB(filtro);
            // if (cliente == null)
            // {
            //     MessageBox.Show("No se encontró ningún cliente.");
            //     return;
            // }
            // MostrarClienteEnFormulario(cliente);
            MessageBox.Show("Función de búsqueda pendiente de implementar con PostgreSQL", "Información");
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

            // TODO: Implementar activación desde PostgreSQL
            // await ActivarClienteEnDB(id);
            MessageBox.Show("Función de activación pendiente de implementar con PostgreSQL", "Información");
        }

        // TODO: Implementar cuando los controles XAML estén definidos
        private void btnDesactivar_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Función pendiente de implementar con controles XAML", "Información");
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Función pendiente de implementar con controles XAML", "Información");
        }

        private string EscapeCsv(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return "";

            return value.Replace("\"", "\"\"");
        }

    }
}
