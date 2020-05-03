﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication3.Models;
using Gestion_du_stock;
using System.Data.SqlClient;
using System.Text;
using MimeKit;
using MailKit.Net.Smtp;
using article = WebApplication3.Models.article;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private string ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Alexandre\Gestion_du_stock\Gestion_du_stock\Database1.mdf; Integrated Security = True";
        private SqlConnection con;


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //GET
        public IActionResult Index()
        {
          article.show = "";
          return View();
        }

        [HttpPost]
        public IActionResult Index(article model)
        {
            bool check = false;

            if (Request.Form["Add"].Count != 0)
            {
                article.show = "Add";
                if (model.Name != null && model.NumberRef != 0 && model.SellPrice != 0 && model.QuantityStock != 0 && check == false)
                {
                    con = new SqlConnection(ConnectionString);
                    string articleREF = model.NumberRef.ToString();
                    string articleNAME = model.Name;
                    string articlePRICE = model.SellPrice.ToString();
                    string articleQUANTITY = model.QuantityStock.ToString();
                    Gestion_du_stock.article newArticle = new Gestion_du_stock.article(Int32.Parse(articleREF), articleNAME, Convert.ToDouble(articlePRICE), Int32.Parse(articleQUANTITY));
                    DB.ConnectDB(con);
                    DB.AddToDB(newArticle, con);
                    check = true;
                    ModelState.Clear();
                }
            }
            else if (Request.Form["Remove"].Count != 0)
            {
                article.show = "Remove";
                if (model.NumberRef != 0 && check == false)
                {
                    con = new SqlConnection(ConnectionString);
                    string articleREF = model.NumberRef.ToString();
                    DB.ConnectDB(con);
                    DB.RemoveArticleByRef(articleREF, con);
                    check = true;
                    ModelState.Clear();
                }
            }
            else if (Request.Form["Modify"].Count != 0)
            {
                article.show = "Modify";
                if (model.Name != null && model.NumberRef != 0 && model.SellPrice != 0 && model.QuantityStock != 0 && check == false)
                {
                    con = new SqlConnection(ConnectionString);
                    string articleREF = model.NumberRef.ToString();
                    string articleNAME = model.Name;
                    string articlePRICE = model.SellPrice.ToString();
                    string articleQUANTITY = model.QuantityStock.ToString();
                    Gestion_du_stock.article newArticle = new Gestion_du_stock.article(Int32.Parse(articleREF), articleNAME, Convert.ToDouble(articlePRICE), Int32.Parse(articleQUANTITY));
                    DB.ConnectDB(con);
                    DB.ModifyArticle(newArticle, con);
                    check = true;
                    ModelState.Clear();
                }
            }
            else if (Request.Form["Search"].Count != 0)
            {
                article.show = "Search";
                if (model.NumberRef != 0 && check == false)
                {
                    con = new SqlConnection(ConnectionString);
                    DB.ConnectDB(con);
                    ViewData["ref"] = DB.DBTOLIST(con).Where(x => x.NumberRef == model.NumberRef).ToList();
                    check = true;
                }
                
                if(check == true)
                {
                    article.found = true;
                    ModelState.Clear();
                }

            }
            return View();
        }

        //View stock
        public IActionResult ShowDB()
        {
            con = new SqlConnection(ConnectionString);
            DB.ConnectDB(con);
            ViewData["stock"] = DB.DBTOLIST(con);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Contact()
        {
            return View();
        }
        
    }
}
