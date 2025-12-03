using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using System.IO;

namespace Interfaz
{
    /// <summary>
    /// Lógica de interacción para Inventarios.xaml
    /// </summary>
    public partial class Inventarios : Window
    {
        public Inventarios()
        {
            InitializeComponent();
            Conexion conexion = new Conexion();
            conexion.ProbarConexion();
            CargarInventario();
        }

         public void CargarInventario()
        {
            var listaInventario = new List<InventarioViewModel>();

            try
            {
                Conexion conexion = new Conexion();
                using (var conn = conexion.GetConnection())
                {
                    string query = @"
                SELECT 
                    COALESCE(i.id_inventario, 0) as id_inventario,
                    p.id_producto,
                    p.nombre AS nombre_producto,
                    p.id_categoria,
                    COALESCE(i.cantidad, 0) as cantidad,
                    COALESCE(i.estado, 'Sin inventario') as estado,
                    COALESCE(i.fecha_actualizacion, CURRENT_TIMESTAMP) as fecha_actualizacion
                FROM productos p
                LEFT JOIN inventarios i ON p.id_producto = i.id_producto
                ORDER BY i.fecha_actualizacion DESC NULLS LAST, p.id_categoria, p.nombre;
            ";

                    using (var cmd = new Npgsql.NpgsqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listaInventario.Add(new InventarioViewModel
                            {
                                IdInventario = reader.GetInt32(0),
                                IdProducto = reader.GetInt32(1),
                                NombreProducto = reader.GetString(2),
                                IdCategoria = reader.GetInt32(3),
                                Cantidad = reader.GetInt32(4),
                                Estado = reader.GetString(5),
                                FechaActualizacion = reader.GetDateTime(6)
                            });
                        }
                    }
                }

                // Filtrar por categoría
                CartonDataGrid.ItemsSource = listaInventario.Where(i => i.IdCategoria == 1).ToList();
                PlasticosDataGrid.ItemsSource = listaInventario.Where(i => i.IdCategoria == 2).ToList();
                VehiculosDataGrid.ItemsSource = listaInventario.Where(i => i.IdCategoria == 3).ToList();

                if (listaInventario.Count == 0)
                {
                    MessageBox.Show("No hay productos en el inventario.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar inventario desde la base de datos:\n\n" + ex.Message, 
                    "Error de Conexión", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        private void ActInve_Click(object sender, RoutedEventArgs e)
        {
            ActualizarInv actualizarInv = new ActualizarInv();
            actualizarInv.Show();
        }

        private void ContacProv_Click(object sender, RoutedEventArgs e)
        {
            ContactarProv contactarProv = new ContactarProv();
            contactarProv.Show();
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Buscar_Click(object sender, RoutedEventArgs e)
        {
            string criterio = BuscarTextBox.Text?.Trim() ?? string.Empty;

            if (string.IsNullOrEmpty(criterio))
            {
                CargarInventario();
                return;
            }

            var listaInventario = new List<InventarioViewModel>();

            try
            {
                Conexion conexion = new Conexion();
                using (var conn = conexion.GetConnection())
                {
                    string query = @"
                SELECT 
                    COALESCE(i.id_inventario, 0) as id_inventario,
                    p.id_producto,
                    p.nombre AS nombre_producto,
                    p.id_categoria,
                    COALESCE(i.cantidad, 0) as cantidad,
                    COALESCE(i.estado, 'Sin inventario') as estado,
                    COALESCE(i.fecha_actualizacion, CURRENT_TIMESTAMP) as fecha_actualizacion
                FROM productos p
                LEFT JOIN inventarios i ON p.id_producto = i.id_producto
                WHERE (
                    (@esNumero = TRUE AND p.id_producto = @idProd)
                    OR (@esNumero = FALSE AND p.nombre ILIKE @patron)
                )
                ORDER BY i.fecha_actualizacion DESC NULLS LAST, p.id_categoria, p.nombre;";

                    bool esNumero = int.TryParse(criterio, out int idProd);
                    string patron = $"%{criterio}%";

                    using (var cmd = new Npgsql.NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@esNumero", esNumero);
                        cmd.Parameters.AddWithValue("@idProd", idProd);
                        cmd.Parameters.AddWithValue("@patron", patron);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listaInventario.Add(new InventarioViewModel
                                {
                                    IdInventario = reader.GetInt32(0),
                                    IdProducto = reader.GetInt32(1),
                                    NombreProducto = reader.GetString(2),
                                    IdCategoria = reader.GetInt32(3),
                                    Cantidad = reader.GetInt32(4),
                                    Estado = reader.GetString(5),
                                    FechaActualizacion = reader.GetDateTime(6)
                                });
                            }
                        }
                    }
                }

                // Filtrar por categoría
                CartonDataGrid.ItemsSource = listaInventario.Where(i => i.IdCategoria == 1).ToList();
                PlasticosDataGrid.ItemsSource = listaInventario.Where(i => i.IdCategoria == 2).ToList();
                VehiculosDataGrid.ItemsSource = listaInventario.Where(i => i.IdCategoria == 3).ToList();

                if (listaInventario.Count == 0)
                {
                    MessageBox.Show($"No se encontraron productos con el criterio: {criterio}", 
                        "Sin resultados", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // Limpiar barra de búsqueda después de mostrar resultados
                    BuscarTextBox.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar inventario:\n\n" + ex.Message, 
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // =====================================================================
        // EXPORTAR A EXCEL
        // =====================================================================
        private void ExportarExcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx",
                    FileName = $"Inventario_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    using (var workbook = new XLWorkbook())
                    {
                        // Hoja de Cartón
                        var wsCarton = workbook.Worksheets.Add("Cartón");
                        wsCarton.Cell(1, 1).Value = "ID Producto";
                        wsCarton.Cell(1, 2).Value = "Nombre";
                        wsCarton.Cell(1, 3).Value = "Cantidad";
                        wsCarton.Cell(1, 4).Value = "Estado";
                        wsCarton.Cell(1, 5).Value = "Fecha Actualización";

                        var cartonItems = CartonDataGrid.ItemsSource as List<InventarioViewModel>;
                        if (cartonItems != null)
                        {
                            int fila = 2;
                            foreach (var item in cartonItems)
                            {
                                wsCarton.Cell(fila, 1).Value = item.IdProducto;
                                wsCarton.Cell(fila, 2).Value = item.NombreProducto;
                                wsCarton.Cell(fila, 3).Value = item.Cantidad;
                                wsCarton.Cell(fila, 4).Value = item.Estado;
                                wsCarton.Cell(fila, 5).Value = item.FechaActualizacion.ToString("dd/MM/yyyy");
                                fila++;
                            }
                        }

                        // Hoja de Plásticos
                        var wsPlasticos = workbook.Worksheets.Add("Plásticos");
                        wsPlasticos.Cell(1, 1).Value = "ID Producto";
                        wsPlasticos.Cell(1, 2).Value = "Nombre";
                        wsPlasticos.Cell(1, 3).Value = "Cantidad";
                        wsPlasticos.Cell(1, 4).Value = "Estado";
                        wsPlasticos.Cell(1, 5).Value = "Fecha Actualización";

                        var plasticosItems = PlasticosDataGrid.ItemsSource as List<InventarioViewModel>;
                        if (plasticosItems != null)
                        {
                            int fila = 2;
                            foreach (var item in plasticosItems)
                            {
                                wsPlasticos.Cell(fila, 1).Value = item.IdProducto;
                                wsPlasticos.Cell(fila, 2).Value = item.NombreProducto;
                                wsPlasticos.Cell(fila, 3).Value = item.Cantidad;
                                wsPlasticos.Cell(fila, 4).Value = item.Estado;
                                wsPlasticos.Cell(fila, 5).Value = item.FechaActualizacion.ToString("dd/MM/yyyy");
                                fila++;
                            }
                        }

                        // Hoja de Vehículos
                        var wsVehiculos = workbook.Worksheets.Add("Vehículos");
                        wsVehiculos.Cell(1, 1).Value = "ID Producto";
                        wsVehiculos.Cell(1, 2).Value = "Nombre";
                        wsVehiculos.Cell(1, 3).Value = "Cantidad";
                        wsVehiculos.Cell(1, 4).Value = "Estado";
                        wsVehiculos.Cell(1, 5).Value = "Fecha Actualización";

                        var vehiculosItems = VehiculosDataGrid.ItemsSource as List<InventarioViewModel>;
                        if (vehiculosItems != null)
                        {
                            int fila = 2;
                            foreach (var item in vehiculosItems)
                            {
                                wsVehiculos.Cell(fila, 1).Value = item.IdProducto;
                                wsVehiculos.Cell(fila, 2).Value = item.NombreProducto;
                                wsVehiculos.Cell(fila, 3).Value = item.Cantidad;
                                wsVehiculos.Cell(fila, 4).Value = item.Estado;
                                wsVehiculos.Cell(fila, 5).Value = item.FechaActualizacion.ToString("dd/MM/yyyy");
                                fila++;
                            }
                        }

                        workbook.SaveAs(saveFileDialog.FileName);
                    }

                    MessageBox.Show("Inventario exportado exitosamente a Excel", "Éxito", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al exportar a Excel: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
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
                    FileName = $"Inventario_{DateTime.Now:yyyyMMdd_HHmmss}.pdf"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 30, 30);
                    PdfWriter.GetInstance(pdfDoc, new FileStream(saveFileDialog.FileName, FileMode.Create));
                    pdfDoc.Open();

                    // Título
                    iTextSharp.text.Paragraph titulo = new iTextSharp.text.Paragraph("INVENTARIO DE PRODUCTOS\n\n",
                        new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 18, iTextSharp.text.Font.BOLD));
                    titulo.Alignment = Element.ALIGN_CENTER;
                    pdfDoc.Add(titulo);

                    // Fecha
                    iTextSharp.text.Paragraph fecha = new iTextSharp.text.Paragraph($"Fecha: {DateTime.Now:dd/MM/yyyy HH:mm}\n\n",
                        new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10));
                    fecha.Alignment = Element.ALIGN_RIGHT;
                    pdfDoc.Add(fecha);

                    // Cartón
                    var cartonItems = CartonDataGrid.ItemsSource as List<InventarioViewModel>;
                    if (cartonItems != null && cartonItems.Count > 0)
                    {
                        iTextSharp.text.Paragraph subtitulo1 = new iTextSharp.text.Paragraph("CARTÓN\n",
                            new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.BOLD));
                        pdfDoc.Add(subtitulo1);

                        PdfPTable tabla1 = new PdfPTable(5) { WidthPercentage = 100 };
                        tabla1.AddCell("ID");
                        tabla1.AddCell("Nombre");
                        tabla1.AddCell("Cantidad");
                        tabla1.AddCell("Estado");
                        tabla1.AddCell("Fecha Act.");

                        foreach (var item in cartonItems)
                        {
                            tabla1.AddCell(item.IdProducto.ToString());
                            tabla1.AddCell(item.NombreProducto);
                            tabla1.AddCell(item.Cantidad.ToString());
                            tabla1.AddCell(item.Estado);
                            tabla1.AddCell(item.FechaActualizacion.ToString("dd/MM/yyyy"));
                        }
                        pdfDoc.Add(tabla1);
                        pdfDoc.Add(new iTextSharp.text.Paragraph("\n"));
                    }

                    // Plásticos
                    var plasticosItems = PlasticosDataGrid.ItemsSource as List<InventarioViewModel>;
                    if (plasticosItems != null && plasticosItems.Count > 0)
                    {
                        iTextSharp.text.Paragraph subtitulo2 = new iTextSharp.text.Paragraph("PLÁSTICOS\n",
                            new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.BOLD));
                        pdfDoc.Add(subtitulo2);

                        PdfPTable tabla2 = new PdfPTable(5) { WidthPercentage = 100 };
                        tabla2.AddCell("ID");
                        tabla2.AddCell("Nombre");
                        tabla2.AddCell("Cantidad");
                        tabla2.AddCell("Estado");
                        tabla2.AddCell("Fecha Act.");

                        foreach (var item in plasticosItems)
                        {
                            tabla2.AddCell(item.IdProducto.ToString());
                            tabla2.AddCell(item.NombreProducto);
                            tabla2.AddCell(item.Cantidad.ToString());
                            tabla2.AddCell(item.Estado);
                            tabla2.AddCell(item.FechaActualizacion.ToString("dd/MM/yyyy"));
                        }
                        pdfDoc.Add(tabla2);
                        pdfDoc.Add(new iTextSharp.text.Paragraph("\n"));
                    }

                    // Vehículos
                    var vehiculosItems = VehiculosDataGrid.ItemsSource as List<InventarioViewModel>;
                    if (vehiculosItems != null && vehiculosItems.Count > 0)
                    {
                        iTextSharp.text.Paragraph subtitulo3 = new iTextSharp.text.Paragraph("VEHÍCULOS\n",
                            new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.BOLD));
                        pdfDoc.Add(subtitulo3);

                        PdfPTable tabla3 = new PdfPTable(5) { WidthPercentage = 100 };
                        tabla3.AddCell("ID");
                        tabla3.AddCell("Nombre");
                        tabla3.AddCell("Cantidad");
                        tabla3.AddCell("Estado");
                        tabla3.AddCell("Fecha Act.");

                        foreach (var item in vehiculosItems)
                        {
                            tabla3.AddCell(item.IdProducto.ToString());
                            tabla3.AddCell(item.NombreProducto);
                            tabla3.AddCell(item.Cantidad.ToString());
                            tabla3.AddCell(item.Estado);
                            tabla3.AddCell(item.FechaActualizacion.ToString("dd/MM/yyyy"));
                        }
                        pdfDoc.Add(tabla3);
                    }

                    pdfDoc.Close();

                    MessageBox.Show("Inventario exportado exitosamente a PDF", "Éxito", 
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
    public class InventarioViewModel
    {
        public int IdInventario { get; set; }
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public int Cantidad { get; set; }
        public int IdCategoria { get; set; }
        public string Estado { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}
