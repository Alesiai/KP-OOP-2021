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
using Lab_6_7.Model;
using Lab_6_7.Repository;
using Lab_6_7.ViewModel;
using Lab_6_7.CaffeDbContext;
using System.Text.RegularExpressions;
using Lab_6_7.Command;

namespace Lab_6_7.View
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class EmployeeView : Window
    {
        //public MainViewModel ViewModel { get; set; }

        public EmployeeViewModel ViewModel
        { get; set; }

        public static int ID;
        public EmployeeView(EmployeeViewModel VM)
        {
            ViewModel = VM;

            if (ViewModel.IsNew)
            {
                ViewModel.Employee.EmpPersonId = Employee.size;
            }


            InitializeComponent();

            SatusBlock.ItemsSource = Enum.GetNames(typeof(EmpStatus));
            SatusBlock.SelectedItem = Convert.ToString(ViewModel.Employee.EmpStatus);

            ListOfEmployees.ACTIVE = false;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ConfirmActionDialog confirmActionDialog = new ConfirmActionDialog();

                if (confirmActionDialog.ShowDialog() == true)
                {
                    EmployeeViewModel.ListOfEmployees.Remove(ViewModel.Employee);
                }


                if (!ViewModel.IsNew)
                {
                    try
                    {
                        Employee EmployeeFromEmployeeList = new Employee();

                        EmployeeFromEmployeeList.EmpFullName = ViewModel.Employee.EmpFullName;
                        EmployeeFromEmployeeList.EmpPersonId = ViewModel.Employee.EmpPersonId;
                        EmployeeFromEmployeeList.EmpPhone = ViewModel.Employee.EmpPhone;
                        EmployeeFromEmployeeList.EmpLogin = ViewModel.Employee.EmpLogin;
                        EmployeeFromEmployeeList.EmpHashPassword = ViewModel.Employee.EmpHashPassword;
                        EmployeeFromEmployeeList.EmpCreateDate = ViewModel.Employee.EmpCreateDate;
                        EmployeeFromEmployeeList.EmpDeleteDate = ViewModel.Employee.EmpDeleteDate;
                        EmployeeFromEmployeeList.EmpStatus = ViewModel.Employee.EmpStatus;
                        EmployeeFromEmployeeList.EmpPersonId = ViewModel.Employee.EmpPersonId;

                        using (EmployeeRepos db = new EmployeeRepos())
                        {
                            db.Delete(EmployeeFromEmployeeList.EmpPersonId, EmployeeFromEmployeeList);
                        }
                    }
                    catch { }
                }

                this.Close();
                ListOfEmployees taskWindow = new ListOfEmployees();
                taskWindow.Show();
            }
            catch { }
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!Regex.IsMatch(ViewModel.EmpPhone, @"(\+375)-\d{2}-\d{3}-\d{2}-\d{2}"))
                {
                    MessageBox.Show("Ошибка ввода, номер должен быть в следующем формате: +375-XX-XXX-XX-XX , а у вас: " + ViewModel.EmpPhone);
                }
                else
                {
                  
                    ConfirmActionDialog confirmActionDialog = new ConfirmActionDialog();

                    if (ViewModel.IsNew)
                    {
                        try
                        {
                            Employee EmployeeFromEmployeeList = new Employee();

                            EmployeeFromEmployeeList.EmpFullName = ViewModel.EmpFullName;
                            EmployeeFromEmployeeList.EmpPersonId = ViewModel.EmpPersonId;
                            EmployeeFromEmployeeList.EmpPhone = ViewModel.EmpPhone;
                            EmployeeFromEmployeeList.EmpLogin = ViewModel.EmpLogin;
                            EmployeeFromEmployeeList.EmpHashPassword = ViewModel.EmpHashPassword;
                            EmployeeFromEmployeeList.EmpCreateDate = ViewModel.EmpCreateDate;
                            EmployeeFromEmployeeList.EmpDeleteDate = ViewModel.EmpDeleteDate;
                            EmployeeFromEmployeeList.EmpStatus = ViewModel.EmpStatus;


                            using (EmployeeRepos db = new EmployeeRepos())
                            {
<<<<<<< HEAD
                                db.AddPart(EmployeeFromEmployeeList);
=======
                                using (EmployeeContext db = new EmployeeContext())
                                {
                                    DateTime date = Convert.ToDateTime(employee.EmpCreateDate);
                                    int month = date.Month, year = date.Year, day = date.Day;
                                    string str = Convert.ToString(month) + "." + Convert.ToString(day) + "." + Convert.ToString(year);
                                    MessageBox.Show(str);
                                    db.AddPart(employee, str);
                                }
>>>>>>> parent of ddd02f0 (NEW)

                            }
                        }
                        catch { }
                    }

                    this.Close();
                    ListOfEmployees taskWindow = new ListOfEmployees();
                    taskWindow.Show();
                }
            }
            catch { }
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

        private void PhoneTextBlock_TextChanged(object sender, TextChangedEventArgs e)
        {
            string input = (sender as TextBox).Text;
            if (input != "")
            {
                if ((sender as TextBox).Text.Length == 1)
                {
                    if (!Regex.IsMatch(input, @"\+{1}"))
                    {
                        MessageBox.Show("Пожалуйста, вводите номер телефона как сказано в примере");
                    }
                }
                else
                {
                    if (!Regex.IsMatch(input, @"\+{1}[0-9\-]{1,17}"))
                    {
                        MessageBox.Show("Ошибка ввода, вводите пожалуйста цифры и символ -");
                    }
                }
            }
        }

        private void SatusBlock_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ViewModel.Employee.EmpStatus = (EmpStatus)Enum.Parse(typeof(EmpStatus), (string)SatusBlock.SelectedItem);
            }
            catch { }
        }

       
    }
}
