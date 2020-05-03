using log4net.Config;
using log4net.Core;
using System;
using System.Data.SqlClient;
using System.IO;

namespace Gestion_du_stock
{
    class Program
    {

        static void Main(string[] args)
        {
            //Connection à la DB
            string ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Alexandre\Gestion_du_stock\Gestion_du_stock\Database1.mdf; Integrated Security = True";
            SqlConnection con = new SqlConnection(ConnectionString);
            DB.ConnectDB(con);
            //var configFile = Directory.GetCurrentDirectory() + @"\log4net.config";
            //BasicConfigurator.Configure();

            var task = new loggingService();


            //Stock temporaire
            //List<article> Stock = new List<article>();

            string decision;
            do
            {
                task.Run("Démarrage de l'application", Level.Info);

                Console.WriteLine();
                Console.WriteLine("Que voulez-vous faire ?");
                Console.WriteLine("  1- Recherche d'un article");
                Console.WriteLine("  2- Ajouter un article");
                Console.WriteLine("  3- Supprimer un article");
                Console.WriteLine("  4- Modifier un article");
                Console.WriteLine("  5- Rechercher un article par le nom");
                Console.WriteLine("  6- Rechercher un article par son prix");
                Console.WriteLine("  7- Affichage du stock");
                Console.WriteLine("  8- Quitter");
                decision = Console.ReadLine();
                switch (decision)
                {
                    case "1":
                        Console.WriteLine("Recherche d'un article");
                        Console.WriteLine("Introduisez la référence de l'article");
                        string articleREF = Console.ReadLine();
                        //ManageStock.SearchArticleByRef(Stock, Int32.Parse(articleREF));
                        Console.WriteLine();
                        DB.SearchArticle("ref", articleREF, con);
                        break;

                    case "2":
                        Console.WriteLine("Ajouter un article");
                        Console.WriteLine("Introduisez la référence de l'article");
                        articleREF = Console.ReadLine();
                        Console.WriteLine("Introduisez le nom de l'article");
                        string articleNAME = Console.ReadLine();
                        Console.WriteLine("Introduisez le prix de l'article");
                        string articlePRICE = Console.ReadLine();
                        Console.WriteLine("Introduisez la quantité en stock de cet article");
                        string articleQUANTITY = Console.ReadLine();
                        article newArticle = new article(Int32.Parse(articleREF), articleNAME, Convert.ToDouble(articlePRICE), Int32.Parse(articleQUANTITY));
                        //ManageStock.AddArticle(Stock, newArticle);
                        Console.WriteLine();
                        DB.AddToDB(newArticle, con);
                        break;

                    case "3":
                        Console.WriteLine("Supprimer un article");
                        Console.WriteLine("Introduisez la référence de l'article");
                        articleREF = Console.ReadLine();
                        DB.RemoveArticleByRef(articleREF, con);
                        //ManageStock.RemoveArticleByRef(Stock, Int32.Parse(articleREF));
                        break;

                    case "4":
                        Console.WriteLine("Modifier un article");
                        Console.WriteLine("Introduisez la référence de l'article");
                        articleREF = Console.ReadLine();
                        Console.WriteLine("Introduisez le nom de l'article");
                        articleNAME = Console.ReadLine();
                        Console.WriteLine("Introduisez le prix de l'article");
                        articlePRICE = Console.ReadLine();
                        Console.WriteLine("Introduisez la quantité en stock de cet article");
                        articleQUANTITY = Console.ReadLine();
                        newArticle = new article(Int32.Parse(articleREF), articleNAME, Convert.ToDouble(articlePRICE), Int32.Parse(articleQUANTITY));
                        //ManageStock.ModifyArticle(Stock, Int32.Parse(articleREF), newArticle);
                        Console.WriteLine();
                        DB.ModifyArticle(newArticle, con);
                        break;

                    case "5":
                        Console.WriteLine("Rechercher un article par le nom");
                        Console.WriteLine("Introduisez le nom de l'article");
                        articleNAME = Console.ReadLine();
                        //ManageStock.SearchArticleByName(Stock, articleNAME);
                        Console.WriteLine();
                        DB.SearchArticle("name", articleNAME, con);
                        break;

                    case "6":
                        Console.WriteLine("Rechercher un article par son prix");
                        Console.WriteLine("Introduisez le prix de l'article");
                        articlePRICE = Console.ReadLine();
                        //ManageStock.SearchArticleByprice(Stock, Convert.ToDouble(articlePRICE));
                        Console.WriteLine();
                        DB.SearchArticle("price", articlePRICE, con);
                        break;

                    case "7":
                        Console.WriteLine("Affichage du stock");
                        //ManageStock.DisplayAllArticle(Stock);
                        Console.WriteLine();
                        DB.ShowDB(con);
                        break;

                    case "8":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("Choix invalide");
                        break;
                }
            }
            while (decision != "8");
            Console.ReadKey();
            DB.CloseDB(con);
        }
    }
}
