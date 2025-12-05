using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SpaCitas_del_este.Data
{
    public static class Database
    {
        public static SqlConnection GetConnection()
        {
            string cs = ConfigurationManager.ConnectionStrings["SpaCitas_del_este"].ConnectionString;
            if (string.IsNullOrEmpty(cs))
                throw new InvalidOperationException("No se encontró la cadena de conexión. Verifica el nombre en App.config.");

            return new SqlConnection(cs);
        }
    }



}
