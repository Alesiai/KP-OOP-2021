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
using System.Text.RegularExpressions;
using Lab_6_7.Command;
using Lab_6_7.Repository;

namespace Lab_6_7.View
{
   
    public partial class OrderView : Window
    {
        float sale = 1;

        public OrderViewModel ViewModel
        { get; set; }

        public OrderView(OrderViewModel VM)
        {
            ViewModel = VM;

            if (ViewModel.IsNew)
            {
                ViewModel.Order.OrId = Order.size;
            }


            InitializeComponent();
            try
            {
                SaleTextBox.Text = Convert.ToString(ViewModel.Order.Cost - ViewModel.Order.Discount);


                ClientPhoneTextBox.Text = ViewModel.Order.Client.ClPhone;


                this.ProductList.ItemsSource = ProductViewModel.ListOfCoffee;
                this.DataGridXAML.ItemsSource = ViewModel.Order.ProductsInOrder;

                OrderStatus.ItemsSource = Enum.GetNames(typeof(OrderStatus));
                OrderStatus.SelectedItem = Convert.ToString(ViewModel.Order.OrderStatus);

                ListOfOrders.ACTIVE = false;
            }
            catch { }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            ConfirmActionDialog confirmActionDialog = new ConfirmActionDialog();

            if (confirmActionDialog.ShowDialog() == true)
            {
                if (ViewModel.IsNew)
                {
                    OrderViewModel.ListOfOrders.Remove(ViewModel.Order);
                }
                else
                {
                    
                    ViewModel.Order.OrderStatus = (OrderStatus)Enum.Parse(typeof(OrderStatus), "Deleted");
                }
            }
            this.Close();
            ListOfOrders taskWindow = new ListOfOrders();
            taskWindow.Show();
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ConfirmActionDialog confirmActionDialog = new ConfirmActionDialog();

            if (ViewModel.IsNew)
            {
                try
                {
                    Order OrderFormOrderList = new Order();

                    OrderFormOrderList.Employee = ViewModel.Order.Employee;
                    OrderFormOrderList.Discount = ViewModel.Order.Discount;
                    OrderFormOrderList.CreateDate = ViewModel.Order.CreateDate;
                    OrderFormOrderList.Cost = ViewModel.Order.Cost;
                    OrderFormOrderList.Time = ViewModel.Order.Time;
                    OrderFormOrderList.OrderStatus = ViewModel.Order.OrderStatus;


                    using (OrderRepos db = new OrderRepos())
                    {
                        db.AddPart(OrderFormOrderList);
                    }

                    foreach (ProductsInOrder productsInOrder in ViewModel.Order.ProductsInOrder)
                    {
                        using (ProductsInOrderRepos db = new ProductsInOrderRepos())
                        {
                            db.AddPart(productsInOrder);
                        }
                    }

                }
                catch { }
            }

            this.Close();
            ListOfOrders taskWindow = new ListOfOrders();
            taskWindow.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button pressed = (Button)sender;
                if (pressed.Content != null)
                {
                    try
                    {
                        switch (pressed.Content.ToString())
                        {
                            case "Кофе":
                                ProductList.ItemsSource = ProductViewModel.ListOfCoffee;
                                break;
                            case "Чай":
                                ProductList.ItemsSource = ProductViewModel.ListOfTea;
                                break;
                            case "Закуски":
                                ProductList.ItemsSource = ProductViewModel.ListOfSnacks;
                                break;
                            case "Десерты":
                                ProductList.ItemsSource = ProductViewModel.ListOfDesserts;
                                break;
                            default:
                                ProductList.ItemsSource = ProductViewModel.ListOfCoffee;
                                break;
                        } }  catch { }
                }
            }
            catch { }
        }

       
        Product device;

