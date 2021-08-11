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
using System.Windows.Shapes;
using Lab_6_7.ViewModel;
using Lab_6_7.CaffeDbContext;
using Lab_6_7.Model;
using Lab_6_7.Repository;
using Lab_6_7.Command;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;

namespace Lab_6_7.View
{
    /// <summary>
    /// Логика взаимодействия для ListOfOrders.xaml
    /// </summary>
    public partial class ListOfOrders : Window
    {
        public static MainViewModel ViewModel { get; set; }
        public static bool ACTIVE;


        public ListOfOrders()
        {
            ACTIVE = true;
            ViewModel = new ViewModel.MainViewModel(OrderViewModel.ListOfOrders); // Создали ViewModel 

            InitializeComponent();

            this.DataGridXAML.ItemsSource = ProductViewModel.ListOfProducts;
            this.DataGridXAML.ItemsSource = OrderViewModel.ListOfOrders;
        }


        
        Order row;

        //Save Order's ID
        private void DataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (ACTIVE == true)
                {
                    try
                    {
                        DataGrid dg = sender as DataGrid;
                        row = dg.SelectedItems[0] as Order;
                    }
                    catch { }
                }
            }
            catch { }
        }

        //Opening Order's card
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                OrderView taskWindow = new OrderView(new OrderViewModel(row));
                taskWindow.Show();
                this.Close();
            }
            catch { }
        }

        //Add Order
        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newEmp = new Order();

                foreach (Employee employee in EmployeeViewModel.ListOfEmployees)
                {

                    if (employee.EmpPersonId == PasswordWindow.currentUser.UserId)
                    {
                        newEmp.Employee = employee;
                    }
                }

                OrderView taskWindow = new OrderView(new OrderViewModel(newEmp) { IsNew = true });

                OrderViewModel.ListOfOrders.Add(newEmp);
                taskWindow.Show();
                this.Close();
            }
            catch { }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void UpdateOrders_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (Order Order in OrderViewModel.ListOfOrders)
                {
                    using (OrderRepos db = new OrderRepos())
                    {
                        db.Update(Order.OrId, Order);
                    }
                }
               
                OrderViewModel.OrderInit();
                ProductsInOrderViewModel.ProductsInOrderInit();

                MessageBox.Show("Данные обновлены");

                this.DataGridXAML.ItemsSource = ProductViewModel.ListOfProducts;
                this.DataGridXAML.ItemsSource = OrderViewModel.ListOfOrders;

                ViewModel = new ViewModel.MainViewModel(OrderViewModel.ListOfOrders); // Создали ViewModel 
            }
            catch 
            { }

        }

        private void SearchControl_SearchTextChanged(string search)
        {
            try
            {
                if (searchControl is null)
                {

                    this.DataGridXAML.ItemsSource = OrderViewModel.ListOfOrders;
                }
                OrderViewModel.SeachList(search);
                this.DataGridXAML.ItemsSource = OrderViewModel.CV;
            }
            catch 
            {
                this.DataGridXAML.ItemsSource = OrderViewModel.ListOfOrders;
            }
        }
    }
}

