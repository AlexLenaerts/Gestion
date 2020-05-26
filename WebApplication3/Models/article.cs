using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class article
    {
        public int NumberRef { get; set; }
        public string Name { get; set; }
        public double SellPrice { get; set; }
        public int QuantityStock { get; set; }
        public static bool Found { get; set; }
        public static string Show { get; set; }
    }

}
