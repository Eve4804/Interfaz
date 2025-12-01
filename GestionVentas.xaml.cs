using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Interfaz
{
    public partial class GestionVentas : Window
    {
        private List<Venta> todasLasVentas;
        private Venta ventaSeleccionada;

        public GestionVentas()
        {
            InitializeComponent();
            CargarDatosEjemplo();
            ActualizarEstadisticas();
        }

        private void CargarDatosEjemplo()
        {
            // TODO: Reemplazar con datos de la base de datos
            todasLasVentas = new List<Venta>
            {
                // Pedidos Nuevos
                new Venta { IdVenta = 1001, Fecha = DateTime.Now.AddHours(-2), ClienteNombre = "Juan Pérez", Total = 1500.00m, MetodoPago = "Tarjeta", Estado = "Pendiente" },
                new Venta { IdVenta = 1002, Fecha = DateTime.Now.AddHours(-1), ClienteNombre = "María García", Total = 2300.50m, MetodoPago = "Transferencia", Estado = "Pendiente" },
                new Venta { IdVenta = 1003, Fecha = DateTime.Now.AddMinutes(-30), ClienteNombre = "Carlos López", Total = 890.00m, MetodoPago = "Efectivo", Estado = "Pendiente" },
                new Venta { IdVenta = 1004, Fecha = DateTime.Now.AddMinutes(-15), ClienteNombre = "Ana Martínez", Total = 3200.00m, MetodoPago = "Crédito", Estado = "Pendiente" },

                // Pedidos Confirmados (Pendientes de entrega)
                new Venta { IdVenta = 1005, Fecha = DateTime.Now.AddDays(-1), ClienteNombre = "Roberto Sánchez", Total = 1800.00m, MetodoPago = "Tarjeta", Estado = "Confirmada" },
                new Venta { IdVenta = 1006, Fecha = DateTime.Now.AddDays(-1), ClienteNombre = "Laura Rodríguez", Total = 2100.00m, MetodoPago = "Transferencia", Estado = "En tránsito" },
                new Venta { IdVenta = 1007, Fecha = DateTime.Now.AddDays(-2), ClienteNombre = "Pedro Hernández", Total = 950.00m, MetodoPago = "Efectivo", Estado = "Confirmada" },

                // Pedidos Cancelados
                new Venta { IdVenta = 1008, Fecha = DateTime.Now.AddDays(-3), ClienteNombre = "Sofía Torres", Total = 1200.00m, MetodoPago = "Tarjeta", Estado = "Cancelada", Notas = "Cliente canceló" },
                new Venta { IdVenta = 1009, Fecha = DateTime.Now.AddDays(-5), ClienteNombre = "Diego Ramírez", Total = 3500.00m, MetodoPago = "Crédito", Estado = "Cancelada", Notas = "Sin stock" },
                new Venta { IdVenta = 1010, Fecha = DateTime.Now.AddDays(-7), ClienteNombre = "Carmen Flores", Total = 780.00m, MetodoPago = "Efectivo", Estado = "Cancelada", Notas = "Pago rechazado" }
            };

            ActualizarListas();
        }

        private void ActualizarListas()
        {
            // Filtrar por estado
            var nuevos = todasLasVentas.Where(v => v.Estado == "Pendiente").ToList();
            var pendientes = todasLasVentas.Where(v => v.Estado == "Confirmada" || v.Estado == "En tránsito").ToList();
            var cancelados = todasLasVentas.Where(v => v.Estado == "Cancelada").ToList();

            // Asignar a los DataGrids
            DgPedidosNuevos.ItemsSource = nuevos;
            DgPedidosPendientes.ItemsSource = pendientes;
            DgPedidosCancelados.ItemsSource = cancelados;

            // Actualizar estadísticas
            TxtTotalNuevos.Text = nuevos.Count.ToString();
            TxtTotalPendientes.Text = pendientes.Count.ToString();
            TxtTotalCancelados.Text = cancelados.Count.ToString();
        }

        private void ActualizarEstadisticas()
        {
            if (todasLasVentas == null) return;

            var nuevos = todasLasVentas.Count(v => v.Estado == "Pendiente");
            var pendientes = todasLasVentas.Count(v => v.Estado == "Confirmada" || v.Estado == "En tránsito");
            var cancelados = todasLasVentas.Count(v => v.Estado == "Cancelada");

            TxtTotalNuevos.Text = nuevos.ToString();
            TxtTotalPendientes.Text = pendientes.ToString();
            TxtTotalCancelados.Text = cancelados.ToString();
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string criterio = TxtBuscar.Text.ToLower().Trim();

            if (string.IsNullOrWhiteSpace(criterio))
            {
                ActualizarListas();
                return;
            }

            // Filtrar ventas
            var resultados = todasLasVentas.Where(v =>
                v.IdVenta.ToString().Contains(criterio) ||
                v.ClienteNombre.ToLower().Contains(criterio)
            ).ToList();

            // Actualizar listas con resultados
            var nuevos = resultados.Where(v => v.Estado == "Pendiente").ToList();
            var pendientes = resultados.Where(v => v.Estado == "Confirmada" || v.Estado == "En tránsito").ToList();
            var cancelados = resultados.Where(v => v.Estado == "Cancelada").ToList();

            DgPedidosNuevos.ItemsSource = nuevos;
            DgPedidosPendientes.ItemsSource = pendientes;
            DgPedidosCancelados.ItemsSource = cancelados;
        }

        private void BtnNuevaVenta_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "Funcionalidad de Nueva Venta\n\n" +
                "Aquí se abrirá un formulario para crear una nueva venta:\n" +
                "- Seleccionar cliente\n" +
                "- Agregar productos\n" +
                "- Calcular total\n" +
                "- Registrar venta",
                "Nueva Venta",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void BtnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var venta = button?.DataContext as Venta;

            if (venta == null) return;

            var resultado = MessageBox.Show(
                $"¿Confirmar el pedido #{venta.IdVenta}?\n\n" +
                $"Cliente: {venta.ClienteNombre}\n" +
                $"Total: {venta.Total:C}\n\n" +
                "El pedido pasará a estado 'Confirmada'",
                "Confirmar Pedido",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (resultado == MessageBoxResult.Yes)
            {
                venta.Estado = "Confirmada";
                ActualizarListas();
                MessageBox.Show(
                    $"✓ Pedido #{venta.IdVenta} confirmado exitosamente",
                    "Éxito",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var venta = button?.DataContext as Venta;

            if (venta == null) return;

            var resultado = MessageBox.Show(
                $"¿Cancelar el pedido #{venta.IdVenta}?\n\n" +
                $"Cliente: {venta.ClienteNombre}\n" +
                $"Total: {venta.Total:C}\n\n" +
                "Esta acción no se puede deshacer",
                "Cancelar Pedido",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (resultado == MessageBoxResult.Yes)
            {
                venta.Estado = "Cancelada";
                venta.Notas = "Cancelado manualmente";
                ActualizarListas();
                MessageBox.Show(
                    $"✗ Pedido #{venta.IdVenta} cancelado",
                    "Pedido Cancelado",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }

        private void BtnVerDetalles_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var venta = button?.DataContext as Venta;

            if (venta == null) return;

            MessageBox.Show(
                $"📋 DETALLES DEL PEDIDO #{venta.IdVenta}\n\n" +
                $"Cliente: {venta.ClienteNombre}\n" +
                $"Fecha: {venta.Fecha:dd/MM/yyyy HH:mm}\n" +
                $"Estado: {venta.Estado}\n" +
                $"Método de Pago: {venta.MetodoPago}\n" +
                $"Subtotal: {venta.Subtotal:C}\n" +
                $"Impuestos: {venta.Impuestos:C}\n" +
                $"Total: {venta.Total:C}\n" +
                (string.IsNullOrEmpty(venta.Notas) ? "" : $"\nNotas: {venta.Notas}"),
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
    }
}
