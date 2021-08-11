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
    /// Логика взаимодействия для ListOfEmployees.xaml
    /// </summary>
    public partial class ListOfEmployees : Window
    {
        public static MainViewModel ViewModel { get; set; }
        public static bool ACTIVE;


        public ListOfEmployees()
        {
            ACTIVE = true;
            ViewModel = new ViewModel.MainViewModel(EmployeeViewModel.ListOfEmployees); // Создали ViewModel 

            InitializeComponent();


            this.DataGridXAML.ItemsSource = OrderViewModel.ListOfOrders;
            this.DataGridXAML.ItemsSource = EmployeeViewModel.ListOfEmployees;

        }


        Employee row;

        //Save employee's ID
        private void DataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (ACTIVE == true)
                {
                    try
                    {
                        DataGrid dg = sender as DataGrid;
                        row = dg.SelectedItems[0] as Employee;
                    }
                    catch { }
                }
            }
            catch { }
        }

        //Opening employee's card
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                EmployeeView taskWindow = new EmployeeView(new EmployeeViewModel(row));
                taskWindow.Show();
                this.Close();
            }
            catch { }
        }

        //Add employee
        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
<<<<<<< HEAD
            try
            {
                var newEmp = new Employee();
                EmployeeView taskWindow = new EmployeeView(new EmployeeViewModel(newEmp) { IsNew = true });

                EmployeeViewModel.ListOfEmployees.Add(newEmp);
                taskWindow.Show();
                this.Close();
            }
            catch { }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

       
        private void Updateemployees_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (Employee employee in EmployeeViewModel.ListOfEmployees)
                {
                    using (EmployeeRepos db = new EmployeeRepos())
                    {
                        db.Update(employee.EmpPersonId, employee);
                    }
                }
                EmployeeViewModel.EmployeeInit();


                MessageBox.Show("Данные обновлены");
                this.DataGridXAML.ItemsSource = OrderViewModel.ListOfOrders;
                this.DataGridXAML.ItemsSource = EmployeeViewModel.ListOfEmployees;
                


                ViewModel = new ViewModel.MainViewModel(EmployeeViewModel.ListOfEmployees); // Создали ViewModel
            }
            catch { }

        }

        private void SearchControl_SearchTextChanged(string search)
        {
            try
            {
                if (searchControl is null)
                {
                    this.DataGridXAML.ItemsSource = EmployeeViewModel.ListOfEmployees;
                }
                EmployeeViewModel.SeachList(search);
                this.DataGridXAML.ItemsSource = EmployeeViewModel.CV;
            }
            catch 
            {
                this.DataGridXAML.ItemsSource = EmployeeViewModel.ListOfEmployees;
            }
        }
=======
            IsNew = true;
            EmployeeView taskWindow = new EmployeeView();
            taskWindow.Show();
            this.Close();
        }
>>>>>>> parent of ddd02f0 (NEW)
    }
}
