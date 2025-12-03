using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Npgsql;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using System.IO;
using ClosedXML.Excel;



namespace Interfaz
{
    public partial class GestionVentas : Window
    {
        // ← Tu conexión global
        Conexion conexion = new Conexion();

        private List<Venta> todasLasVentas = new List<Venta>();
        private Venta ventaSeleccionada;

        public GestionVentas()
        {
            InitializeComponent();
            CargarVentasDesdeBD();
            MostrarDiagnostico();
        }

        private void MostrarDiagnostico()
        {
            try
            {
                using (var conn = conexion.GetConnection())
                {
                    // Contar total de ventas
                    string countQuery = "SELECT COUNT(*) FROM ventas";
                    using (var cmd = new NpgsqlCommand(countQuery, conn))
                    {
                        int totalVentas = Convert.ToInt32(cmd.ExecuteScalar());
                        
                        if (totalVentas == 0)
                        {
                            MessageBox.Show("⚠️ La tabla 'ventas' está vacía.\n\nNo hay pedidos para mostrar.",
                                "Sin datos", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                    }

                    // Obtener estados únicos
                    string estadosQuery = "SELECT DISTINCT estado FROM ventas ORDER BY estado";
                    var estados = new List<string>();
                    using (var cmd = new NpgsqlCommand(estadosQuery, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            estados.Add($"'{reader.GetString(0)}'");
                        }
                    }

                    // Contar por estado
                    string mensaje = "📊 DIAGNÓSTICO DE VENTAS\n\n";
                    foreach (var estado in estados)
                    {
                        string estadoLimpio = estado.Trim('\'');
                        string countByEstado = $"SELECT COUNT(*) FROM ventas WHERE estado = '{estadoLimpio}'";
                        using (var cmd = new NpgsqlCommand(countByEstado, conn))
                        {
                            int count = Convert.ToInt32(cmd.ExecuteScalar());
                            mensaje += $"• {estado}: {count} pedido(s)\n";
                        }
                    }

                    mensaje += "\n✅ Estados configurados:\n";
                    mensaje += "• 'Nueva' → Pedidos Nuevos\n";
                    mensaje += "• 'Pendiente' → Pedidos Pendientes\n";
                    mensaje += "• 'Cancelada' → Pedidos Cancelados";

                    MessageBox.Show(mensaje, "Diagnóstico de Estados", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en diagnóstico: {ex.Message}", 
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        
        private void CargarVentasDesdeBD(string filtro = "")
        {
            todasLasVentas.Clear();

            try
            {
                using (var conn = conexion.GetConnection())
                {
                    string query = @"
                        SELECT 
                            v.id_venta,
                            v.fecha,
                            c.nombre AS cliente,
                            p.nombre AS producto,
                            v.cantidad,
                            c.direccion_fiscal,
                            c.direccion_envio,
                            COALESCE(c.direccion_envio, c.direccion_fiscal) AS direccion,
                            v.total,
                            v.estado,
                            v.metodo_pago,
                            v.fecha_entrega_estimada,
                            v.fecha_entrega_real,
                            v.fecha_creacion,
                            v.fecha_modificacion
                        FROM ventas v
                        JOIN clientes c ON c.id_cliente = v.id_cliente
                        LEFT JOIN productos p ON p.id_producto = v.id_producto";

                    if (!string.IsNullOrWhiteSpace(filtro))
                        query += " WHERE CAST(v.id_venta AS TEXT) ILIKE @f OR c.nombre ILIKE @f";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(filtro))
                            cmd.Parameters.AddWithValue("@f", $"%{filtro}%");

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
                                    DireccionFiscal = reader.IsDBNull(5) ? "" : reader.GetString(5),
                                    DireccionEnvio = reader.IsDBNull(6) ? "" : reader.GetString(6),
                                    Direccion = reader.IsDBNull(7) ? "" : reader.GetString(7),
                                    Total = reader.GetDecimal(8),
                                    Estado = reader.GetString(9),
                                    MetodoPago = reader.GetString(10),
                                    FechaEntregaEstimada = reader.IsDBNull(11) ? (DateTime?)null : reader.GetDateTime(11),
                                    FechaEntregaReal = reader.IsDBNull(12) ? (DateTime?)null : reader.GetDateTime(12),
                                    FechaCreacion = reader.GetDateTime(13),
                                    FechaModificacion = reader.GetDateTime(14)
                                });
                            }
                        }
                    }
                }

                ActualizarListas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando ventas: " + ex.Message,
                                "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ActualizarListas()
        {
            // Filtrado según los estados reales de tu BD
            var nuevos = todasLasVentas.Where(v => 
                v.Estado.Trim().Equals("Nueva", StringComparison.OrdinalIgnoreCase)).ToList();
            
            var pendientes = todasLasVentas.Where(v => 
                v.Estado.Trim().Equals("Pendiente", StringComparison.OrdinalIgnoreCase) || 
                v.Estado.Trim().Equals("Confirmada", StringComparison.OrdinalIgnoreCase) ||
                v.Estado.Trim().Equals("En tránsito", StringComparison.OrdinalIgnoreCase) ||
                v.Estado.Trim().Equals("En transito", StringComparison.OrdinalIgnoreCase)).ToList();
            
            var cancelados = todasLasVentas.Where(v => 
                v.Estado.Trim().Equals("Cancelada", StringComparison.OrdinalIgnoreCase)).ToList();

            DgPedidosNuevos.ItemsSource = nuevos;
            DgPedidosPendientes.ItemsSource = pendientes;
            DgPedidosCancelados.ItemsSource = cancelados;

            // Mensaje de depuración
            if (todasLasVentas.Count > 0)
            {
                var estadosUnicos = todasLasVentas.Select(v => $"'{v.Estado}'").Distinct().ToList();
                string mensaje = $"📊 Total ventas: {todasLasVentas.Count}\n" +
                                $"Estados en BD: {string.Join(", ", estadosUnicos)}\n" +
                                $"Nuevos: {nuevos.Count} | Pendientes: {pendientes.Count} | Cancelados: {cancelados.Count}";
                
                System.Diagnostics.Debug.WriteLine(mensaje);
                
                // Si no hay nuevos ni pendientes, mostrar alerta
                if (nuevos.Count == 0 && pendientes.Count == 0 && todasLasVentas.Count > 0)
                {
                    MessageBox.Show($"⚠️ No se encontraron pedidos nuevos o pendientes.\n\n{mensaje}\n\n" +
                                   "Verifica que los estados en la base de datos sean:\n" +
                                   "• 'Pendiente' para nuevos\n" +
                                   "• 'Confirmada' o 'En tránsito' para pendientes\n" +
                                   "• 'Cancelada' para cancelados",
                                   "Información de Estados", 
                                   MessageBoxButton.OK, 
                                   MessageBoxImage.Information);
                }
            }
            else if (todasLasVentas.Count == 0)
            {
                MessageBox.Show("No hay ventas registradas en la base de datos.",
                               "Sin datos",
                               MessageBoxButton.OK,
                               MessageBoxImage.Information);
            }
        }

     
        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string criterio = TxtBuscar.Text.Trim();
            CargarVentasDesdeBD(criterio);
        }

        private void BtnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            var venta = (sender as Button)?.DataContext as Venta;
            if (venta == null) return;

            if (MessageBox.Show(
                $"¿Confirmar el pedido #{venta.IdVenta}?",
                "Confirmar Pedido",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    using (var conn = conexion.GetConnection())
                    {
                        string query = @"UPDATE ventas 
                                         SET estado='Pendiente', fecha_modificacion=NOW() 
                                         WHERE id_venta=@id";

                        using (var cmd = new NpgsqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", venta.IdVenta);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Pedido confirmado", "Éxito",
                                    MessageBoxButton.OK, MessageBoxImage.Information);

                    CargarVentasDesdeBD();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al confirmar: " + ex.Message);
                }
            }
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            var venta = (sender as Button)?.DataContext as Venta;
            if (venta == null) return;

            if (MessageBox.Show(
                $"¿Cancelar el pedido #{venta.IdVenta}?",
                "Cancelar Pedido",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    using (var conn = conexion.GetConnection())
                    {
                        string query = @"UPDATE ventas 
                                         SET estado='Cancelada', fecha_modificacion=NOW() 
                                         WHERE id_venta=@id";

                        using (var cmd = new NpgsqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", venta.IdVenta);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Pedido cancelado", "Cancelado",
                                    MessageBoxButton.OK, MessageBoxImage.Information);

                    CargarVentasDesdeBD();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cancelar: " + ex.Message);
                }
            }
        }

        private void BtnVerDetalles_Click(object sender, RoutedEventArgs e)
        {
            var venta = (sender as Button)?.DataContext as Venta;
            if (venta == null) return;

            MessageBox.Show(
                $"📋 DETALLES DEL PEDIDO #{venta.IdVenta}\n\n" +
                $"Cliente: {venta.ClienteNombre}\n" +
                $"Producto: {venta.ProductoNombre}\n" +
                $"Dirección de Entrega: {venta.Direccion}\n" +
                $"Dirección Fiscal: {venta.DireccionFiscal}\n" +
                $"Dirección Envío: {venta.DireccionEnvio}\n" +
                $"Fecha: {venta.Fecha:dd/MM/yyyy HH:mm}\n" +
                $"Estado: {venta.Estado}\n" +
                $"Método de Pago: {venta.MetodoPago}\n" +
                $"Cantidad: {venta.Cantidad}\n" +
                $"Total: {venta.Total:C}\n" +
                (venta.FechaEntregaEstimada.HasValue ? $"\nFecha Estimada: {venta.FechaEntregaEstimada:dd/MM/yyyy}" : "") +
                (venta.FechaEntregaReal.HasValue ? $"\nFecha Real: {venta.FechaEntregaReal:dd/MM/yyyy}" : ""),
                "Detalles del Pedido",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void DgPedidosNuevos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ventaSeleccionada = DgPedidosNuevos.SelectedItem as Venta;
        }

        private void DgPedidosPendientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ventaSeleccionada = DgPedidosPendientes.SelectedItem as Venta;
        }

        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ExportarExcel_Click(object sender, RoutedEventArgs e)
        {
            DataGrid dg = ObtenerDataGridActivo();

            if (dg == null || dg.Items.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.", "Sin datos", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Excel (*.xlsx)|*.xlsx";
            save.FileName = "ReporteVentas.xlsx";

            if (save.ShowDialog() == true)
            {
                try
                {
                    var wb = new XLWorkbook();
                    var ws = wb.Worksheets.Add("Reporte Ventas");

                    // ENCABEZADOS
                    ws.Cell(1, 1).Value = "ID Pedido";
                    ws.Cell(1, 2).Value = "Fecha";
                    ws.Cell(1, 3).Value = "Cliente";
                    ws.Cell(1, 4).Value = "Producto";
                    ws.Cell(1, 5).Value = "Cantidad";
                    ws.Cell(1, 6).Value = "Dirección";
                    ws.Cell(1, 7).Value = "Total";
                    ws.Cell(1, 8).Value = "Estado";
                    ws.Cell(1, 9).Value = "Método Pago";

                    // Estilo de encabezados
                    var headerRange = ws.Range(1, 1, 1, 9);
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;

                    // FILAS DE DATOS
                    int fila = 2;
                    foreach (var item in dg.Items)
                    {
                        if (item is Venta venta)
                        {
                            ws.Cell(fila, 1).Value = venta.IdVenta;
                            ws.Cell(fila, 2).Value = venta.Fecha.ToString("dd/MM/yyyy");
                            ws.Cell(fila, 3).Value = venta.ClienteNombre;
                            ws.Cell(fila, 4).Value = venta.ProductoNombre;
                            ws.Cell(fila, 5).Value = venta.Cantidad;
                            ws.Cell(fila, 6).Value = venta.Direccion;
                            ws.Cell(fila, 7).Value = venta.Total;
                            ws.Cell(fila, 8).Value = venta.Estado;
                            ws.Cell(fila, 9).Value = venta.MetodoPago;
                            fila++;
                        }
                    }

                    // Ajustar ancho de columnas
                    ws.Columns().AdjustToContents();

                    wb.SaveAs(save.FileName);

                    MessageBox.Show($"Archivo Excel generado correctamente.\n\n{fila - 2} registros exportados.",
                        "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al generar Excel:\n{ex.Message}",
                        "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void ExportarPDF_Click(object sender, RoutedEventArgs e)
        {
            DataGrid dg = ObtenerDataGridActivo();

            if (dg == null || dg.Items.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.", "Sin datos",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "PDF (*.pdf)|*.pdf";
            save.FileName = "ReporteVentas.pdf";

            if (save.ShowDialog() == true)
            {
                try
                {
                    Document pdfDoc = new Document(PageSize.A4.Rotate(), 10, 10, 10, 10); // Horizontal para más columnas
                    PdfWriter.GetInstance(pdfDoc, new FileStream(save.FileName, FileMode.Create));
                    pdfDoc.Open();

                    // ENCABEZADO DEL REPORTE
                    Paragraph titulo = new Paragraph("REPORTE DE VENTAS\n\n",
                        FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18));
                    titulo.Alignment = Element.ALIGN_CENTER;
                    pdfDoc.Add(titulo);

                    Paragraph fecha = new Paragraph($"Fecha de generación: {DateTime.Now:dd/MM/yyyy HH:mm}\n\n",
                        FontFactory.GetFont(FontFactory.HELVETICA, 10));
                    fecha.Alignment = Element.ALIGN_RIGHT;
                    pdfDoc.Add(fecha);

                    // TABLA CON 9 COLUMNAS
                    PdfPTable tabla = new PdfPTable(9);
                    tabla.WidthPercentage = 100;
                    tabla.SetWidths(new float[] { 8f, 12f, 15f, 15f, 20f, 10f, 10f, 10f, 12f });

                    // ENCABEZADOS
                    string[] headers = { "ID", "Fecha", "Cliente", "Producto", "Cantidad", "Dirección", "Total", "Estado", "Método Pago" };
                    foreach (string header in headers)
                    {
                        PdfPCell celdaEncabezado = new PdfPCell(new Phrase(header, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9)))
                        {
                            BackgroundColor = new BaseColor(230, 230, 230),
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            Padding = 5
                        };
                        tabla.AddCell(celdaEncabezado);
                    }

                    // FILAS DE DATOS
                    int contador = 0;
                    foreach (var item in dg.Items)
                    {
                        if (item is Venta venta)
                        {
                            tabla.AddCell(new Phrase(venta.IdVenta.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                            tabla.AddCell(new Phrase(venta.Fecha.ToString("dd/MM/yyyy"), FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                            tabla.AddCell(new Phrase(venta.ClienteNombre, FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                            tabla.AddCell(new Phrase(venta.ProductoNombre, FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                            tabla.AddCell(new Phrase(venta.Cantidad.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                            tabla.AddCell(new Phrase(venta.Direccion, FontFactory.GetFont(FontFactory.HELVETICA, 7)));
                            tabla.AddCell(new Phrase(venta.Total.ToString("C"), FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                            tabla.AddCell(new Phrase(venta.Estado, FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                            tabla.AddCell(new Phrase(venta.MetodoPago, FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                            contador++;
                        }
                    }

                    pdfDoc.Add(tabla);

                    // PIE DE PÁGINA
                    Paragraph pie = new Paragraph($"\n\nTotal de registros: {contador}",
                        FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10));
                    pie.Alignment = Element.ALIGN_RIGHT;
                    pdfDoc.Add(pie);

                    pdfDoc.Close();

                    MessageBox.Show($"PDF generado correctamente.\n\n{contador} registros exportados.",
                        "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al generar PDF:\n{ex.Message}",
                        "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private DataGrid ObtenerDataGridActivo()
        {
            if (TabControlVentas == null) return null;

            var selected = TabControlVentas.SelectedItem as TabItem;
            if (selected == null) return null;

            string h = selected.Header?.ToString() ?? "";

            if (h.Contains("📋") || h.Contains("Nuevos")) return DgPedidosNuevos;
            if (h.Contains("⏳") || h.Contains("Pendientes")) return DgPedidosPendientes;
            if (h.Contains("❌") || h.Contains("Cancelados")) return DgPedidosCancelados;

            return null;
        }

    }
}

