using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab_6_7.Model;
using Lab_6_7.CaffeDbContext;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Collections.ObjectModel;
using Lab_6_7.ViewModel;

namespace Lab_6_7.Repository
{
    class OrderRepos : IRepository<Order>
    {
        public OrderContext or;

        public OrderRepos()
        {
            this.or = new OrderContext();

        }

        public OrderRepos(OrderContext context)
        {
            this.or = context;
        }


        public void Dispose()
        {
            if (or != null)
                or.connection.Close();
        }

        public ObservableCollection<Order> GetParts()
        {
            string sql = "Select * From ЗАКАЗ";
            DataTable OrdersTable = new DataTable();
            SqlDataAdapter adapter;

            ObservableCollection<Order> Orders = new ObservableCollection<Order>();

            try
            {
                SqlCommand command = new SqlCommand(sql, or.connection);
            adapter = new SqlDataAdapter(command);
            adapter.Fill(OrdersTable);
            for (int i = 0; i < OrdersTable.Rows.Count; i++)
            {
                Order.size = OrdersTable.Rows[i].Field<int>("НомерЗаказа");
                Order order = new Order();
                order.OrId = OrdersTable.Rows[i].Field<int>("НомерЗаказа");
                order.CreateDate = OrdersTable.Rows[i].Field<DateTime>("ДатаЗаказа");
                order.Discount = OrdersTable.Rows[i].Field<decimal>("CуммаЗаказаСоСкидкой");
                order.Cost = OrdersTable.Rows[i].Field<decimal>("CуммаЗаказа");
                order.Time = Convert.ToString(OrdersTable.Rows[i].Field<TimeSpan>("ВремяЗаказа"));
                order.OrderStatus = (OrderStatus)Enum.Parse(typeof(OrderStatus), OrdersTable.Rows[i].Field<string>("Статус"));

                order.ProductsInOrder = new List<ProductsInOrder>();
                Orders.Add(order);

                

                foreach (Employee employee in EmployeeViewModel.ListOfEmployees)
                {
                    if (employee.EmpPersonId == OrdersTable.Rows[i].Field<int>("ИНСотрудника"))
                    {
                        order.Employee = employee;
                    }
                }

                foreach (Client client in ClientViewModel.ListOfClients)
                {
                    if (client.ClPersonId == OrdersTable.Rows[i].Field<int>("ИНКлиента"))
                    {
                        order.Client = client;
                    }
                }
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return Orders;
        }

        public bool AddPart(Order p)
        {
            DateTime date = Convert.ToDateTime(p.CreateDate);
            int month = date.Month, year = date.Year, day = date.Day;
            string str = Convert.ToString(month) + "." + Convert.ToString(day) + "." + Convert.ToString(year);

            int Des = (int)((p.Discount - Math.Truncate(p.Discount)) * 100);
            string DiscountString = Convert.ToString(Math.Truncate(p.Discount)) + "." + Convert.ToString(Des);

            Des = (int)((p.Cost - Math.Truncate(p.Cost)) * 100);
            string CostString = Convert.ToString(Math.Truncate(p.Cost)) + "." + Convert.ToString(Des);

            string sql = $"INSERT INTO ЗАКАЗ(ИНСотрудника, ИНКлиента, ДатаЗаказа, CуммаЗаказаСоСкидкой, CуммаЗаказа, ВремяЗаказа, Статус) VALUES " +
                $"({p.Employee.EmpPersonId}, {p.Client.ClPersonId} ,\'{str}\', \'{DiscountString}\', \'{CostString}\',\'{p.Time}\' ,\'{Convert.ToString(p.OrderStatus)}\'); ";

            try
            {
                SqlCommand command = new SqlCommand(sql, or.connection);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool Update(int id, Order p)
        {
            DateTime date = Convert.ToDateTime(p.CreateDate);
            int month = date.Month, year = date.Year, day = date.Day;
            string str = Convert.ToString(month) + "." + Convert.ToString(day) + "." + Convert.ToString(year);

            int Des = (int)((p.Discount - Math.Truncate(p.Discount)) * 100);
            string DiscountString = Convert.ToString(Math.Truncate(p.Discount)) + "." + Convert.ToString(Des);

            Des = (int)((p.Cost - Math.Truncate(p.Cost)) * 100);
            string CostString = Convert.ToString(Math.Truncate(p.Cost)) + "." + Convert.ToString(Des);

            string sql = $"UPDATE ЗАКАЗ SET " +
                $"CуммаЗаказаСоСкидкой = \'{DiscountString}\', " +
                $"CуммаЗаказа = \'{CostString}\', " +
                $"ВремяЗаказа = \'{p.Time}\', " +
                $"Статус = \'{Convert.ToString(p.OrderStatus)}\' " +
                $"WHERE НомерЗаказа = {id}";
            try
            {
                SqlCommand command = new SqlCommand(sql, or.connection);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool Delete(int id, Order p)
        {
            return true;
        }
    }
}
