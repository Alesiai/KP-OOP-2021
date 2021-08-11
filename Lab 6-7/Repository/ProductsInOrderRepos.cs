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
    class ProductsInOrderRepos : IRepository<ProductsInOrder>
    {
        public ProductsInOrderContext pr;

        public ProductsInOrderRepos()
        {
            this.pr = new ProductsInOrderContext();

        }

        public ProductsInOrderRepos(ProductsInOrderContext context)
        {
            this.pr = context;
        }


        public void Dispose()
        {
            if (pr.connection != null)
                pr.connection.Close();
        }

        public ObservableCollection<ProductsInOrder> GetParts()
        {
            string sql = "Select * From ТОВАРЫВЗАКАЗЕ";
            DataTable ProductsInOrdersInOrderTable = new DataTable();
            SqlDataAdapter adapter;

            ObservableCollection<ProductsInOrder> ProductsInOrders = new ObservableCollection<ProductsInOrder>();

            try
            {
                SqlCommand command = new SqlCommand(sql, pr.connection);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ProductsInOrdersInOrderTable);

                for (int i = 0; i < ProductsInOrdersInOrderTable.Rows.Count; i++)
                {
                    
                    ProductsInOrder productsInOrder = new ProductsInOrder();
                    
                    productsInOrder.ProductId = ProductsInOrdersInOrderTable.Rows[i].Field<int>("ИНТовара");
                    productsInOrder.OrderId = ProductsInOrdersInOrderTable.Rows[i].Field<int>("ИНЗаказа");
                    productsInOrder.Price = ProductsInOrdersInOrderTable.Rows[i].Field<decimal>("Цена");
                    productsInOrder.NumOfPos = ProductsInOrdersInOrderTable.Rows[i].Field<int>("КоличествоПозиций");

                    ProductsInOrders.Add(productsInOrder);
                }

               
                foreach (ProductsInOrder searchProductsInOrder in ProductsInOrders)
                {
                    foreach (Product product in ProductViewModel.ListOfProducts)
                    {
                        if (searchProductsInOrder.ProductId == product.ProductId)
                        {
                            searchProductsInOrder.ProductName = product.ProductName;
                        }
                    }

                    foreach (Order order in OrderViewModel.ListOfOrders)
                    {
                        if (searchProductsInOrder.OrderId == order.OrId)
                        {
                            order.ProductsInOrder.Add(searchProductsInOrder);
                        }
                    }
                    
                    }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return ProductsInOrders;
        }

        public bool AddPart(ProductsInOrder p)
        {
            int Des = (int)((p.Price - Math.Truncate(p.Price)) * 100);
            string STR = Convert.ToString(Math.Truncate(p.Price)) + "." + Convert.ToString(Des);
            
            string sql = $"INSERT INTO ТОВАРЫВЗАКАЗЕ(ИНТовара, ИНЗаказа, Цена, КоличествоПозиций) VALUES " +
                $"(\'{p.ProductId}\', \'{p.OrderId}\', \'{STR}\', \'{p.NumOfPos}\'); ";
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

        public bool Update(int id, ProductsInOrder p)
        {
            return true;
        }

        public bool Delete(int id, ProductsInOrder p)
        {
            return true;
        }
    }
}
