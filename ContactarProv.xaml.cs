using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static Interfaz.Notificaciones;

namespace Interfaz
{
    /// <summary>
    /// Lógica de interacción para ContactarProv.xaml
    /// </summary>
    public partial class ContactarProv : Window
    {
        public ContactarProv()
        {
            InitializeComponent();
            txtProducto.LostFocus += TxtProducto_LostFocus;
        }

        private void TxtProducto_LostFocus(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtProducto.Text.Trim(), out int idProd))
            {
                try
                {
                    Conexion conexion = new Conexion();
                    using (var conn = conexion.GetConnection())
                    {
                        if (conn.State != System.Data.ConnectionState.Open)
                            conn.Open();

                        string query = @"
                            SELECT p.nombre, c.id_categoria, c.nombre AS categoria
                            FROM productos p
                            JOIN categorias c ON p.id_categoria = c.id_categoria
                            WHERE p.id_producto = @idProd";

                        using (var cmd = new NpgsqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@idProd", idProd);
                            using (var reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    txtDescripcion.Text = reader["nombre"].ToString();

                                    int idCategoria = reader.GetInt32(1);
                                    string nombreCategoria = reader["categoria"].ToString();

                                    foreach (ComboBoxItem item in cmbCategoria.Items)
                                    {
                                        if (item.Tag != null && item.Tag.ToString() == idCategoria.ToString())
                                        {
                                            cmbCategoria.SelectedItem = item;
                                            break;
                                        }
                                    }

                                    CargarProveedoresPorCategoria(idCategoria);
                                }
                                else
                                {
                                    MessageBox.Show("No se encontró un producto con ese ID.",
                                                    "Producto no encontrado", MessageBoxButton.OK, MessageBoxImage.Information);
                                    txtDescripcion.Clear();
                                    cmbCategoria.SelectedIndex = -1;
                                    cmbProveedor.Items.Clear();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al buscar producto:\n{ex.Message}",
                                    "Error inesperado", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void cmbCategoria_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selected = cmbCategoria.SelectedItem as ComboBoxItem;
            if (selected != null && int.TryParse(selected.Tag.ToString(), out int idCategoria))
            {
                CargarProveedoresPorCategoria(idCategoria);
            }
        }

        private void CargarProveedoresPorCategoria(int idCategoria)
        {
            try
            {
                Conexion conexion = new Conexion();
                using (var conn = conexion.GetConnection())
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    string query = @"SELECT id_proveedor, nombre FROM proveedores WHERE id_categoria = @idCat ORDER BY nombre";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idCat", idCategoria);
                        using (var reader = cmd.ExecuteReader())
                        {
                            cmbProveedor.Items.Clear();
                            while (reader.Read())
                            {
                                ComboBoxItem item = new ComboBoxItem
                                {
                                    Content = reader["nombre"].ToString(),
                                    Tag = reader["id_proveedor"].ToString()
                                };
                                cmbProveedor.Items.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar proveedores:\n{ex.Message}",
                                "Error de conexión", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EnvSolicitud_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validación de campos obligatorios
                if (cmbProveedor.SelectedItem == null ||
                    string.IsNullOrWhiteSpace(txtProducto.Text) ||
                    string.IsNullOrWhiteSpace(txtDescripcion.Text) ||
                    string.IsNullOrWhiteSpace(txtCantidad.Text) ||
                    dpFecha.SelectedDate == null)
                {
                    MessageBox.Show("Por favor completa todos los campos antes de enviar la solicitud.",
                                    "Campos incompletos", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Validación de proveedor
                ComboBoxItem item = cmbProveedor.SelectedItem as ComboBoxItem;
                if (item == null || item.Tag == null)
                {
                    MessageBox.Show("Selecciona un proveedor válido.",
                                    "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Captura de valores
                int idProveedor = int.Parse(item.Tag.ToString());
                string nombreProveedor = item.Content.ToString();
                int idProducto = int.Parse(txtProducto.Text.Trim());
                string descripcion = txtDescripcion.Text.Trim();
                int cantidad = int.Parse(txtCantidad.Text.Trim());
                DateTime fechaSolicitud = dpFecha.SelectedDate.Value;

                int nuevoId = 0; // aquí guardaremos el ID generado

                Conexion conexion = new Conexion();
                using (var conn = conexion.GetConnection())
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    // Validación previa en la BD para evitar duplicados
                    string checkQuery = @"
                SELECT COUNT(*) 
                FROM solicitudes_proveedor 
                WHERE id_proveedor = @idProveedor 
                  AND id_producto = @idProducto 
                  AND fecha_solicitud = @fechaSolicitud";

                    using (var checkCmd = new NpgsqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@idProveedor", idProveedor);
                        checkCmd.Parameters.AddWithValue("@idProducto", idProducto);
                        checkCmd.Parameters.AddWithValue("@fechaSolicitud", fechaSolicitud);

                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (count > 0)
                        {
                            MessageBox.Show("Ya existe una solicitud para este proveedor, producto y fecha.",
                                            "Duplicado", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                    }

                    // Inserción con RETURNING para obtener el ID generado
                    string insertQuery = @"
                INSERT INTO solicitudes_proveedor 
                (id_proveedor, id_producto, descripcion, cantidad, fecha_solicitud, estado) 
                VALUES (@idProveedor, @idProducto, @descripcion, @cantidad, @fechaSolicitud, 'Pendiente')
                RETURNING id_solicitud";

                    using (var cmd = new NpgsqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@idProveedor", idProveedor);
                        cmd.Parameters.AddWithValue("@idProducto", idProducto);
                        cmd.Parameters.AddWithValue("@descripcion", descripcion);
                        cmd.Parameters.AddWithValue("@cantidad", cantidad);
                        cmd.Parameters.AddWithValue("@fechaSolicitud", fechaSolicitud);

                        nuevoId = Convert.ToInt32(cmd.ExecuteScalar()); // recupera el ID generado
                    }

                }

                // Mostrar confirmación al usuario
                MessageBox.Show(
                    $"Solicitud enviada correctamente:\n\n" +
                    $"ID Solicitud: {nuevoId}\n" +
                    $"Proveedor: {nombreProveedor}\n" +
                    $"Producto ID: {idProducto}\n" +
                    $"Descripción: {descripcion}\n" +
                    $"Cantidad: {cantidad}\n" +
                    $"Fecha: {fechaSolicitud:dd/MM/yyyy}",
                    "Solicitud enviada", MessageBoxButton.OK, MessageBoxImage.Information
                );

                // Guardar notificación en base de datos con el ID de la solicitud real
                string titulo = $"Solicitud #{nuevoId} a {nombreProveedor}";
                string mensaje = $"Producto: {descripcion} | Cantidad: {cantidad} | Fecha: {fechaSolicitud:dd/MM/yyyy}";
                NotificacionesService.AgregarSolicitud(titulo, mensaje, nuevoId);

                // Generar respuesta automática del proveedor
                NotificacionesService.GenerarRespuestaAutomatica(nuevoId, nombreProveedor, descripcion, cantidad);

                // Limpieza de campos
                cmbProveedor.SelectedIndex = -1;
                txtProducto.Clear();
                txtDescripcion.Clear();
                txtCantidad.Clear();
                dpFecha.SelectedDate = null;
                cmbCategoria.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al enviar la solicitud:\n{ex.Message}",
                        "Error inesperado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void IrApp_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Te estamos dirigiendo a la app de proveedor",
                            "Redirección",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void IrMenu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Notificaciones_Click(object sender, RoutedEventArgs e)
        {
            Notificaciones notificaciones = new Notificaciones();
            notificaciones.Show();
        }
    }


    public static class NotificacionesService
    {
        // Verificar y crear tabla si no existe
        private static void VerificarTabla()
        {
            try
            {
                Conexion conexion = new Conexion();
                using (var conn = conexion.GetConnection())
                {
                    string checkTable = @"
                        SELECT EXISTS (
                            SELECT FROM information_schema.tables 
                            WHERE table_name = 'notificaciones'
                        )";

                    using (var cmd = new NpgsqlCommand(checkTable, conn))
                    {
                        bool existe = (bool)cmd.ExecuteScalar();
                        
                        if (!existe)
                        {
                            string createTable = @"
                                CREATE TABLE notificaciones (
                                    id_notificacion SERIAL PRIMARY KEY,
                                    tipo VARCHAR(50) NOT NULL CHECK (tipo IN ('Solicitud', 'Respuesta', 'Alerta', 'Información')),
                                    titulo VARCHAR(200) NOT NULL,
                                    mensaje TEXT NOT NULL,
                                    id_usuario INTEGER,
                                    leida BOOLEAN DEFAULT FALSE,
                                    fecha_creacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP
                                )";
                            
                            using (var createCmd = new NpgsqlCommand(createTable, conn))
                            {
                                createCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al verificar tabla: {ex.Message}\n\nAsegúrate de que la tabla 'notificaciones' existe en tu base de datos.", 
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Guardar solicitudes a proveedores en BD usando el ID real de la tabla solicitudes_proveedor
        public static void AgregarSolicitud(string titulo, string mensaje, int idSolicitud)
        {
            try
            {
                Conexion conexion = new Conexion();
                using (var conn = conexion.GetConnection())
                {
                    string mensajeCompleto = $"[SOLICITUD] {titulo}: {mensaje}";
                    
                    // Insertar con el id_solicitud real de la tabla solicitudes_proveedor
                    string query = @"
                        INSERT INTO notificaciones (id_solicitud, mensaje, fecha)
                        VALUES (@idSolicitud, @mensaje, CURRENT_TIMESTAMP)";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idSolicitud", idSolicitud);
                        cmd.Parameters.AddWithValue("@mensaje", mensajeCompleto);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar notificación: {ex.Message}", 
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static void AgregarRespuesta(string titulo, string mensaje, int idSolicitud = 0)
        {
            try
            {
                Conexion conexion = new Conexion();
                using (var conn = conexion.GetConnection())
                {
                    string mensajeCompleto = $"[RESPUESTA] {titulo}: {mensaje}";
                    
                    // Si no se proporciona id_solicitud, obtener el último + 1
                    if (idSolicitud == 0)
                    {
                        string getMaxId = "SELECT COALESCE(MAX(id_solicitud), 0) + 1 FROM notificaciones";
                        using (var maxCmd = new NpgsqlCommand(getMaxId, conn))
                        {
                            idSolicitud = Convert.ToInt32(maxCmd.ExecuteScalar());
                        }
                    }
                    
                    string query = @"
                        INSERT INTO notificaciones (id_solicitud, mensaje, fecha)
                        VALUES (@idSolicitud, @mensaje, CURRENT_TIMESTAMP)";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idSolicitud", idSolicitud);
                        cmd.Parameters.AddWithValue("@mensaje", mensajeCompleto);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar respuesta: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static List<NotificacionItem> ObtenerSolicitudes()
        {
            var lista = new List<NotificacionItem>();
            try
            {
                Conexion conexion = new Conexion();
                using (var conn = conexion.GetConnection())
                {
                    // Mostrar TODAS las notificaciones como solicitudes
                    string query = @"
                        SELECT id_notificacion, id_solicitud, mensaje, fecha
                        FROM notificaciones
                        ORDER BY fecha DESC";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idSolicitud = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                            string mensajeCompleto = reader.GetString(2);
                            
                            lista.Add(new NotificacionItem
                            {
                                IdNotificacion = reader.GetInt32(0),
                                Tipo = "Solicitud",
                                Titulo = idSolicitud > 0 ? $"Solicitud #{idSolicitud}" : "Solicitud a Proveedor",
                                Mensaje = mensajeCompleto.Replace("[SOLICITUD] ", "").Replace("[RESPUESTA] ", ""),
                                Leida = false,
                                Fecha = reader.GetDateTime(3)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar solicitudes: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return lista;
        }

        public static List<NotificacionItem> ObtenerRespuestas()
        {
            var lista = new List<NotificacionItem>();
            try
            {
                Conexion conexion = new Conexion();
                using (var conn = conexion.GetConnection())
                {
                    // Mostrar solo las que tienen el prefijo [RESPUESTA]
                    string query = @"
                        SELECT id_notificacion, id_solicitud, mensaje, fecha
                        FROM notificaciones
                        WHERE mensaje LIKE '[RESPUESTA]%'
                        ORDER BY fecha DESC";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idSolicitud = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                            string mensajeCompleto = reader.GetString(2);
                            
                            lista.Add(new NotificacionItem
                            {
                                IdNotificacion = reader.GetInt32(0),
                                Tipo = "Respuesta",
                                Titulo = idSolicitud > 0 ? $"Respuesta Solicitud #{idSolicitud}" : "Respuesta de Proveedor",
                                Mensaje = mensajeCompleto.Replace("[RESPUESTA] ", ""),
                                Leida = false,
                                Fecha = reader.GetDateTime(3)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar respuestas: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return lista;
        }

        public static void MarcarComoLeida(int idNotificacion)
        {
            // Tu tabla no tiene columna 'leida', esta función no hace nada por ahora
            // Si quieres implementarla, agrega la columna: ALTER TABLE notificaciones ADD COLUMN leida BOOLEAN DEFAULT FALSE;
        }

        public static int ContarNotificacionesNuevas()
        {
            int count = 0;
            try
            {
                Conexion conexion = new Conexion();
                using (var conn = conexion.GetConnection())
                {
                    string query = "SELECT COUNT(*) FROM notificaciones";
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        count = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch { }
            return count;
        }

        // =====================================================================
        // GENERAR RESPUESTA AUTOMÁTICA PARA UNA SOLICITUD ESPECÍFICA
        // =====================================================================
        public static void GenerarRespuestaAutomatica(int idSolicitud, string nombreProveedor, string producto, int cantidad)
        {
            Random random = new Random();
            
            try
            {
                Conexion conexion = new Conexion();
                using (var conn = conexion.GetConnection())
                {
                    // Tipos de respuesta con probabilidades
                    string[] tiposRespuesta = {
                        "CONFIRMADA",
                        "CONFIRMADA",
                        "CONFIRMADA",
                        "EN_REVISION",
                        "PRECIO_ACTUALIZADO"
                    };

                    string tipoRespuesta = tiposRespuesta[random.Next(tiposRespuesta.Length)];
                    string mensajeRespuesta = "";
                    string nuevoEstado = "Pendiente";

                    switch (tipoRespuesta)
                    {
                        case "CONFIRMADA":
                            int diasEntrega = random.Next(3, 15);
                            DateTime fechaEntrega = DateTime.Now.AddDays(diasEntrega);
                            decimal precioUnitario = random.Next(50, 500) + (decimal)(random.NextDouble());
                            decimal total = precioUnitario * cantidad;
                            
                            mensajeRespuesta = $"✅ Solicitud APROBADA por {nombreProveedor}\n" +
                                $"Producto: {producto}\n" +
                                $"Cantidad: {cantidad} unidades\n" +
                                $"Precio unitario: ${precioUnitario:F2}\n" +
                                $"Total: ${total:F2}\n" +
                                $"Fecha estimada de entrega: {fechaEntrega:dd/MM/yyyy}\n" +
                                $"Estado: Aprobado - En preparación";
                            nuevoEstado = "Aprobada";
                            break;

                        case "EN_REVISION":
                            mensajeRespuesta = $"⏳ Solicitud EN REVISIÓN por {nombreProveedor}\n" +
                                $"Producto: {producto}\n" +
                                $"Cantidad solicitada: {cantidad} unidades\n" +
                                $"Mensaje: Estamos verificando disponibilidad en almacén.\n" +
                                $"Tiempo estimado de respuesta: 24-48 horas\n" +
                                $"Estado: En revisión";
                            break;

                        case "PRECIO_ACTUALIZADO":
                            decimal precioNuevo = random.Next(60, 600) + (decimal)(random.NextDouble());
                            decimal totalNuevo = precioNuevo * cantidad;
                            
                            mensajeRespuesta = $"💰 COTIZACIÓN ACTUALIZADA de {nombreProveedor}\n" +
                                $"Producto: {producto}\n" +
                                $"Cantidad: {cantidad} unidades\n" +
                                $"Precio unitario actualizado: ${precioNuevo:F2}\n" +
                                $"Total: ${totalNuevo:F2}\n" +
                                $"Nota: Precio sujeto a confirmación en 48 horas\n" +
                                $"Estado: Cotización enviada";
                            break;
                    }

                    // Insertar respuesta en notificaciones
                    string insertRespuesta = @"
                        INSERT INTO notificaciones (id_solicitud, mensaje, fecha)
                        VALUES (@idSolicitud, @mensaje, CURRENT_TIMESTAMP)";

                    using (var cmdInsert = new NpgsqlCommand(insertRespuesta, conn))
                    {
                        cmdInsert.Parameters.AddWithValue("@idSolicitud", idSolicitud);
                        cmdInsert.Parameters.AddWithValue("@mensaje", $"[RESPUESTA] {mensajeRespuesta}");
                        cmdInsert.ExecuteNonQuery();
                    }

                    // Actualizar estado de la solicitud si fue aprobada
                    if (nuevoEstado == "Aprobada")
                    {
                        string updateSolicitud = @"
                            UPDATE solicitudes_proveedor 
                            SET estado = @estado 
                            WHERE id_solicitud = @idSolicitud";

                        using (var cmdUpdate = new NpgsqlCommand(updateSolicitud, conn))
                        {
                            cmdUpdate.Parameters.AddWithValue("@estado", nuevoEstado);
                            cmdUpdate.Parameters.AddWithValue("@idSolicitud", idSolicitud);
                            cmdUpdate.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // No mostrar error al usuario, solo registrar en consola
                System.Diagnostics.Debug.WriteLine($"Error al generar respuesta automática: {ex.Message}");
            }
        }

        // =====================================================================
        // GENERAR RESPUESTAS SIMULADAS DE PROVEEDORES
        // =====================================================================
        public static int GenerarRespuestasSimuladas()
        {
            int respuestasGeneradas = 0;
            Random random = new Random();
            
            try
            {
                Conexion conexion = new Conexion();
                using (var conn = conexion.GetConnection())
                {
                    // Obtener TODAS las solicitudes que aún no tienen respuesta
                    string querySolicitudes = @"
                        SELECT sp.id_solicitud, sp.id_proveedor, sp.id_producto, 
                               sp.descripcion, sp.cantidad, sp.fecha_solicitud, sp.estado,
                               pr.nombre as nombre_proveedor, 
                               COALESCE(p.nombre, sp.descripcion) as nombre_producto
                        FROM solicitudes_proveedor sp
                        JOIN proveedores pr ON sp.id_proveedor = pr.id_proveedor
                        LEFT JOIN productos p ON sp.id_producto = p.id_producto
                        WHERE sp.estado = 'Pendiente'
                        AND NOT EXISTS (
                            SELECT 1 FROM notificaciones n 
                            WHERE n.id_solicitud = sp.id_solicitud 
                            AND n.mensaje LIKE '[RESPUESTA]%'
                        )
                        ORDER BY sp.fecha_solicitud DESC";

                    var solicitudesPendientes = new List<(int idSolicitud, string proveedor, string producto, int cantidad, DateTime fecha)>();

                    using (var cmd = new NpgsqlCommand(querySolicitudes, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            solicitudesPendientes.Add((
                                reader.GetInt32(0),      // id_solicitud
                                reader.GetString(7),     // nombre_proveedor
                                reader.GetString(8),     // nombre_producto
                                reader.GetInt32(4),      // cantidad
                                reader.GetDateTime(5)    // fecha_solicitud
                            ));
                        }
                    }

                    // Generar respuestas para cada solicitud
                    foreach (var solicitud in solicitudesPendientes)
                    {
                        // Respuestas variadas y realistas
                        string[] tiposRespuesta = {
                            "CONFIRMADA",
                            "CONFIRMADA",
                            "CONFIRMADA",
                            "EN_REVISION",
                            "PRECIO_ACTUALIZADO"
                        };

                        string tipoRespuesta = tiposRespuesta[random.Next(tiposRespuesta.Length)];
                        string mensajeRespuesta = "";
                        string nuevoEstado = "Pendiente";

                        switch (tipoRespuesta)
                        {
                            case "CONFIRMADA":
                                int diasEntrega = random.Next(3, 15);
                                DateTime fechaEntrega = DateTime.Now.AddDays(diasEntrega);
                                decimal precioUnitario = random.Next(50, 500) + (decimal)(random.NextDouble());
                                decimal total = precioUnitario * solicitud.cantidad;
                                
                                mensajeRespuesta = $"✅ Solicitud APROBADA por {solicitud.proveedor}\n" +
                                    $"Producto: {solicitud.producto}\n" +
                                    $"Cantidad: {solicitud.cantidad} unidades\n" +
                                    $"Precio unitario: ${precioUnitario:F2}\n" +
                                    $"Total: ${total:F2}\n" +
                                    $"Fecha estimada de entrega: {fechaEntrega:dd/MM/yyyy}\n" +
                                    $"Estado: Aprobado - En preparación";
                                nuevoEstado = "Aprobada";
                                break;

                            case "EN_REVISION":
                                mensajeRespuesta = $"⏳ Solicitud EN REVISIÓN por {solicitud.proveedor}\n" +
                                    $"Producto: {solicitud.producto}\n" +
                                    $"Cantidad solicitada: {solicitud.cantidad} unidades\n" +
                                    $"Mensaje: Estamos verificando disponibilidad en almacén.\n" +
                                    $"Tiempo estimado de respuesta: 24-48 horas\n" +
                                    $"Estado: En revisión";
                                break;

                            case "PRECIO_ACTUALIZADO":
                                decimal precioNuevo = random.Next(60, 600) + (decimal)(random.NextDouble());
                                decimal totalNuevo = precioNuevo * solicitud.cantidad;
                                
                                mensajeRespuesta = $"💰 COTIZACIÓN ACTUALIZADA de {solicitud.proveedor}\n" +
                                    $"Producto: {solicitud.producto}\n" +
                                    $"Cantidad: {solicitud.cantidad} unidades\n" +
                                    $"Precio unitario actualizado: ${precioNuevo:F2}\n" +
                                    $"Total: ${totalNuevo:F2}\n" +
                                    $"Nota: Precio sujeto a confirmación en 48 horas\n" +
                                    $"Estado: Cotización enviada";
                                break;
                        }

                        // Insertar respuesta en notificaciones
                        string insertRespuesta = @"
                            INSERT INTO notificaciones (id_solicitud, mensaje, fecha)
                            VALUES (@idSolicitud, @mensaje, CURRENT_TIMESTAMP)";

                        using (var cmdInsert = new NpgsqlCommand(insertRespuesta, conn))
                        {
                            cmdInsert.Parameters.AddWithValue("@idSolicitud", solicitud.idSolicitud);
                            cmdInsert.Parameters.AddWithValue("@mensaje", $"[RESPUESTA] {mensajeRespuesta}");
                            cmdInsert.ExecuteNonQuery();
                        }

                        // Actualizar estado de la solicitud si fue aprobada
                        if (nuevoEstado == "Aprobada")
                        {
                            string updateSolicitud = @"
                                UPDATE solicitudes_proveedor 
                                SET estado = @estado 
                                WHERE id_solicitud = @idSolicitud";

                            using (var cmdUpdate = new NpgsqlCommand(updateSolicitud, conn))
                            {
                                cmdUpdate.Parameters.AddWithValue("@estado", nuevoEstado);
                                cmdUpdate.Parameters.AddWithValue("@idSolicitud", solicitud.idSolicitud);
                                cmdUpdate.ExecuteNonQuery();
                            }
                        }

                        respuestasGeneradas++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar respuestas simuladas: {ex.Message}", 
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return respuestasGeneradas;
        }
    }

    public class NotificacionItem
    {
        public int IdNotificacion { get; set; }
        public string Tipo { get; set; }
        public string Titulo { get; set; }
        public string Mensaje { get; set; }
        public bool Leida { get; set; }
        public DateTime Fecha { get; set; }

        public override string ToString()
        {
            string estado = Leida ? "" : "🔴 ";
            return $"{estado}[{Fecha:dd/MM/yyyy HH:mm}] {Titulo}\n   {Mensaje}";
        }
    }


}
