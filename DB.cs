using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Gestion_du_stock
{
    public class DB
    {
        public static void ConnectDB(SqlConnection con)
        {
            try
            {
                con.Open();
                Console.WriteLine("Connected to the server");
            }
            catch
            {
                Console.WriteLine("Serveur non disponible");
            }
        }

        public static void CloseDB(SqlConnection con)
        {
            con.Close();
            Console.WriteLine("Disconnected from the server");
        }

        public static void SearchArticle(string param, string value, SqlConnection con)
        {
            string queryStr = $"SELECT * from STOCK where {param}= '{value}'";
            SqlCommand cmd = new SqlCommand(queryStr, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Console.WriteLine(String.Format("{0} {1} {2} {3}", dr[1], dr[2], dr[3], dr[4]));
                }
            }
            else
            {
                Console.WriteLine("No found");
            }
            dr.Close();
        }

        public static void AddToDB(article article, SqlConnection con)
        {
            string queryStr = $"INSERT INTO STOCK (name, ref, quantity, price) VALUES ('{article.Name}',{article.NumberRef},{article.QuantityStock},{article.SellPrice})";
            SqlCommand cmd = new SqlCommand(queryStr, con);
            cmd.ExecuteNonQuery();

        }

        public static void RemoveArticleByRef(string reference, SqlConnection con)
        {
            string queryStr = $"DELETE FROM STOCK where ref= '{reference}'";
            SqlCommand cmd = new SqlCommand(queryStr, con);
            cmd.ExecuteNonQuery();
        }

        public static void ModifyArticle(article article, SqlConnection con)
        {
            string queryStr = $"Update STOCK set name = '{article.Name}', quantity= {article.QuantityStock}, price= {article.SellPrice} where ref= {article.NumberRef}";
            SqlCommand cmd = new SqlCommand(queryStr, con);
            cmd.ExecuteNonQuery();
        }

        public static void ShowDB(SqlConnection con)
        {
            string queryStr = "SELECT * from STOCK";
            SqlCommand cmd = new SqlCommand(queryStr, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Console.WriteLine(String.Format("{0} {1} {2} {3}", dr[1], dr[2], dr[3], dr[4]));
            }
            dr.Close();
        }
        public static List<article> DBTOLIST(SqlConnection con)
        {
            string queryStr = "SELECT * from STOCK";
            SqlCommand cmd = new SqlCommand(queryStr, con);
            SqlDataReader dr = cmd.ExecuteReader();
            List<article> stock = new List<article>();
            while (dr.Read())
            {
                stock.Add(new article(Convert.ToInt32(dr[2]), dr[1].ToString(), Convert.ToDouble(dr[4]), Convert.ToInt32(dr[3])));
            }
            dr.Close();
            return stock;
        }
    }
}

