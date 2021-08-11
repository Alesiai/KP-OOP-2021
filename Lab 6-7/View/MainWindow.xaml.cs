using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;
using Lab_6_7.Model;
using Lab_6_7.Repository;
using Lab_6_7.View;
using Lab_6_7.ViewModel;
using Lab_6_7.CaffeDbContext;
using System.Collections.Generic;
using System.Linq;

namespace Lab_6_7.View
{
    
    public partial class MainWindow : Window
    {
        public static List<Employee> employees = new List<Employee>();

        public MainWindow()
        {
            PasswordWindow window1 = new PasswordWindow();
            window1.Close();
            InitializeComponent();

            EmployeesName.Text = PasswordWindow.currentUser.UserName;

            List<string> styles = new List<string> { "default", "blue" };
            styleBox.SelectionChanged += ThemeChange;
            styleBox.ItemsSource = styles;
            styleBox.SelectedItem = "default";
        }

        private void ThemeChange(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string style = styleBox.SelectedItem as string;
                // определяем путь к файлу ресурсов
                var uri = new Uri("View/" + style + ".xaml", UriKind.Relative);
                // загружаем словарь ресурсов
                ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
                // очищаем коллекцию ресурсов приложения
                Application.Current.Resources.Clear();
                // добавляем загруженный словарь ресурсов
                Application.Current.Resources.MergedDictionaries.Add(resourceDict);
            }
            catch { }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ListOfOrders taskWindow = new ListOfOrders();
           taskWindow.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ListOfEmployees view = new ListOfEmployees(); 
            view.Show();
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            PasswordWindow passwordWindow = new PasswordWindow();
            passwordWindow.Show();
            this.Close();

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ListOfProducts view = new ListOfProducts();
            view.Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            ListOfClients view = new ListOfClients();
            view.Show();
        }

        private void Button_Click5(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Cafe Manager Version 1.2");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            using (ClientRepos db = new ClientRepos())
            {
               db.Dispose();
            }
            using (OrderRepos db = new OrderRepos())
            {
                db.Dispose();
            }
            using (EmployeeRepos db = new EmployeeRepos())
            {
                db.Dispose();
            }
            using (ProductRepos db = new ProductRepos())
            {
                db.Dispose();
            }
            using (ProductsInOrderRepos db = new ProductsInOrderRepos())
            {
                db.Dispose();
            }


            this.Close();
        }
    }
}
