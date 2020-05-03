using System;
using System.Collections.Generic;

namespace Gestion_du_stock
{
    public static class ManageStock
    {
        public static void SearchArticleByRef(List<article> Stock,
                                            int numberRef)
        {
            article item = Stock.Find(item2 => item2.NumberRef.Equals(numberRef));
            if (item == null)
            {
                Console.WriteLine("l'article n'est pas en stock");
            }
            else
            {
                Console.WriteLine(item);
            }
        }

        public static void AddArticle(List<article> Stock,
                                        article article)
        {
            int compteur = 0;
            foreach (article element in Stock)
            {
                if (element.NumberRef == article.NumberRef)
                {
                    compteur += 1;
                }
            }
            if (compteur == 0)
            {
                Stock.Add(article);
            }
            else
            {
                Console.WriteLine("déjà en stock");
            }
        }
        public static void RemoveArticleByRef(List<article> Stock, int numberRef)
        {
            List<article> Stock2 = new List<article>(Stock);
            foreach (article element in Stock2)
            {
                if (element.NumberRef == numberRef)
                {
                    Stock.Remove(element);
                }
            }
        }


        public static void ModifyArticle(List<article> Stock, int numberRef, article new_article)
        {
            foreach (article element in Stock)
            {
                if (element.NumberRef == numberRef)
                {
                    element.Name = new_article.Name;
                    element.QuantityStock = new_article.QuantityStock;
                    element.SellPrice = new_article.SellPrice;
                }
            }
        }
        public static void SearchArticleByName(List<article> Stock, string name)
        {
            foreach (article element in Stock)
            {
                if (element.Name == name)
                {
                    Console.WriteLine(element);
                }
            }
        }
        public static void SearchArticleByprice(List<article> Stock, double price)
        {
            foreach (article element in Stock)
            {
                if (element.SellPrice == price)
                {
                    Console.WriteLine(element);
                }
            }

        }
        public static void DisplayAllArticle(List<article> Stock)
        {
            foreach (article element in Stock)
            {
                Console.WriteLine(element);
            }

        }
    }
}
