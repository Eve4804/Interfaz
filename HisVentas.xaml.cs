using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using Npgsql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Interfaz
{
    public partial class HisVentas : Window
    {
        private Conexion conexion = new Conexion();
        private List<Venta> todasLasVentas = new List<Venta>();
        private Venta ventaSeleccionada;

        public HisVentas()
        {
            InitializeComponent();
            CargarVentasDesdeBD();
        }

        // =====================================================================
        // CARGAR VENTAS DESDE BD
        // =====================================================================
        private void CargarVentasDesdeBD(string clienteFiltro = "", string productoFiltro = "", DateTime? desde = null, DateTime? hasta = null)
        {
            todasLasVentas.Clear();

            try
            {
                using (var conn = conexion.GetConnection())
                {
                    string query = @"
                        SELECT v.id_venta,
                               v.fecha,
                               c.nombre AS cliente,
                               p.nombre AS producto,
                               v.cantidad,
                               v.total,
                               v.estado,
                               v.metodo_pago
                        FROM ventas v
                        JOIN clientes c ON c.id_cliente = v.id_cliente
                        LEFT JOIN productos p ON p.id_producto = v.id_producto
                        WHERE 1=1";

                    // Filtro por cliente (ID o nombre)
                    if (!string.IsNullOrWhiteSpace(clienteFiltro))
                    {
                        if (int.TryParse(clienteFiltro, out int idCliente))
                            query += " AND v.id_cliente = @idCliente";
                        else
                            query += " AND c.nombre ILIKE @cliente";
                    }

                    // Filtro por producto (ID o nombre)
                    if (!string.IsNullOrWhiteSpace(productoFiltro))
                    {
                        if (int.TryParse(productoFiltro, out int idProducto))
                            query += " AND v.id_producto = @idProducto";
                        else
                            query += " AND p.nombre ILIKE @producto";
                    }

                    if (desde.HasValue)
                        query += " AND v.fecha >= @desde";

                    if (hasta.HasValue)
                        query += " AND v.fecha <= @hasta";

                    query += " ORDER BY v.fecha DESC";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(clienteFiltro))
                        {
                            if (int.TryParse(clienteFiltro, out int idCliente))
                                cmd.Parameters.AddWithValue("@idCliente", idCliente);
                            else
                                cmd.Parameters.AddWithValue("@cliente", $"%{clienteFiltro}%");
                        }

                        if (!string.IsNullOrWhiteSpace(productoFiltro))
                        {
                            if (int.TryParse(productoFiltro, out int idProducto))
                                cmd.Parameters.AddWithValue("@idProducto", idProducto);
                            else
                                cmd.Parameters.AddWithValue("@producto", $"%{productoFiltro}%");
                        }

                        if (desde.HasValue)
                            cmd.Parameters.AddWithValue("@desde", desde.Value.Date);

                        if (hasta.HasValue)
                            cmd.Parameters.AddWithValue("@hasta", hasta.Value.Date.AddDays(1).AddTicks(-1));

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                todasLasVentas.Add(new Venta
                                {
                                    IdVenta = reader.GetInt32(0),
                                    Fecha = reader.GetDateTime(1),
                                    ClienteNombre = reader.GetString(2),
                                    ProductoNombre = reader.IsDBNull(3) ? "Sin producto" : reader.GetString(3),
                                    Cantidad = reader.IsDBNull(4) ? 1 : reader.GetInt32(4),
                                    Total = reader.GetDecimal(5),
                                    Estado = reader.GetString(6),
                                    MetodoPago = reader.GetString(7)
                                });
                            }
                        }
                    }
                }

                dgVentas.ItemsSource = null;
                dgVentas.ItemsSource = todasLasVentas;

                if (todasLasVentas.Count == 0)
                {
                    MessageBox.Show("No se encontraron ventas con los criterios especificados.",
                        "Sin resultados", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando ventas: " + ex.Message,
                                "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // =====================================================================
        // BOTÓN BUSCAR
        // =====================================================================
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string clienteFiltro = txtCliente.Text.Trim();
            string productoFiltro = txtProducto.Text.Trim();
            DateTime? desde = dpDesde.SelectedDate;
            DateTime? hasta = dpHasta.SelectedDate;

            CargarVentasDesdeBD(clienteFiltro, productoFiltro, desde, hasta);

            // Limpiar campos de búsqueda después de buscar
            txtCliente.Clear();
            txtProducto.Clear();
        }

        // =====================================================================
        // SELECCION DE FILA
        // =====================================================================
        private void dgVentas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ventaSeleccionada = dgVentas.SelectedItem as Venta;

            if (ventaSeleccionada != null)
            {
                txtIdVenta.Text = ventaSeleccionada.IdVenta.ToString();
                txtFecha.Text = ventaSeleccionada.Fecha.ToString("dd/MM/yyyy");
                txtClienteDetalle.Text = ventaSeleccionada.ClienteNombre;
                txtProductoDetalle.Text = ventaSeleccionada.ProductoNombre;
                txtCantidadDetalle.Text = ventaSeleccionada.Cantidad.ToString();
                txtEstado.Text = ventaSeleccionada.Estado;
                txtMetodoPago.Text = ventaSeleccionada.MetodoPago;
            }
            else
            {
                txtIdVenta.Text = "";
                txtFecha.Text = "";
                txtClienteDetalle.Text = "";
                txtProductoDetalle.Text = "";
                txtCantidadDetalle.Text = "";
                txtEstado.Text = "";
                txtMetodoPago.Text = "";
            }
        }

        private void ExportarPDF_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "PDF (*.pdf)|*.pdf";
            save.FileName = "HistorialVentas.pdf";

            if (save.ShowDialog() == true)
            {
                // Documento PDF tamaño A4 con márgenes
                Document pdfDoc = new Document(PageSize.A4, 12, 12, 12, 12);
                PdfWriter.GetInstance(pdfDoc, new FileStream(save.FileName, FileMode.Create));
                pdfDoc.Open();

                // Título
                Paragraph titulo = new Paragraph("Historial de Ventas\n\n",
                    new iTextSharp.text.Font(Font.FontFamily.HELVETICA, 18, Font.BOLD));
                titulo.Alignment = Element.ALIGN_CENTER;

                pdfDoc.Add(titulo);

                // Tabla con tantas columnas como tenga el DataGrid
                PdfPTable pdfTable = new PdfPTable(dgVentas.Columns.Count);
                pdfTable.WidthPercentage = 100;

                // === Encabezados ===
                foreach (DataGridColumn col in dgVentas.Columns)
                {
                    PdfPCell header = new PdfPCell(new Phrase(col.Header.ToString()))
                    {
                        BackgroundColor = new BaseColor(230, 230, 230),
                        HorizontalAlignment = Element.ALIGN_CENTER
                    };
                    pdfTable.AddCell(header);
                }

                // === Filas ===
                foreach (var row in dgVentas.Items)
                {
                    if (row == null) continue;

                    foreach (DataGridColumn col in dgVentas.Columns)
                    {
                        // Obtener contenido del DataGrid
                        var cellContent = col.GetCellContent(row) as TextBlock;
                        string texto = cellContent != null ? cellContent.Text : "";

                        pdfTable.AddCell(new Phrase(texto));
                    }
                }

                pdfDoc.Add(pdfTable);
                pdfDoc.Close();

                MessageBox.Show("PDF exportado exitosamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }



}

