using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Gestion_du_stock
{
    class DB
    {
        SqlConnection con = new SqlConnection();

        public void connectDB()
        {
            con.ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Alexandre\Gestion_du_stock\Gestion_du_stock\Database1.mdf; Integrated Security = True";
            try
            {
                con.Open();
                Console.WriteLine("Connection opened");
            }
            catch
            {
                Console.WriteLine("Serveur non disponible");

            }
        }

        public void showDB()
        {
            var result = new StringBuilder();

            string queryStr = "select name from Stock where ref = '123'";
            SqlCommand cmd = new SqlCommand(queryStr, con);
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Append(reader.GetString(0));
                }
            }

            Console.WriteLine(result.ToString());
        }

        public void CloseDB()
        {
            con.Close();
            Console.WriteLine("Connection closed");
        }
    }
}

