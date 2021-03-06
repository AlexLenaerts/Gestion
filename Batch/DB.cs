﻿using System;
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
                con.Close();
            }
            catch (Exception)
            {
                Console.WriteLine("Serveur non disponible");
            }
            finally
            {
                Console.WriteLine("Version 1.0");
            }

        }

        public static void CloseDB(SqlConnection con)
        {
            con.Close();
            Console.WriteLine("Disconnected from the server");
        }

        public static void SearchArticle(string param, string value, SqlConnection con)
        {
            con.Open();
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
            con.Close();
        }

        public static void AddToDB(article article, SqlConnection con)
        {
            con.Open();
            string queryStr = $"INSERT INTO STOCK (name, ref, quantity, price) VALUES ('{article.Name}',{article.NumberRef},{article.QuantityStock},{article.SellPrice})";
            SqlCommand cmd = new SqlCommand(queryStr, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }


        public static void RemoveArticleByRef(string reference, SqlConnection con)
        {
            con.Open();
            string queryStr = $"DELETE FROM STOCK where ref= '{reference}'";
            SqlCommand cmd = new SqlCommand(queryStr, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static void ModifyArticle(article article, SqlConnection con)
        {
            con.Open();
            string queryStr = $"Update STOCK set name = '{article.Name}', quantity= {article.QuantityStock}, price= {article.SellPrice} where ref= {article.NumberRef}";
            SqlCommand cmd = new SqlCommand(queryStr, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static void ShowDB(SqlConnection con)
        {
            con.Open();
            string queryStr = "SELECT * from STOCK";
            SqlCommand cmd = new SqlCommand(queryStr, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Console.WriteLine(String.Format("{0} {1} {2} {3}", dr[1], dr[2], dr[3], dr[4]));
            }
            dr.Close();
            con.Close();

        }
        public static List<article> DBTOLIST(SqlConnection con)
        {
            con.Open();
            string queryStr = "SELECT * from STOCK";
            SqlCommand cmd = new SqlCommand(queryStr, con);
            SqlDataReader dr = cmd.ExecuteReader();
            List<article> stock = new List<article>();
            while (dr.Read())
            {
                stock.Add(new article(Convert.ToInt32(dr[2]), dr[1].ToString(), Convert.ToDouble(dr[4]), Convert.ToInt32(dr[3])));
            }
            dr.Close();
            con.Close();
            return stock;
        }
    }
}

