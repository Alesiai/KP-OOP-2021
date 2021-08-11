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
    /// Логика взаимодействия для ListOfClients.xaml
    /// </summary>
    public partial class ListOfClients : Window
    {
        public static MainViewModel ViewModel { get; set; }
        public static bool ACTIVE;


        public ListOfClients()
        {
            ACTIVE = true;
            ViewModel = new ViewModel.MainViewModel(ClientViewModel.ListOfClients); // Создали ViewModel 

            InitializeComponent();

            this.DataGridXAML.ItemsSource = OrderViewModel.ListOfOrders;
            this.DataGridXAML.ItemsSource = ClientViewModel.ListOfClients;

        }



        Client row;

        //Save Client's ID
        private void DataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (ACTIVE == true)
                {
                    try
                    {
                        DataGrid dg = sender as DataGrid;
                        row = dg.SelectedItems[0] as Client;
                    }
                    catch { }
                }
            }
            catch { }
        }

        //Opening Client's card
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ClientView taskWindow = new ClientView(new ClientViewModel(row));
                taskWindow.Show();
                this.Close();
            }
            catch { }
        }

        //Add Client
        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newEmp = new Client();
                ClientView taskWindow = new ClientView(new ClientViewModel(newEmp) { IsNew = true });

                ClientViewModel.ListOfClients.Add(newEmp);
                taskWindow.Show();
                this.Close();
            }
            catch { }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UpdateClients_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (Client Client in ClientViewModel.ListOfClients)
                {
                    using (ClientRepos db = new ClientRepos())
                    {
                        db.Update(Client.ClPersonId, Client);
                    }
                }
                ClientViewModel.ClientInit();

                MessageBox.Show("Данные обновлены");
                this.DataGridXAML.ItemsSource = OrderViewModel.ListOfOrders;
                this.DataGridXAML.ItemsSource = ClientViewModel.ListOfClients;

                ViewModel = new ViewModel.MainViewModel(ClientViewModel.ListOfClients); // Создали ViewModel 
            }
            catch { }

        }

        private void SearchControl_SearchTextChanged(string search)
        {
            //  MessageBox.Show((searchControl.SearchText is null).ToString());
            try 
            {
                if (searchControl is null)
                {
                    this.DataGridXAML.ItemsSource = ClientViewModel.ListOfClients;
                }
                ClientViewModel.SeachList(search);
                this.DataGridXAML.ItemsSource = ClientViewModel.CV;
            }
            catch 
            {
                this.DataGridXAML.ItemsSource = ClientViewModel.ListOfClients;
            }
           
        }
    }
}

