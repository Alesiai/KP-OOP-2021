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
    /// Логика взаимодействия для ListOfProducts.xaml
    /// </summary>
    public partial class ListOfProducts : Window
    {
        public static MainViewModel ViewModel { get; set; }
        public static bool ACTIVE;


        public ListOfProducts()
        {
            ACTIVE = true;
            ViewModel = new ViewModel.MainViewModel(ProductViewModel.ListOfProducts); // Создали ViewModel 

            InitializeComponent();
            this.DataGridXAML.ItemsSource = OrderViewModel.ListOfOrders;
            this.DataGridXAML.ItemsSource = ProductViewModel.ListOfProducts;

        }

        Product row;

        //Save Product's ID
        private void DataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (ACTIVE == true)
                {
                    try
                    {
                        DataGrid dg = sender as DataGrid;
                        row = dg.SelectedItems[0] as Product;
                    }
                    catch { }
                }
            }
            catch { }
        }

        //Opening Product's card
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ProductView taskWindow = new ProductView(new ProductViewModel(row));
                taskWindow.Show();
                this.Close();
            }
            catch { }
        }

        //Add Product
        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newEmp = new Product();
                ProductView taskWindow = new ProductView(new ProductViewModel(newEmp) { IsNew = true });

                ProductViewModel.ListOfProducts.Add(newEmp);
                taskWindow.Show();
                this.Close();
            }
            catch { }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UpdateProducts_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (Product Product in ProductViewModel.ListOfProducts)
                {
                    using (ProductRepos db = new ProductRepos())
                    {
                        db.Update(Product.ProductId, Product);
                    }
                }
                ProductViewModel.ProductInit();

                MessageBox.Show("Данные обновлены");
                this.DataGridXAML.ItemsSource = OrderViewModel.ListOfOrders;
                this.DataGridXAML.ItemsSource = ProductViewModel.ListOfProducts;

                ViewModel = new ViewModel.MainViewModel(ProductViewModel.ListOfProducts); // Создали ViewModel 
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

                    this.DataGridXAML.ItemsSource = ProductViewModel.ListOfProducts;
                }
                ProductViewModel.SeachList(search);
                this.DataGridXAML.ItemsSource = ProductViewModel.CV;
            }
            catch 
            {
                this.DataGridXAML.ItemsSource = ProductViewModel.ListOfProducts;
            }
        }
    }
}

