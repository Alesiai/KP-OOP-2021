using System.Data;
using System.Linq;
using System.Windows;
using Lab_6_7.Model;
using Lab_6_7.ViewModel;
using Lab_6_7.View;
using System.Collections.Generic;
using System;

namespace Lab_6_7
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            EmployeeViewModel.EmployeeInit();
            ProductViewModel.ProductInit();
            ClientViewModel.ClientInit();

            OrderViewModel.OrderInit();
            ProductsInOrderViewModel.ProductsInOrderInit();


            PasswordWindow window = new PasswordWindow();
            window.Show();
        }
    }
}
