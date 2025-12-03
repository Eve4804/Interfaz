using System;
using System.Windows;
using System.Windows.Controls;
using Npgsql;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using System.IO;
using System.Collections.Generic;

namespace Interfaz
{
    public partial class Frm_clientes : Window
    {
        private Conexion conexion = new Conexion(); // tu clase de conexión
        private int? idClienteActual = null;

        public Frm_clientes()
        {
            InitializeComponent();
        }

        // Clase para el DataGrid
        public class ClienteViewModel
        {
            public int IdCliente { get; set; }
            public string Rfc { get; set; }
            public string Nombre { get; set; }
            public string Tipo { get; set; }
            public string Email { get; set; }
            public string Telefono { get; set; }
            public string DireccionFiscal { get; set; }
            public string DireccionEnvio { get; set; }
            public string Estado { get; set; }
        }

        // Buscar cliente por RFC, nombre o "Todos"
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string criterio = txtBuscar.Text.Trim();

            if (string.IsNullOrEmpty(criterio))
            {
                MessageBox.Show("Ingrese RFC, nombre o escriba 'Todos' para ver todos los clientes.", "Aviso",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var listaClientes = new List<ClienteViewModel>();

                using (var conn = conexion.GetConnection())
                {
                    string query;
                    
                    // Si el criterio es "Todos", traer todos los clientes
                    if (criterio.Equals("Todos", StringComparison.OrdinalIgnoreCase))
                    {
                        query = @"
                            SELECT id_cliente, rfc, nombre, tipo, email, telefono,
                                   direccion_fiscal, direccion_envio, estado
                            FROM clientes
                            ORDER BY nombre";
                    }
                    else
                    {
                        query = @"
                            SELECT id_cliente, rfc, nombre, tipo, email, telefono,
                                   direccion_fiscal, direccion_envio, estado
                            FROM clientes
                            WHERE rfc ILIKE @criterio OR nombre ILIKE @criterio
                            ORDER BY nombre";
                    }

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        if (!criterio.Equals("Todos", StringComparison.OrdinalIgnoreCase))
                        {
                            cmd.Parameters.AddWithValue("@criterio", $"%{criterio}%");
                        }

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listaClientes.Add(new ClienteViewModel
                                {
                                    IdCliente = reader.GetInt32(0),
                                    Rfc = reader.GetString(1),
                                    Nombre = reader.GetString(2),
                                    Tipo = reader.GetString(3),
                                    Email = reader.IsDBNull(4) ? "" : reader.GetString(4),
                                    Telefono = reader.IsDBNull(5) ? "" : reader.GetString(5),
                                    DireccionFiscal = reader.IsDBNull(6) ? "" : reader.GetString(6),
                                    DireccionEnvio = reader.IsDBNull(7) ? "" : reader.GetString(7),
                                    Estado = reader.GetString(8)
                                });
                            }
                        }
                    }
                }