        private void ProductList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ListBox dg = sender as ListBox;
                device = dg.SelectedItems[0] as Product;
            }
            catch { }
        }

        private void ListBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            ProductsCount productCountWindow = new ProductsCount();
            if (productCountWindow.ShowDialog() == true)
            {
                try
                {
                    ProductsInOrder productsInOrder = new ProductsInOrder();

                    productsInOrder.ProductId = device.ProductId;
                    productsInOrder.ProductName = device.ProductName;
                    productsInOrder.OrderId = ViewModel.Order.OrId;
                    productsInOrder.Price = device.ProdCost;
                    productsInOrder.NumOfPos = ProductsCount.ProdCount;

                    ViewModel.Order.ProductsInOrder.Add(productsInOrder);
                    
                    ProductsInOrderViewModel.ListOfProductsInOrders.Add(productsInOrder);


                    ViewModel.Order.Cost = 0;
                    foreach (ProductsInOrder produts in ViewModel.Order.ProductsInOrder)
                    {
                        ViewModel.Order.Cost += produts.Price * produts.NumOfPos;
                    }

                    if (!ViewModel.IsNew)
                    {
                        using (ProductsInOrderRepos db = new ProductsInOrderRepos())
                        {
                            db.AddPart(productsInOrder);
                        }
                    }

                    ViewModel.Order.Discount = ViewModel.Order.Cost * (decimal)sale;
                    SaleTextBox.Text = Convert.ToString(ViewModel.Order.Cost * (decimal)(1 - sale));


                    this.DataGridXAML.ItemsSource = EmployeeViewModel.ListOfEmployees;
                    this.DataGridXAML.ItemsSource = ViewModel.Order.ProductsInOrder;
                }
                catch { }
            }
        }


        ProductsInOrder row;
        private void DataGridXAML_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid dg = sender as DataGrid;
                row = dg.SelectedItems[0] as ProductsInOrder;

                ViewModel.Order.Cost = 0;
                foreach (ProductsInOrder produts in ViewModel.Order.ProductsInOrder)
                {
                    ViewModel.Order.Cost += produts.Price * produts.NumOfPos;
                }
                
                ViewModel.Order.Discount = ViewModel.Order.Cost * (decimal)sale;
                SaleTextBox.Text = Convert.ToString(ViewModel.Order.Cost * (decimal)(1 - sale));
            }
            catch { }
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewModel.Order.ProductsInOrder.Remove(row);
                ProductsInOrderViewModel.ListOfProductsInOrders.Remove(row);

                ViewModel.Order.Cost = 0;
                foreach (ProductsInOrder produts in ViewModel.Order.ProductsInOrder)
                {
                    ViewModel.Order.Cost += produts.Price * produts.NumOfPos;
                }

                ViewModel.Order.Discount = ViewModel.Order.Cost * (decimal)sale;
                SaleTextBox.Text = Convert.ToString(ViewModel.Order.Cost * (decimal)(1 - sale));

                this.DataGridXAML.ItemsSource = EmployeeViewModel.ListOfEmployees;
                this.DataGridXAML.ItemsSource = ViewModel.Order.ProductsInOrder;
            }
            catch { }

        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            bool IsSearched = false;
            if (!Regex.IsMatch(ClientPhoneTextBox.Text, @"(\+375)-\d{2}-\d{3}-\d{2}-\d{2}"))
            {
                MessageBox.Show("Ошибка ввода, номер должен быть в следующем формате: +375-XX-XXX-XX-XX , а у вас: " + ClientPhoneTextBox.Text);
            }
            else
            {
                try
                {
                    foreach (Client client in ClientViewModel.ListOfClients)
                    {
                        try
                        {
                            if (ClientPhoneTextBox.Text == client.ClPhone)
                            {
                                try
                                {
                                    ViewModel.Order.Client = client;
                                    MessageBox.Show("Найден клиент: " + client.ClFullName);
                                    IsSearched = true;
                                }
                                catch { }
                            }
                        }
                        catch { }
                    }
                }
                catch { }

                if (IsSearched == false) { 
                    MessageBox.Show("Клиент не найден");
                    try
                    {
                        foreach (Client client in ClientViewModel.ListOfClients)
                        {
                            if (client.ClPersonId == 3)
                            {
                                ViewModel.Order.Client = client;

                            }
                        }
                    }
                    catch 
                    {
                        Client client = new Client();
                        client.ClFullName = "Клиент";
                        client.ClPhone = "+375-44-444-44-44";
                        ViewModel.Order.Client = client;
                    }
                }
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                RadioButton pressed = (RadioButton)sender;
                if (pressed.Content!=null) 
                {
                    try
                    {
                        switch (pressed.Content.ToString())
                        {
                            case "0%":
                                sale = 1f;
                                break;
                            case "10%":
                                sale = 0.9f;
                                break;
                            case "20%":
                                sale = 0.8f;
                                break;
                            case "50%":
                                sale = 0.5f;
                                break;
                            case "100%":
                                sale = 0f;
                                break;
                            default:
                                sale = 1f;
                                break;
                        }
                    }
                    catch { }
                    ViewModel.Order.Discount = Math.Round(ViewModel.Order.Cost * (decimal)sale,2);
                    SaleTextBox.Text = Convert.ToString(Math.Round(ViewModel.Order.Cost * (decimal)(1 - sale),2));
                }
                
            }
            catch { }

        }

        private void ClientPhoneTextBox_TextChanged(object sender, TextChangedEventArgs e)
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
            }
        }

        private void SatusBlock_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.Order.OrderStatus = (OrderStatus)Enum.Parse(typeof(OrderStatus), (string)OrderStatus.SelectedItem);
        }
    }
}
