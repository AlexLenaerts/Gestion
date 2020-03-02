using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Gestion_du_stock;
using System.Data.SqlClient;
using System.Threading;

namespace WpfApp2
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Alexandre\Gestion_du_stock\Gestion_du_stock\Database1.mdf; Integrated Security = True";
        private SqlConnection con;
        public List<article> stock_article;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += Window_Loaded;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            con = new SqlConnection(ConnectionString);
            DB.ConnectDB(con);
            stock_article = DB.DBTOLIST(con);
            Refresh();
        }

        public void Refresh()
        {
            this.DataContext = null;
            this.DataContext = stock_article;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            string articleREF = reference.Text;
            string articleNAME = name.Text;
            string articlePRICE = price.Text;
            string articleQUANTITY = stock.Text;
            article newArticle = new article(Int32.Parse(articleREF), articleNAME, Convert.ToDouble(articlePRICE), Int32.Parse(articleQUANTITY));
            stock_article.Add(newArticle);
            DB.AddToDB(newArticle, con);
            Refresh();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            string articleREF = reference.Text;
            DB.RemoveArticleByRef(articleREF, con);
            ManageStock.RemoveArticleByRef(stock_article, int.Parse(articleREF));
            Refresh();


        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string articleREF = reference.Text;
            string articleNAME = name.Text;
            string articlePRICE = price.Text;
            string articleQUANTITY = stock.Text;
            article newArticle = new article(Int32.Parse(articleREF), articleNAME, Convert.ToDouble(articlePRICE), Int32.Parse(articleQUANTITY));
            ManageStock.ModifyArticle(stock_article, Int32.Parse(articleREF), newArticle);
            DB.ModifyArticle(newArticle, con);
            Refresh();
        }
    }
}

