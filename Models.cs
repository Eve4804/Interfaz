using System;
using System.Collections.Generic;

namespace Interfaz
{
    // Modelos de datos para la aplicación
    // Estos modelos se mapearán a las tablas de PostgreSQL

    public class Cliente
    {
        public int IdCliente { get; set; }
        public string RFC { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string DireccionFiscal { get; set; }
        public string DireccionEnvio { get; set; }
        public string MetodoPago { get; set; }
        public DateTime FechaAlta { get; set; }
        public bool Activo { get; set; } = true;
    }

    public class Categoria
    {
        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; } = true;
    }

    public class Producto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int? IdCategoria { get; set; }
        public string ImagenUrl { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime FechaCreacion { get; set; }
    }

    public class Inventario
    {
        public int IdInventario { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public string Estado { get; set; }
        public string Ubicacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }

    public class Proveedor
    {
        public int IdProveedor { get; set; }
        public string Nombre { get; set; }
        public string Contacto { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public bool Activo { get; set; } = true;
    }

    public class SolicitudProveedor
    {
        public int IdSolicitud { get; set; }
        public int IdProveedor { get; set; }
        public int IdProducto { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string Estado { get; set; }
    }

    public class RegistroPago
    {
        public int IdPago { get; set; }
        public string Proveedor { get; set; }
        public int Solicitud { get; set; }
        public string Monto { get; set; }
        public string FormaPago { get; set; }
        public string FechaPago { get; set; }
        public string Estado { get; set; }
    }
    public class Venta
    {
        public int IdVenta { get; set; }
        public DateTime Fecha { get; set; }
        public string ClienteNombre { get; set; }
        public string ProductoNombre { get; set; }
        public int Cantidad { get; set; }
        public string DireccionFiscal { get; set; }
        public string DireccionEnvio { get; set; }
        public string Direccion { get; set; } // Dirección que se usará (fiscal o envío)
        public decimal Total { get; set; }
        public string Estado { get; set; }
        public string MetodoPago { get; set; }
        public DateTime? FechaEntregaEstimada { get; set; }
        public DateTime? FechaEntregaReal { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }



    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }
        public string NombreCompleto { get; set; }
        public string Email { get; set; }
        public string Rol { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime? UltimoAcceso { get; set; }
    }
    public class DetalleVenta
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Total => Cantidad * PrecioUnitario;
    }
    public class Notificacion
    {
        public int IdNotificacion { get; set; }
        public string Tipo { get; set; }
        public string Titulo { get; set; }
        public string Mensaje { get; set; }
        public int? IdUsuario { get; set; }
        public bool Leida { get; set; }
        public DateTime FechaCreacion { get; set; }
    }

    public class HistorialInventario
    {
        public int IdHistorial { get; set; }
        public int IdProducto { get; set; }
        public int CantidadAnterior { get; set; }
        public int CantidadNueva { get; set; }
        public string TipoMovimiento { get; set; }
        public int? IdUsuario { get; set; }
        public string Motivo { get; set; }
        public DateTime FechaMovimiento { get; set; }
    }
}
