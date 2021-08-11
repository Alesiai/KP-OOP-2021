using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Lab_6_7.Model;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Collections.ObjectModel;
using Lab_6_7.ViewModel;

namespace Lab_6_7.CaffeDbContext
{
    public class ProductContext : IDisposable
    {
        string connectionString;
        public SqlConnection connection = null;

        public ProductContext()
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

<<<<<<< HEAD
=======
        public List<Product> GetParts()
        {
            string sql = "Select * From ТОВАР";
            DataTable productsTable = new DataTable();
            SqlDataAdapter adapter;

            List<Product> products = new List<Product>();

            //try
            //{
                SqlCommand command = new SqlCommand(sql, connection);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(productsTable);
                for (int i = 0; i < productsTable.Rows.Count; i++)
                {
                    Product.size = productsTable.Rows[i].Field<int>("ИНТовара");
                    Product product= new Product();
                    product.ProductId = productsTable.Rows[i].Field<int>("ИНТовара");
                    product.ProductName = productsTable.Rows[i].Field<string>("Товар");
                    product.ProdCost = productsTable.Rows[i].Field<float>("Цена");
                    product.ProductDescription = productsTable.Rows[i].Field<string>("Описание");
                    product.Type = (ProductType)Enum.Parse(typeof(ProductType), productsTable.Rows[i].Field<string>("ТипТовара"));

                    products.Add(product);

                    DateTime? delate = productsTable.Rows[i].Field<DateTime?>("ДатаУдаления");
                    if (delate != null)
                    {
                        product.DeleteDate = delate;
                        products.Remove(product);
                    }
                }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            return products;
        }

        public bool AddPart(Product p)
        {
            string sql = $"INSERT INTO ТОВАР(Товар, Цена, Описание, ТипТовара) VALUES " +
                $"(\'{p.ProductName}\', \'{p.ProdCost}\', \'{p.ProductDescription}\', \'{Convert.ToString(p.Type)}\'); ";
            try
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool Update(int id, Product p)
        {
            string sql = $"UPDATE СОТРУДНИК SET " +
                $"Товар = \'{p.ProductName}\', " +
                $"Цена = \'{p.ProdCost}\', " +
                $"Описание = \'{p.ProductDescription}\', " +
                $"ТипТовара = \'{Convert.ToString(p.Type)}\', " +
                $"WHERE ИНТовара = {id}";
            try
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool Delete(int id, Product p)
        {
            string sql = $"UPDATE СОТРУДНИК SET " +
                $"Товар = \'{p.ProductName}\', " +
                $"Цена = \'{p.ProdCost}\', " +
                $"Описание = \'{p.ProductDescription}\', " +
                $"ТипТовара = \'{Convert.ToString(p.Type)}\', " +
                $"ДатаУдаления = GETDATE() " +
                $"WHERE ИНТовара = {id}";            
            try
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

>>>>>>> parent of ddd02f0 (NEW)
    }
}