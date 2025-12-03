using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using Npgsql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Interfaz
{
    public partial class HistorialPagos : Window
    {
        public HistorialPagos()
        {
            InitializeComponent();
            CargarHistorial(); // carga automática al abrir la ventana
        }

        private void BtnActualizar_Click(object sender, RoutedEventArgs e)
        {
            CargarHistorial(); // refresca los datos al dar clic en "Actualizar"
        }

        private void CargarHistorial()
        {
            try
            {
                Conexion conexion = new Conexion();
                using (var conn = conexion.GetConnection())
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    string query = @"
                SELECT 
                    p.id_pago,
                    pr.nombre AS proveedor,
                    p.id_solicitud,
                    p.monto_total,
                    p.forma_pago,
                    p.fecha_pago,
                    p.estado_pago
                FROM pagos p
                JOIN proveedores pr ON p.id_proveedor = pr.id_proveedor
                ORDER BY p.id_pago DESC;";   // 👈 ordena del mayor al menor

                    using (var cmd = new NpgsqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        var listaPagos = new List<RegistroPago>();

                        while (reader.Read())
                        {
                            listaPagos.Add(new RegistroPago
                            {
                                IdPago = Convert.ToInt32(reader["id_pago"]),
                                Proveedor = reader["proveedor"].ToString(),
                                Solicitud = reader["id_solicitud"] != DBNull.Value ? Convert.ToInt32(reader["id_solicitud"]) : 0,
                                Monto = Convert.ToDecimal(reader["monto_total"]).ToString("C2"),
                                FormaPago = reader["forma_pago"].ToString(),
                                FechaPago = Convert.ToDateTime(reader["fecha_pago"]).ToString("dd/MM/yyyy"),
                                Estado = reader["estado_pago"].ToString()
                            });
                        }

                        dgHistorial.ItemsSource = listaPagos;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar historial de pagos:\n{ex.Message}",
                                "Error inesperado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

  
     

        // Si quieres manejar doble clic en una fila
        private void dgHistorial_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (dgHistorial.SelectedItem is RegistroPago pago)
            {
                MessageBox.Show($"Detalles del pago:\n\n" +
                                $"ID Pago: {pago.IdPago}\n" +
                                $"Proveedor: {pago.Proveedor}\n" +
                                $"Solicitud: {pago.Solicitud}\n" +
                                $"Monto: {pago.Monto}\n" +
                                $"Forma de Pago: {pago.FormaPago}\n" +
                                $"Fecha: {pago.FechaPago}\n" +
                                $"Estado: {pago.Estado}",
                                "Detalle de Pago", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ExportarPDF_Click(object sender, RoutedEventArgs e)
        {
            if (dgHistorial.Items.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.", "Aviso",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "PDF (*.pdf)|*.pdf";
            save.FileName = "HistorialPagos.pdf";

            if (save.ShowDialog() == true)
            {
                Document pdfDoc = new Document(PageSize.A4.Rotate(), 10, 10, 10, 10);
                PdfWriter.GetInstance(pdfDoc, new FileStream(save.FileName, FileMode.Create));
                pdfDoc.Open();

                // TÍTULO
                Paragraph titulo = new Paragraph("Historial de Pagos\n\n",
                    FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20));
                titulo.Alignment = Element.ALIGN_CENTER;
                pdfDoc.Add(titulo);

                // TABLA PDF
                PdfPTable tabla = new PdfPTable(dgHistorial.Columns.Count);
                tabla.WidthPercentage = 100;

                // AGREGAR ENCABEZADOS
                foreach (DataGridColumn col in dgHistorial.Columns)
                {
                    PdfPCell header = new PdfPCell(new Phrase(col.Header.ToString()))
                    {
                        BackgroundColor = new BaseColor(220, 220, 220),
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        Padding = 5
                    };
                    tabla.AddCell(header);
                }

                // AGREGAR FILAS
                foreach (var row in dgHistorial.Items)
                {
                    foreach (DataGridColumn col in dgHistorial.Columns)
                    {
                        var cellContent = col.GetCellContent(row) as TextBlock;
                        string texto = cellContent != null ? cellContent.Text : "";

                        PdfPCell celda = new PdfPCell(new Phrase(texto))
                        {
                            HorizontalAlignment = Element.ALIGN_LEFT,
                            Padding = 5
                        };

                        tabla.AddCell(celda);
                    }
                }

                // Agregar tabla al documento
                pdfDoc.Add(tabla);
                pdfDoc.Close();

                MessageBox.Show("PDF exportado correctamente.",
                                "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}