using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab_6_7.Model;
using Lab_6_7.CaffeDbContext;
using Lab_6_7.ViewModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Collections.ObjectModel;

namespace Lab_6_7.Repository
{
    class ProductRepos : IRepository<Product>
    {
        public ProductContext pr;

        public ProductRepos()
        {
            this.pr = new ProductContext();

        }

        public ProductRepos(ProductContext context)
        {
            this.pr = context;
        }


        public void Dispose()
        {
            if (pr != null)
                pr.connection.Close();
        }

        public ObservableCollection<Product> GetParts()
        {
            string sql = "Select * From ТОВАР";
            DataTable productsTable = new DataTable();
            SqlDataAdapter adapter;

            ObservableCollection<Product> products = new ObservableCollection<Product>();

            try
            {
                SqlCommand command = new SqlCommand(sql, pr.connection);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(productsTable);
                for (int i = 0; i < productsTable.Rows.Count; i++)
                {
                    Product.size = productsTable.Rows[i].Field<int>("ИНТовара");
                    Product product = new Product();
                    product.ProductId = productsTable.Rows[i].Field<int>("ИНТовара");
                    product.ProductName = productsTable.Rows[i].Field<string>("Товар");
                    product.ProdCost = productsTable.Rows[i].Field<decimal>("Цена");
                    product.ProductDescription = productsTable.Rows[i].Field<string>("Описание");
                    product.Type = (ProductType)Enum.Parse(typeof(ProductType), productsTable.Rows[i].Field<string>("ТипТовара"));

                    products.Add(product);

                    DateTime? delate = productsTable.Rows[i].Field<DateTime?>("ДатаУдаления");
                    if (delate != null)
                    {
                        product.DeleteDate = delate;
                        products.Remove(product);
                    }
                    else
                    {
                        if (product.Type == ProductType.Coffee) { ProductViewModel.ListOfCoffee.Add(product); }
                        else if (product.Type == ProductType.Tea) { ProductViewModel.ListOfTea.Add(product); }
                        else if (product.Type == ProductType.Desert) { ProductViewModel.ListOfDesserts.Add(product); }
                        else { ProductViewModel.ListOfSnacks.Add(product); };
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return products;
        }

        public bool AddPart(Product p)
        {
            int Des = (int)((p.ProdCost - Math.Truncate(p.ProdCost)) * 100);
            string STR = Convert.ToString(Math.Truncate(p.ProdCost)) + "." + Convert.ToString(Des);

            string sql = $"INSERT INTO ТОВАР(Товар, Цена, Описание, ТипТовара) VALUES " +
                $"(\'{p.ProductName}\', {STR}, \'{p.ProductDescription}\', \'{Convert.ToString(p.Type)}\'); ";
            try
            {
                SqlCommand command = new SqlCommand(sql, pr.connection);
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
            int Des = (int)((p.ProdCost - Math.Truncate(p.ProdCost)) * 100);
            string STR = Convert.ToString(Math.Truncate(p.ProdCost)) + "." + Convert.ToString(Des);
            string sql = $"UPDATE ТОВАР SET " +
                $"Товар = \'{p.ProductName}\', " +
                $"Цена = {STR}, " +
                $"Описание = \'{p.ProductDescription}\', " +
                $"ТипТовара = \'{Convert.ToString(p.Type)}\' " +
                $"WHERE ИНТовара = {id}";
            try
            {
                SqlCommand command = new SqlCommand(sql, pr.connection);
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
            string sql = $"UPDATE ТОВАР SET " +
                $"ДатаУдаления = GETDATE() " +
                $"WHERE ИНТовара = {id}";
            try
            {
                SqlCommand command = new SqlCommand(sql, pr.connection);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }
    }
}
