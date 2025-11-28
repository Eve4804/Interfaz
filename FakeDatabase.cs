using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaz
{
    public static class FakeDatabase
    {
        // ===== CLIENTES =====
        public static List<Cliente> Clientes = new List<Cliente>()
        {
            new Cliente
            {
                IdCliente = 1,
                RFC = "ABC123",
                Nombre = "Juan Pérez",
                Tipo = "Regular",
                Email = "juan@example.com",
                Telefono = "555-1234",
                DireccionFiscal = "Calle Falsa 123",
                DireccionEnvio = "Av. Siempre Viva 742",
                Activo = true
            },

            new Cliente
            {
                IdCliente = 2,
                RFC = "XYZ987",
                Nombre = "María López",
                Tipo = "Premium",
                Email = "maria@example.com",
                Telefono = "555-9876",
                DireccionFiscal = "Centro 505",
                DireccionEnvio = "Centro 505",
                Activo = false
            }
        };

        // ===== VENTAS =====
        public static List<Venta> Ventas = new List<Venta>()
{
    new Venta
    {
        IdVenta = 101,
        Fecha = new DateTime(2024, 1, 15),
        IdCliente = 1,
        ClienteNombre = "Juan Pérez",
        Estado = "Confirmada",
        MetodoPago = "Tarjeta",

        Items = new List<VentaItem>()
        {
            new VentaItem
            {
                IdProducto = 10,
                Producto = "Caja de cartón",
                Cantidad = 10,
                Precio = 25
            }
        },

        Total = 10 * 25
    },

    new Venta
    {
        IdVenta = 102,
        Fecha = new DateTime(2024, 1, 18),
        IdCliente = 2,
        ClienteNombre = "María López",
        Estado = "Pendiente",
        MetodoPago = "Efectivo",

        Items = new List<VentaItem>()
        {
            new VentaItem
            {
                IdProducto = 20,
                Producto = "Plástico burbuja",
                Cantidad = 5,
                Precio = 36
            }
        },

        Total = 5 * 36
    }
};

        // ===== MÉTODOS PARA CLIENTES =====

        public static Cliente BuscarClientePorNombre(string nombre)
        {
            return Clientes.FirstOrDefault(c =>
                c.Nombre.ToLower().Contains(nombre.ToLower()));
        }

        public static Cliente BuscarClientePorId(int id)
        {
            return Clientes.FirstOrDefault(c => c.IdCliente == id);
        }

        public static void ActivarCliente(int id)
        {
            var cliente = BuscarClientePorId(id);
            if (cliente != null)
                cliente.Activo = true;
        }

        public static void DesactivarCliente(int id)
        {
            var cliente = BuscarClientePorId(id);
            if (cliente != null)
                cliente.Activo = false;
        }

        // ===== MÉTODOS PARA VENTAS =====

        public static List<Venta> BuscarVentas(string cliente, string producto)
        {
            return Ventas.Where(v =>
                v.ClienteNombre.ToLower().Contains(cliente.ToLower()) &&
                v.Items.Any(i => i.Producto.ToLower().Contains(producto.ToLower()))
            ).ToList();
        }
    }
}
