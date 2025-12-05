using SpaCitas_del_este.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaCitas_del_este.Data
{
    public class TerapeutaRepository
    {
        private readonly string _connectionString;

        public TerapeutaRepository()
        {
            _connectionString = "Server=jorday\\MSSQLSERVER01;Database=SpaCitas_del_este;Trusted_Connection=True;";


        }
        public List<Terapeuta> GetAll()
        {
            List<Terapeuta> lista = new List<Terapeuta>();
            string query = "SELECT TerapeutaID, Nombre, Especialidad FROM Terapeuta";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Terapeuta
                        {
                            TerapeutaID = dr.GetInt32(0),
                            Nombre = dr.GetString(1),
                            Especialidad = dr.GetString(2)
                        });
                    }
                }
            }
            return lista;
        }
        public bool Add(Terapeuta obj)
        {
            string query = @"INSERT INTO Terapeuta (Nombre, Especialidad)
                             VALUES (@nom, @esp)";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@nom", obj.Nombre);
                cmd.Parameters.AddWithValue("@esp", obj.Especialidad);

                conn.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }

        public bool Update(Terapeuta obj)
        {
            string query = @"UPDATE Terapeuta
                             SET Nombre=@nom, Especialidad=@esp
                             WHERE TerapeutaID=@id";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", obj.TerapeutaID);
                cmd.Parameters.AddWithValue("@nom", obj.Nombre);
                cmd.Parameters.AddWithValue("@esp", obj.Especialidad);

                conn.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }

        public bool Delete(int id)
        {
            string query = "DELETE FROM Terapeuta WHERE TerapeutaID=@id";

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


























