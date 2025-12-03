using System.Windows;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using System.IO;
using System;

namespace Interfaz
{
    /// <summary>
    /// Lógica de interacción para Notificaciones.xaml
    /// </summary>
    public partial class Notificaciones : Window
    {
        public Notificaciones()
        {
            InitializeComponent();
            CargarNotificaciones();
        }

        public void CargarNotificaciones()
        {
            // Cargar solicitudes enviadas desde el servicio
            lstSolicitudes.ItemsSource = null;
            lstSolicitudes.ItemsSource = NotificacionesService.ObtenerSolicitudes();

            // Cargar respuestas recibidas desde el servicio
            lstRespuestas.ItemsSource = null;
            lstRespuestas.ItemsSource = NotificacionesService.ObtenerRespuestas();
        }

        private void Cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void lstSolicitudes_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lstSolicitudes.SelectedItem != null)
            {
                MessageBox.Show(lstSolicitudes.SelectedItem.ToString(),
                                "Detalle de solicitud",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }
        }

        private void lstRespuestas_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lstRespuestas.SelectedItem != null)
            {
                MessageBox.Show(lstRespuestas.SelectedItem.ToString(),
                                "Detalle de respuesta",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }
        }

        // =====================================================================
        // EXPORTAR A PDF
        // =====================================================================
        private void ExportarPDF_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF Files|*.pdf",
                    FileName = $"Notificaciones_{DateTime.Now:yyyyMMdd_HHmmss}.pdf"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 30, 30);
                    PdfWriter.GetInstance(pdfDoc, new FileStream(saveFileDialog.FileName, FileMode.Create));
                    pdfDoc.Open();

                    // Título
                    iTextSharp.text.Paragraph titulo = new iTextSharp.text.Paragraph("NOTIFICACIONES\n\n",
                        new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 18, iTextSharp.text.Font.BOLD));
                    titulo.Alignment = Element.ALIGN_CENTER;
                    pdfDoc.Add(titulo);

                    // Fecha
                    iTextSharp.text.Paragraph fecha = new iTextSharp.text.Paragraph($"Fecha: {DateTime.Now:dd/MM/yyyy HH:mm}\n\n",
                        new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10));
                    fecha.Alignment = Element.ALIGN_RIGHT;
                    pdfDoc.Add(fecha);

                    // Solicitudes Enviadas
                    iTextSharp.text.Paragraph subtitulo1 = new iTextSharp.text.Paragraph("SOLICITUDES ENVIADAS\n\n",
                        new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.BOLD));
                    pdfDoc.Add(subtitulo1);

                    if (lstSolicitudes.Items.Count > 0)
                    {
                        iTextSharp.text.List listaSolicitudes = new iTextSharp.text.List(iTextSharp.text.List.UNORDERED);
                        foreach (var item in lstSolicitudes.Items)
                        {
                            listaSolicitudes.Add(new ListItem(item.ToString(),
                                new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10)));
                        }
                        pdfDoc.Add(listaSolicitudes);
                    }
                    else
                    {
                        pdfDoc.Add(new iTextSharp.text.Paragraph("No hay solicitudes enviadas.\n",
                            new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.ITALIC)));
                    }

                    pdfDoc.Add(new iTextSharp.text.Paragraph("\n\n"));

                    // Respuestas Recibidas
                    iTextSharp.text.Paragraph subtitulo2 = new iTextSharp.text.Paragraph("RESPUESTAS RECIBIDAS\n\n",
                        new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.BOLD));
                    pdfDoc.Add(subtitulo2);

                    if (lstRespuestas.Items.Count > 0)
                    {
                        iTextSharp.text.List listaRespuestas = new iTextSharp.text.List(iTextSharp.text.List.UNORDERED);
                        foreach (var item in lstRespuestas.Items)
                        {
                            listaRespuestas.Add(new ListItem(item.ToString(),
                                new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10)));
                        }
                        pdfDoc.Add(listaRespuestas);
                    }
                    else
                    {
                        pdfDoc.Add(new iTextSharp.text.Paragraph("No hay respuestas recibidas.\n",
                            new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.ITALIC)));
                    }

                    pdfDoc.Close();

                    MessageBox.Show("Notificaciones exportadas exitosamente a PDF", "Éxito",
                        MessageBoxButton.OK, MessageBoxImage.Information);
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