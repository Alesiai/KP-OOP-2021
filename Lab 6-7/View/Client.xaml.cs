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
using Lab_6_7.ViewModel;
using Lab_6_7.CaffeDbContext;
using Lab_6_7.Repository;
using System.Text.RegularExpressions;

using Lab_6_7.Command;

namespace Lab_6_7.View
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class ClientView : Window
    {
        //public MainViewModel ViewModel { get; set; }

        public ClientViewModel ViewModel
        { get; set; }

        public static int ID;
        public ClientView(ClientViewModel VM)
        {
            ViewModel = VM;

            if (ViewModel.IsNew)
            {
                ViewModel.Client.ClPersonId = Client.size;
            }


            InitializeComponent();

            ListOfClients.ACTIVE = false;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ConfirmActionDialog confirmActionDialog = new ConfirmActionDialog();

                if (confirmActionDialog.ShowDialog() == true)
                {
                    ClientViewModel.ListOfClients.Remove(ViewModel.Client);
                }

                if (!ViewModel.IsNew)
                {
                    try
                    {
                        Client ClientFromClientList = new Client();

                        ClientFromClientList.ClFullName = ViewModel.Client.ClFullName;
                        ClientFromClientList.ClPersonId = ViewModel.Client.ClPersonId;
                        ClientFromClientList.ClPhone = ViewModel.Client.ClPhone;


                        using (ClientRepos db = new ClientRepos())
                        {
                            db.Delete(ClientFromClientList.ClPersonId, ClientFromClientList);

                        }
                    }
                    catch { }
                }

                this.Close();
                ListOfClients taskWindow = new ListOfClients();
                taskWindow.Show();
            }
            catch { MessageBox.Show("В удалении проищошла ошибка"); }
           
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (!Regex.IsMatch(PhoneTextBlock.Text, @"(\+375)-\d{2}-\d{3}-\d{2}-\d{2}"))
                {
                    MessageBox.Show("Ошибка ввода, номер должен быть в следующем формате: +375-XX-XXX-XX-XX , а у вас: " + PhoneTextBlock.Text);
                }
                else
                {

                    ConfirmActionDialog confirmActionDialog = new ConfirmActionDialog();

                    if (ViewModel.IsNew)
                    {
                        try
                        {
                            Client ClientFromClientList = new Client();

                            ClientFromClientList.ClFullName = ViewModel.Client.ClFullName;
                            ClientFromClientList.ClPersonId = ViewModel.Client.ClPersonId;
                            ClientFromClientList.ClPhone = ViewModel.Client.ClPhone;



                            using (ClientRepos db = new ClientRepos())
                            {
                                db.AddPart(ClientFromClientList);

                            }
                        }
                        catch { }
                    }
                    if (NameTextBlock.Text == "" || PhoneTextBlock.Text == "")
                    {
                        MessageBox.Show("Вы ввели не всю информацию");
                    }

                    this.Close();
                    ListOfClients taskWindow = new ListOfClients();
                    taskWindow.Show();
                }
            }
            catch { MessageBox.Show("В сохранении произошла ошибка"); }
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
                    if (!Regex.IsMatch(input, @"\+{1}[0-9\-]{1,17}"))
                    {
                        MessageBox.Show("Пожалуйста, вводите номер телефона как сказано в примере");
                    }
                }
            }
        }

    }
}
