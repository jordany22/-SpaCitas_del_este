using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaCitas_del_este.Models
{
    public class Cita
    {
        public int CitaID { get; set; }
        public int PacienteID { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime Hora { get; set; }
        public int ServicioID { get; set; }
        public int TerapeutaID { get; set; }
        public string Observaciones { get; set; }

        public int Duracion { get; set; }
        public string Estado { get; set; }
        public TimeSpan TiempoRestante
        {
            get
            {
                DateTime inicio = Fecha.Date.Add(Hora.TimeOfDay);
                return inicio - DateTime.Now;
            }
        }



    }


}

