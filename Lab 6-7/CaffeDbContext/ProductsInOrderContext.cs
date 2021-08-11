using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Lab_6_7.Model;
using Lab_6_7.ViewModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Collections.ObjectModel;

namespace Lab_6_7.CaffeDbContext
{
    public class ProductsInOrderContext : IDisposable
    {
        string connectionString;
        public SqlConnection connection = null;

        public ProductsInOrderContext()
        {
            connectionString = "server=DESKTOP-9MPO3CO\\MYCOLLECTION;Trusted_Connection=Yes;DataBase=COFFEE;";
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        public void Dispose()
        {
            if (connection != null)
                connection.Close();
        }
    }
}