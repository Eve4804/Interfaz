using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaz
{
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

    public class Producto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
    }

    public class VentaItem
    {
        public int IdProducto { get; set; }
        public string Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Subtotal => Cantidad * Precio;
    }

    public class Venta
    {
        public int IdVenta { get; set; }
        public DateTime Fecha { get; set; }
        public int IdCliente { get; set; }
        public string ClienteNombre { get; set; }
        public List<VentaItem> Items { get; set; } = new List<VentaItem>();
        public decimal Total { get; set; }
        public string Estado { get; set; } // pendiente, confirmada, cancelada, en tránsito, entregado
        public string MetodoPago { get; set; }
    }
    internal class Models
    {
    }
}
