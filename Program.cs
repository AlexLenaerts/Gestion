using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Gestion_du_stock
{
    class Program
    {

        static void Main(string[] args)
        {
            DB db = new DB();
            db.connectDB();

            article article1 = new article(123, "article1", 12.3, 1);
            List<article> Stock = new List<article>(4);
            ManageStock manageStock = new ManageStock();
            Stock.Add(article1);
            string decision;
            do
            {
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
                        manageStock.SearchArticleByRef(Stock, Int32.Parse(articleREF));
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
                        article ArticleName = new article(Int32.Parse(articleREF), articleNAME, Convert.ToDouble(articlePRICE), Int32.Parse(articleQUANTITY));
                        manageStock.AddArticle(Stock, ArticleName);
                        break;

                    case "3":
                        Console.WriteLine("Supprimer un article");
                        Console.WriteLine("Introduisez la référence de l'article");
                        articleREF = Console.ReadLine();
                        manageStock.RemoveArticleByRef(Stock, Int32.Parse(articleREF));
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
                        ArticleName = new article(Int32.Parse(articleREF), articleNAME, Convert.ToDouble(articlePRICE), Int32.Parse(articleQUANTITY));
                        manageStock.ModifyArticle(Stock, Int32.Parse(articleREF), ArticleName);
                        break;

                    case "5":
                        Console.WriteLine("Rechercher un article par le nom");
                        Console.WriteLine("Introduisez le nom de l'article");
                        articleNAME = Console.ReadLine();
                        manageStock.SearchArticleByName(Stock, articleNAME);
                        break;

                    case "6":
                        Console.WriteLine("Rechercher un article par son prix");
                        Console.WriteLine("Introduisez le prix de l'article");
                        articlePRICE = Console.ReadLine();
                        manageStock.SearchArticleByprice(Stock, Convert.ToDouble(articlePRICE));
                        break;
                    case "7":
                        Console.WriteLine("Affichage du stock");
                        manageStock.DisplayAllArticle(Stock);
                        db.showDB();

                        break;

                    case "8":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Choix invalide");
                        break;
                }
            }while (decision != "8");
            Console.ReadKey();
            db.CloseDB();
        }
     

    }
}
