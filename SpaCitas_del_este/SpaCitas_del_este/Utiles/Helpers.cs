using SpaCitas_del_este.Utiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaCitas_del_este.Utiles
{
    public class Helpers
    {
        public static void ShowMessage(string msg, string title = "Información")
        {
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void ShowError(string msg)
        {
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static bool ConfirmAction(string message = "¿Está seguro de realizar esta acción?")
        {
            return MessageBox.Show(message, "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        public static string DescargarArchivoCSV(string name = "Exportacion.csv")
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv",
                FileName = name
            };

            return sfd.ShowDialog() == DialogResult.OK ? sfd.FileName : null;
        }
        public static class CitaHelper
        {
            public static int ObtenerDuracionPorServicio(int servicioID)
            {
                // Simula consulta a la base de datos
                switch (servicioID)
                {
                    case 1: return 30; // Masaje
                    case 2: return 60; // Facial
                    case 3: return 45; // Terapia
                    default: return 0;
                }
            }

            public static TimeSpan CalcularTiempoRestante(DateTime fecha, DateTime hora)
            {
                DateTime fechaHoraCita = fecha.Date + hora.TimeOfDay;
                return fechaHoraCita - DateTime.Now;
            }

            public static string CalcularEstado(DateTime fecha, DateTime hora, int duracionMinutos)
            {
                DateTime inicio = fecha.Date + hora.TimeOfDay;
                DateTime fin = inicio.AddMinutes(duracionMinutos);
                DateTime ahora = DateTime.Now;

                if (ahora < inicio) return "Vigente";
                else if (ahora >= inicio && ahora <= fin) return "En proceso";
                else return "Finalizado";
            }
        }
    }

}
    

