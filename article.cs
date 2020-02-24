using System;
using System.Collections.Generic;
using System.Text;

namespace Gestion_du_stock
{
    class article
    {
        private int _NumRef;
        private string _name;
        private double _SellPrice;
        private int _quantityStock;
        public int NumberRef 
        {
            get { return _NumRef; }
            set{ _NumRef = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public double SellPrice
        {
            get { return _SellPrice; }
            set { _SellPrice = value; }
        }
        public int QuantityStock
        {
            get { return _quantityStock; }
            set { _quantityStock = value; }
        }
        public article(int NumRef, string name, double SellPrice, int quantityStock)
        {
            this._NumRef = NumRef;
            this._name = name;
            this._SellPrice = SellPrice;
            this._quantityStock = quantityStock;
        }
        public override string ToString()
        {
            return ("\r\nRéférence: " + this._NumRef +"\r\nNom: " + this._name + "\r\nPrix de vente :" + this._SellPrice + "\r\nQuantity :" + this._quantityStock);
        }
    }

}