                if (listaClientes.Count == 0)
                {
                    MessageBox.Show("No se encontraron clientes con ese criterio.", "Sin resultados",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    borderResultados.Visibility = Visibility.Collapsed;
                }
                else if (listaClientes.Count == 1 && !criterio.Equals("Todos", StringComparison.OrdinalIgnoreCase))
                {
                    // Si solo hay un resultado y no es búsqueda "Todos", cargarlo directamente
                    CargarClienteEnFormulario(listaClientes[0]);
                    borderResultados.Visibility = Visibility.Collapsed;
                }
                else
                {
                    // Si hay múltiples resultados o es búsqueda "Todos", mostrar el DataGrid
                    dgResultados.ItemsSource = listaClientes;
                    borderResultados.Visibility = Visibility.Visible;
                    MessageBox.Show($"Se encontraron {listaClientes.Count} clientes. Seleccione uno de la tabla o exporte a PDF.",
                        "Resultados", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar cliente:\n{ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Evento cuando se selecciona un cliente del DataGrid
        private void dgResultados_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dgResultados.SelectedItem is ClienteViewModel clienteSeleccionado)
            {
                CargarClienteEnFormulario(clienteSeleccionado);
                borderResultados.Visibility = Visibility.Collapsed;
            }
        }

        // Método para cargar datos del cliente en el formulario
        private void CargarClienteEnFormulario(ClienteViewModel cliente)
        {
            idClienteActual = cliente.IdCliente;
            txtIdCliente.Text = cliente.IdCliente.ToString();
            txtRFC.Text = cliente.Rfc;
            txtNombre.Text = cliente.Nombre;
            cmbTipo.Text = cliente.Tipo;
            txtEmail.Text = cliente.Email;
            txtTelefono.Text = cliente.Telefono;
            txtDireccionFiscal.Text = cliente.DireccionFiscal;
            txtDireccionEnvio.Text = cliente.DireccionEnvio;
            cmbEstado.Text = cliente.Estado;
        }

        // Guardar nuevo cliente o actualizar existente
        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            // Validaciones
            if (string.IsNullOrWhiteSpace(txtRFC.Text) || txtRFC.Text.Length != 13)
            {
                MessageBox.Show("El RFC debe tener exactamente 13 caracteres.",
                    "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre es obligatorio.",
                    "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (cmbTipo.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar el tipo de cliente (Persona Física/Moral).",
                    "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (cmbEstado.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar el estado (Activo/Inactivo).",
                    "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (var conn = conexion.GetConnection())
                {
                    string query;
                    
                    // Si hay ID, actualizar; si no, insertar nuevo
                    if (idClienteActual.HasValue)
                    {
                        query = @"
                            UPDATE clientes
                            SET rfc = @rfc, nombre = @nombre, tipo = @tipo,
                                email = @email, telefono = @telefono,
                                direccion_fiscal = @direccionFiscal,
                                direccion_envio = @direccionEnvio,
                                estado = @estado,
                                fecha_modificacion = CURRENT_TIMESTAMP
                            WHERE id_cliente = @idCliente";
                    }
                    else
                    {
                        query = @"
                            INSERT INTO clientes 
                                (rfc, nombre, tipo, email, telefono, direccion_fiscal, direccion_envio, estado, activo, fecha_alta, fecha_modificacion)
                            VALUES 
                                (@rfc, @nombre, @tipo, @email, @telefono, @direccionFiscal, @direccionEnvio, @estado, TRUE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)";
                    }

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@rfc", txtRFC.Text.Trim());
                        cmd.Parameters.AddWithValue("@nombre", txtNombre.Text.Trim());
                        cmd.Parameters.AddWithValue("@tipo", ((ComboBoxItem)cmbTipo.SelectedItem).Content.ToString());
                        cmd.Parameters.AddWithValue("@email", string.IsNullOrWhiteSpace(txtEmail.Text) ? (object)DBNull.Value : txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@telefono", string.IsNullOrWhiteSpace(txtTelefono.Text) ? (object)DBNull.Value : txtTelefono.Text.Trim());
                        cmd.Parameters.AddWithValue("@direccionFiscal", string.IsNullOrWhiteSpace(txtDireccionFiscal.Text) ? (object)DBNull.Value : txtDireccionFiscal.Text.Trim());
                        cmd.Parameters.AddWithValue("@direccionEnvio", string.IsNullOrWhiteSpace(txtDireccionEnvio.Text) ? (object)DBNull.Value : txtDireccionEnvio.Text.Trim());
                        cmd.Parameters.AddWithValue("@estado", ((ComboBoxItem)cmbEstado.SelectedItem).Content.ToString());
                        
                        if (idClienteActual.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@idCliente", idClienteActual.Value);
                        }

                        int filas = cmd.ExecuteNonQuery();
                        if (filas > 0)
                        {
                            string mensaje = idClienteActual.HasValue ? 
                                "Cliente actualizado correctamente." : 
                                "Cliente registrado correctamente.";
                            
                            MessageBox.Show(mensaje, "Éxito",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            
                            LimpiarFormulario();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar cliente:\n{ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Limpiar formulario
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            idClienteActual = null;
            txtBuscar.Clear();
            txtIdCliente.Clear();
            txtRFC.Clear();
            txtNombre.Clear();
            cmbTipo.SelectedIndex = -1;
            txtEmail.Clear();
            txtTelefono.Clear();
            txtDireccionFiscal.Clear();
            txtDireccionEnvio.Clear();
            cmbEstado.SelectedIndex = -1;
            borderResultados.Visibility = Visibility.Collapsed;
            txtBuscar.Focus();
        }

        // =====================================================================
        // EXPORTAR CLIENTES A PDF
        // =====================================================================
        private void btnExportarPDF_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Verificar si hay clientes en el DataGrid
                if (dgResultados.Items.Count == 0)
                {
                    MessageBox.Show("Primero busque clientes para exportar.\n\nEscriba 'Todos' en el buscador para ver todos los clientes.",
                        "Sin datos", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF Files|*.pdf",
                    FileName = $"Clientes_{DateTime.Now:yyyyMMdd_HHmmss}.pdf"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    Document pdfDoc = new Document(PageSize.A4.Rotate(), 25, 25, 30, 30); // Horizontal para más columnas
                    PdfWriter.GetInstance(pdfDoc, new FileStream(saveFileDialog.FileName, FileMode.Create));
                    pdfDoc.Open();

                    // Título
                    iTextSharp.text.Paragraph titulo = new iTextSharp.text.Paragraph("LISTADO DE CLIENTES\n\n",
                        new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 18, iTextSharp.text.Font.BOLD));
                    titulo.Alignment = Element.ALIGN_CENTER;
                    pdfDoc.Add(titulo);

                    // Fecha
                    iTextSharp.text.Paragraph fecha = new iTextSharp.text.Paragraph($"Fecha: {DateTime.Now:dd/MM/yyyy HH:mm}\n\n",
                        new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10));
                    fecha.Alignment = Element.ALIGN_RIGHT;
                    pdfDoc.Add(fecha);

                    // Tabla con 8 columnas
                    PdfPTable tabla = new PdfPTable(8) { WidthPercentage = 100 };
                    
                    // Anchos de columnas
                    float[] widths = new float[] { 8f, 12f, 20f, 12f, 15f, 12f, 15f, 8f };
                    tabla.SetWidths(widths);

                    // Encabezados
                    string[] headers = { "ID", "RFC", "Nombre", "Tipo", "Email", "Teléfono", "Dirección Fiscal", "Estado" };
                    foreach (string header in headers)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(header, 
                            FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9)))
                        {
                            BackgroundColor = new BaseColor(200, 200, 200),
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            Padding = 5
                        };
                        tabla.AddCell(cell);
                    }

                    // Datos
                    foreach (var item in dgResultados.Items)
                    {
                        if (item is ClienteViewModel cliente)
                        {
                            tabla.AddCell(new Phrase(cliente.IdCliente.ToString(), 
                                FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                            tabla.AddCell(new Phrase(cliente.Rfc, 
                                FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                            tabla.AddCell(new Phrase(cliente.Nombre, 
                                FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                            tabla.AddCell(new Phrase(cliente.Tipo, 
                                FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                            tabla.AddCell(new Phrase(cliente.Email, 
                                FontFactory.GetFont(FontFactory.HELVETICA, 7)));
                            tabla.AddCell(new Phrase(cliente.Telefono, 
                                FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                            tabla.AddCell(new Phrase(cliente.DireccionFiscal, 
                                FontFactory.GetFont(FontFactory.HELVETICA, 7)));
                            tabla.AddCell(new Phrase(cliente.Estado, 
                                FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                        }
                    }

                    pdfDoc.Add(tabla);

                    // Total de clientes
                    iTextSharp.text.Paragraph total = new iTextSharp.text.Paragraph($"\n\nTotal de clientes: {dgResultados.Items.Count}",
                        new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD));
                    total.Alignment = Element.ALIGN_RIGHT;
                    pdfDoc.Add(total);

                    pdfDoc.Close();

                    MessageBox.Show($"PDF exportado exitosamente.\n\nTotal de clientes: {dgResultados.Items.Count}", 
                        "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al exportar a PDF: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
