using SpaCitas_del_este.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaCitas_del_este.Data
{
    public class ServicioRepository
    {
        private readonly string _connectionString;

        public ServicioRepository()
        {
            _connectionString = "Server=jorday\\MSSQLSERVER01;Database=SpaCitas_del_este;Trusted_Connection=True;";

        }
        public List<Servicio> GetAll()
        {
            List<Servicio> lista = new List<Servicio>();
            string query = "SELECT ServicioID, Nombre, DuracionMinutos, Precio FROM Servicio";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Servicio
                        {
                            ServicioID = dr.GetInt32(0),
                            Nombre = dr.GetString(1),
                            DuracionMinutos = dr.GetInt32(2),
                            Precio = dr.IsDBNull(3) ? 0 : dr.GetDecimal(3)
                        });
                    }
                }
            }

            return lista;
        }

        public bool Add(Servicio obj)
        {
            string query = @"INSERT INTO Servicio (Nombre, DuracionMinutos, Precio)
                             VALUES (@nom, @dur, @precio)";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@nom", obj.Nombre);
                cmd.Parameters.AddWithValue("@dur", obj.DuracionMinutos);
                cmd.Parameters.AddWithValue("@precio", obj.Precio);

                conn.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }
        public bool Update(Servicio obj)
        {
            string query = @"UPDATE Servicio
                             SET Nombre=@nom, DuracionMinutos=@dur, Precio=@precio
                             WHERE ServicioID=@id";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", obj.ServicioID);
                cmd.Parameters.AddWithValue("@nom", obj.Nombre);
                cmd.Parameters.AddWithValue("@dur", obj.DuracionMinutos);
                cmd.Parameters.AddWithValue("@precio", obj.Precio);

                conn.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }
        public bool Delete(int id)
        {
            string query = "DELETE FROM Servicio WHERE ServicioID=@id";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }
    }
}
























