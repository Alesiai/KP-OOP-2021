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
using Lab_6_7.Repository;
using Lab_6_7.Model;
using System.Text.RegularExpressions;

namespace Lab_6_7.View
{
    /// <summary>
    /// Логика взаимодействия для PasswordWindow.xaml
    /// </summary>
    public partial class PasswordWindow : Window
    {
        public PasswordWindow()
        {
            InitializeComponent();
            currentUser = CurrentUserId.getInstance(1, "");
        }

        public static string EmployeeName;
        public static CurrentUserId currentUser;

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
           
                bool IsSearched = false;
                if (string.IsNullOrEmpty(LoginTextBox.Text))
                    return;

            foreach (Employee employee in EmployeeViewModel.ListOfEmployees)
            {
                
               
                    if (employee.EmpLogin == LoginTextBox.Text)
                    {
                            IsSearched = true;
                        if (employee.EmpHashPassword == PasswordTextBox.Password)
                        {

                            currentUser.UserName = employee.EmpFullName;
                            currentUser.UserId = employee.EmpPersonId;
                            if (employee.EmpStatus == EmpStatus.Admin)
                            {
                               
                                MainWindow window = new MainWindow();
                                window.Show();
                            }
                            else
                            {
                                
                                SimpleUserMainWindow window = new SimpleUserMainWindow();
                                window.Show();
                            }
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Неверный пароль");
                        }
                    }
                

            }
            if (IsSearched == false)
            {
                MessageBox.Show("Логин не найден");
            }
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e) => Accept_Click(sender, new RoutedEventArgs());

        private void Cancel_Click(object sender, RoutedEventArgs e)
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

        
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string input = (sender as TextBox).Text;
            if (input != "") 
            {
                if (!Regex.IsMatch(input, @"\w"))
                {
                    MessageBox.Show("Ошибка ввода, вводите пожалуйста буквы Латиницей и Кириллицей, а также цифры");
                }
            }
        }

        private void PasswordTextBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            string input = (sender as PasswordBox).Password;
            if (input != "")
            {
                if (!Regex.IsMatch(input, @"\w"))
                {
                    MessageBox.Show("Ошибка ввода, вводите пожалуйста буквы Латиницей и Кириллицей, а также цифры");
                }
            }

        }
    }
}
