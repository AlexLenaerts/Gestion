using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class article
    {
        public int NumberRef { get; set; }
        public string Name { get; set; }
        public double SellPrice { get; set; }
        public int QuantityStock { get; set; }
        public static bool found { get; set; }

        public static string show { get; set; }
       


        /*
        public article(int NumRef, string name, double SellPrice, int quantityStock)
        {
            this.NumberRef = NumRef;
            this.Name = name;
            this.SellPrice = SellPrice;
            this.QuantityStock = quantityStock;
        }
        public override string ToString()
        {
            return ("\r\nRéférence: " + NumberRef + "\r\nNom: " + Name + "\r\nPrix de vente :" + SellPrice + "\r\nQuantity :" + QuantityStock);
        }
        */
    }

}
