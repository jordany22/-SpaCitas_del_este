using SpaCitas_del_este.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SpaCitas_del_este.Utiles.Helpers;

namespace SpaCitas_del_este.Data
{
    public class CitaRepository
    {
        private readonly string _connectionString;

        public CitaRepository()
        {
            _connectionString = "Server=jorday\\MSSQLSERVER01;Database=SpaCitas_del_este;Trusted_Connection=True;";
        }

        public List<Cita> GetAll()
        {
            List<Cita> lista = new List<Cita>();
            string query = "SELECT CitaID, PacienteID, Fecha, Hora, ServicioID, TerapeutaID, Duracion, Estado FROM Cita";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Cita
                        {
                            CitaID = Convert.ToInt32(dr["CitaID"]),
                            PacienteID = Convert.ToInt32(dr["PacienteID"]),
                            Fecha = Convert.ToDateTime(dr["Fecha"]),
                            Hora = DateTime.Today.Add((TimeSpan)dr["Hora"]),
                            ServicioID = Convert.ToInt32(dr["ServicioID"]),
                            TerapeutaID = Convert.ToInt32(dr["TerapeutaID"]),
                            Duracion = CitaHelper.ObtenerDuracionPorServicio(Convert.ToInt32(dr["ServicioID"])),
                            Estado = CitaHelper.CalcularEstado(Convert.ToDateTime(dr["Fecha"]), DateTime.Today.Add((TimeSpan)dr["Hora"]), CitaHelper.ObtenerDuracionPorServicio(Convert.ToInt32(dr["ServicioID"])))



                        });
                    }
                }
            }
            return lista;
        }

        public bool Add(Cita cita)
        {
            string query = @"INSERT INTO Cita 
                            (PacienteID, Fecha, Hora, ServicioID, TerapeutaID, Duracion, Estado)
                            VALUES (@PacienteID, @Fecha, @Hora, @ServicioID, @TerapeutaID, @Duracion, @Estado)";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@PacienteID", cita.PacienteID);
                cmd.Parameters.AddWithValue("@Fecha", cita.Fecha);
                cmd.Parameters.AddWithValue("@Hora", cita.Hora);
                cmd.Parameters.AddWithValue("@ServicioID", cita.ServicioID);
                cmd.Parameters.AddWithValue("@TerapeutaID", cita.TerapeutaID);
                cmd.Parameters.AddWithValue("@Duracion", cita.Duracion);
                cmd.Parameters.AddWithValue("@Estado", cita.Estado);

                conn.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }
        public bool Update(Cita cita)
        {
            string query = @"UPDATE Cita SET 
                                PacienteID = @PacienteID,
                                Fecha = @Fecha,
                                Hora = @Hora,
                                ServicioID = @ServicioID,
                                TerapeutaID = @TerapeutaID,
                                Duracion = @Duracion,
                                Estado = @Estado
                             WHERE CitaID = @CitaID";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@CitaID", cita.CitaID);
                cmd.Parameters.AddWithValue("@PacienteID", cita.PacienteID);
                cmd.Parameters.AddWithValue("@Fecha", cita.Fecha);
                cmd.Parameters.AddWithValue("@Hora", cita.Hora);
                cmd.Parameters.AddWithValue("@ServicioID", cita.ServicioID);
                cmd.Parameters.AddWithValue("@TerapeutaID", cita.TerapeutaID);
                cmd.Parameters.AddWithValue("@Duracion", cita.Duracion);
                cmd.Parameters.AddWithValue("@Estado", cita.Estado);

                conn.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }

        public bool Delete(int idCita)
        {
            string query = "DELETE FROM Cita WHERE CitaID = @CitaID";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@CitaID", idCita);

                conn.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }
    }
}














