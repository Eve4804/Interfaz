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

namespace Interfaz
{
    /// <summary>
    /// Lógica de interacción para Notificaciones.xaml
    /// </summary>
    public partial class Notificaciones : Window
    {
        public static class NotificacionesService
        {
            public static List<string> Solicitudes { get; } = new List<string>();
            public static List<string> Respuestas { get; } = new List<string>();

            public static void AgregarSolicitud(string mensaje)
            {
                Solicitudes.Add(mensaje);
            }

            public static void AgregarRespuesta(string mensaje)
            {
                Respuestas.Add(mensaje);
            }
        }

        public Notificaciones()
        {
            InitializeComponent();

            // Cargar solicitudes reales
            foreach (var solicitud in NotificacionesService.Solicitudes)
            {
                lstSolicitudes.Items.Add(solicitud);
            }

            // Cargar respuestas reales (si las agregas en algún flujo)
            foreach (var respuesta in NotificacionesService.Respuestas)
            {
                lstRespuestas.Items.Add(respuesta);
            }



        }
        private void lstSolicitudes_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lstSolicitudes.SelectedItem == null) return;

            string solicitud = lstSolicitudes.SelectedItem.ToString();
            MessageBox.Show($"Detalle de la solicitud:\n\n{solicitud}\n\nEstado: Pendiente de respuesta.",
                            "Detalle de solicitud",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
        }

        private void lstRespuestas_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lstRespuestas.SelectedItem == null) return;

            string respuesta = lstRespuestas.SelectedItem.ToString();

            if (respuesta.Contains("aceptó"))
            {
                MessageBox.Show("El proveedor aceptó tu solicitud. Tu pedido llegará el 30/11/2025.",
                                "Respuesta del proveedor",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }
            else if (respuesta.Contains("rechazó"))
            {
                MessageBox.Show("El proveedor rechazó tu pedido. Contacta soporte o intenta con otro proveedor.",
                                "Respuesta del proveedor",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
            }
        }

        private void Cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
