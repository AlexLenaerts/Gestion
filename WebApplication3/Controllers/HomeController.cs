using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication.Models;
using Gestion_du_stock;
using System.Data.SqlClient;
using System.Text;
using article = WebApplication.Models.article;
using System.Text.RegularExpressions;

namespace WebApplication.Controllers
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

        //View stock
        public IActionResult Index()
        {
            con = new SqlConnection(ConnectionString);
            ViewData["stock"] = DB.DBTOLIST(con);
            article.show = "DB";
            return View();
        }
        

        [HttpPost]
        public IActionResult Index(article model)
        {

            bool check = false;
            article.found = false;
            List<string> save = new List<string>();

            foreach (var x in Request.Form.Keys)
            {
                if (x.Contains("Remove"))
                {
                    int y = int.Parse(x.Substring(7, x.Count() - 7));
                    con = new SqlConnection(ConnectionString);
                    ViewData["article"] = DB.DBTOLIST(con).Where(z => (z.NumberRef == y)).ToList();
                    if (check == false && ViewData["article"]!=null)
                    {
                        string articleREF = ((List<Gestion_du_stock.article>)ViewData["article"]).First().NumberRef.ToString(); //((List<Gestion_du_stock.article>)ViewData["article"]).items[0].NumberRef;
                        con = new SqlConnection(ConnectionString);
                        DB.RemoveArticleByRef(articleREF, con);
                        check = true;
                        ModelState.Clear();
                        article.show = "DB";
                    }
                }

                else if (x.Contains("Modify"))
                {
                    int y = int.Parse(x.Substring(7, x.Count() - 7));
                    con = new SqlConnection(ConnectionString);
                    ViewData["article"] = DB.DBTOLIST(con).Where(z => (z.NumberRef == y)).ToList();
                    article.show = "Modify";
                }


                else if (x.Contains("Confirm") && model.Name != null && model.SellPrice != 0 && model.QuantityStock != 0 && check == false)
                {
                    con = new SqlConnection(ConnectionString);
                    string articleREF = model.NumberRef.ToString();
                    string articleNAME = model.Name;
                    string articlePRICE = model.SellPrice.ToString();
                    string articleQUANTITY = model.QuantityStock.ToString();
                    Gestion_du_stock.article newArticle = new Gestion_du_stock.article(Int32.Parse(articleREF), articleNAME, Convert.ToDouble(articlePRICE), Int32.Parse(articleQUANTITY));
                    DB.ModifyArticle(newArticle, con);
                    check = true;
                    ModelState.Clear();
                    article.show = "DB";
                }

                else if (x.Contains("Search"))
                {
                    article.show = "Search";
                }
                else if (x.Contains("rechercher") && model.NumberRef != 0 && check == false)
                {
                    con = new SqlConnection(ConnectionString);
                    ViewData["ref"] = DB.DBTOLIST(con).Where(x => x.NumberRef == model.NumberRef).ToList();
                    check = true;
                    article.found = true;
                    ModelState.Clear();
                }

                else if (x.Contains("Add"))
                {
                    article.show = "Add";
                }

                else if (x.Contains("Ajouter") && model.Name != null && model.NumberRef != 0 && model.SellPrice != 0 && model.QuantityStock != 0 && check == false)
                {
                    con = new SqlConnection(ConnectionString);
                    string articleREF = model.NumberRef.ToString();
                    Gestion_du_stock.article newArticle = new Gestion_du_stock.article();
                    if (!save.Any())//vide
                    {
                        string articleNAME = model.Name;
                        string articlePRICE = model.SellPrice.ToString();
                        string articleQUANTITY = model.QuantityStock.ToString();
                        save = new List<string>() {articleNAME, articlePRICE, articleQUANTITY };
                        ViewData["save1"] = save[0];
                        ViewData["save2"] = save[1];
                        ViewData["save3"] = save[2];

                    }
                    newArticle = new Gestion_du_stock.article(Int32.Parse(model.NumberRef.ToString()), save[0], Convert.ToDouble(save[1]), Int32.Parse(save[2]));
                    if (DB.DBTOLIST(con).Where(u => u.NumberRef == Convert.ToInt32(model.NumberRef.ToString())).ToList().Any())
                    {
                        ViewBag.Message = $"La référence {model.NumberRef} existe déjà ! Veuillez encoder une autre.";
                        ModelState.Clear();
                    }
                    else
                    {
                        DB.AddToDB(newArticle, con);
                        check = true;
                        ModelState.Clear();
                        article.show = "DB";
                    }
                    
                }
                else if (x.Contains("Stocks"))
                {
                    con = new SqlConnection(ConnectionString);
                    ViewData["stock"] = DB.DBTOLIST(con);
                    article.show = "DB";
                }
                else if (x.Contains("extract"))
                {
                    string AppName = "excel.exe";

                    con = new SqlConnection(ConnectionString);
                    StringBuilder sbRtn = new StringBuilder();
                    List<Gestion_du_stock.article> data = new List<Gestion_du_stock.article>();
                    data = DB.DBTOLIST(con);
                    var first = "sep = ,";
                    sbRtn.AppendLine(first);
                    var header = string.Format("{0},{1},{2},{3}",
                                               "Reference",
                                               "Name",
                                               "Price",
                                               "Quantity"
                                              );
                    sbRtn.AppendLine(header);
                    foreach (Gestion_du_stock.article item in data)
                    {
                        var body = string.Format("=\"{0}\",=\"{1}\",=\"{2}\",=\"{3}\"",
                                               item.NumberRef,
                                               item.Name.Trim(),
                                               item.SellPrice,
                                               item.QuantityStock);

                        sbRtn.AppendLine(body);
                    }
                    System.IO.File.WriteAllText(@"C:\Users\Alexandre\WebApplication\test.csv", sbRtn.ToString(), Encoding.UTF8);
                    Process.Start(AppName, @"C:\Users\Alexandre\WebApplication\test.csv");
                }
                else if (x.Contains("load"))
                {
                    con = new SqlConnection(ConnectionString);
                    string[] readText = System.IO.File.ReadAllLines(@"C:\Users\Alexandre\WebApplication\test.csv");
                    foreach (string s in readText)
                    {
                        if ((s!= "sep = ,") && (s != "Reference,Name,Price,Quantity"))
                        {
                            string clean = Regex.Replace(s, "[^A-Za-z0-9,]", "");
                            var splited = clean.Split(",");
                            Gestion_du_stock.article new_article = new Gestion_du_stock.article(int.Parse(splited[0].ToString()), splited[1].ToString(), double.Parse(splited[2].ToString()), int.Parse(splited[3]));
                            if (!DB.DBTOLIST(con).Where(u => u.NumberRef == Convert.ToInt32(splited[0].ToString())).ToList().Any())
                            {
                                DB.AddToDB(new_article, con);
                            }
                        }
                    }
                }
                con = new SqlConnection(ConnectionString);
                ViewData["stock"] = DB.DBTOLIST(con);
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
