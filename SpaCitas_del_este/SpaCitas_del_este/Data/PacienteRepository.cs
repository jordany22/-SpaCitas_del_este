using SpaCitas_del_este.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaCitas_del_este.Data
{
    public class PacienteRepository
    {
        private readonly string _connectionString;

        public PacienteRepository()
        {
            _connectionString = "Server=jorday\\MSSQLSERVER01;Database=SpaCitas_del_este;Trusted_Connection=True;";
        }

        public List<Paciente> GetAll()
        {
            List<Paciente> lista = new List<Paciente>();
            string query = "SELECT PacienteID, Nombre, Telefono, Email FROM Paciente";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Paciente
                        {
                            PacienteID = dr.GetInt32(0),
                            Nombre = dr.GetString(1),
                            Telefono = dr.GetString(2),
                            Email = dr.GetString(3)
                        });
                    }
                }
            }
            return lista;
        }

        public bool Add(Paciente obj)
        {
            string query = @"INSERT INTO Paciente (Nombre, Telefono, Email)
                         VALUES (@nom, @tel, @mail)";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@nom", obj.Nombre);
                cmd.Parameters.AddWithValue("@tel", obj.Telefono);
                cmd.Parameters.AddWithValue("@mail", obj.Email);

                conn.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }
        public bool Update(Paciente obj)
        {
            string query = @"UPDATE Paciente
                         SET Nombre=@nom, Telefono=@tel, Email=@mail
                         WHERE PacienteID=@id";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", obj.PacienteID);
                cmd.Parameters.AddWithValue("@nom", obj.Nombre);
                cmd.Parameters.AddWithValue("@tel", obj.Telefono);
                cmd.Parameters.AddWithValue("@mail", obj.Email);

                conn.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }
        public bool Delete(int id)
        {
            string query = "DELETE FROM Paciente WHERE PacienteID=@id";

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








