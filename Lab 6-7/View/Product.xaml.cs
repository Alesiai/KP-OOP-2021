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
using Lab_6_7.Command;
using System.Text.RegularExpressions;

namespace Lab_6_7.View
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class ProductView : Window
    {
        //public MainViewModel ViewModel { get; set; }

        public ProductViewModel ViewModel
        { get; set; }

        public static int ID;
        public ProductView(ProductViewModel VM)
        {
            ViewModel = VM;

            if (ViewModel.IsNew)
            {
                ViewModel.Product.ProductId = Product.size;
            }


<<<<<<< HEAD
            InitializeComponent();
=======
            foreach (Product product in ProductViewModel.ListOfProducts)
            {
                if (product.ProductId == ID)
                {
                    IdTextBlock.Text = Convert.ToString(product.ProductId);
                    NameTextBlock.Text = Convert.ToString(product.ProductName);
                    SatusBlock.Text = Convert.ToString(product.Type);
                    CostTextBlock.Text = Convert.ToString(product.ProdCost);
                }
>>>>>>> parent of ddd02f0 (NEW)

            SatusBlock.ItemsSource = Enum.GetNames(typeof(ProductType));
            SatusBlock.SelectedItem = Convert.ToString(ViewModel.Product.Type);
            ListOfProducts.ACTIVE = false;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            ConfirmActionDialog confirmActionDialog = new ConfirmActionDialog();

            if (confirmActionDialog.ShowDialog() == true)
            {
                ProductViewModel.ListOfProducts.Remove(ViewModel.Product);
                switch (ViewModel.Product.Type)
                {
                    case ProductType.Coffee:
                        ProductViewModel.ListOfCoffee.Remove(ViewModel.Product);
                        break;
                    case ProductType.Tea:
                        ProductViewModel.ListOfTea.Remove(ViewModel.Product);
                        break;
                    case ProductType.Snacks:
                        ProductViewModel.ListOfSnacks.Remove(ViewModel.Product);
                        break;
                    case ProductType.Desert:
                        ProductViewModel.ListOfDesserts.Remove(ViewModel.Product);
                        break;
                    default:
                       
                        break;
                }

                if (!ViewModel.IsNew)
                {
                    Product ProductFromProductList = new Product();

                    ProductFromProductList.ProductName = ViewModel.ProductName;
                    ProductFromProductList.ProductDescription = ViewModel.ProductDescription;
                    ProductFromProductList.ProdCost = ViewModel.ProdCost;
                    ProductFromProductList.ProductId = ViewModel.ProductId;


                    using (ProductRepos db = new ProductRepos())
                    {
                        db.Delete(ProductFromProductList.ProductId, ProductFromProductList);

                    }
                }
            }
            this.Close();
            ListOfProducts taskWindow = new ListOfProducts();
            taskWindow.Show();
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(TextBlockCost.Text, @"^\d+(.\d{1,2})?$"))
            {
                MessageBox.Show("Формат ввода: XX.XX");
            }
            else
            {
                ConfirmActionDialog confirmActionDialog = new ConfirmActionDialog();

                switch (ViewModel.Product.Type)
                {
<<<<<<< HEAD
                    case ProductType.Coffee:
                        ProductViewModel.ListOfCoffee.Add(ViewModel.Product);
                        break;
                    case ProductType.Tea:
                        ProductViewModel.ListOfTea.Add(ViewModel.Product);
                        break;
                    case ProductType.Snacks:
                        ProductViewModel.ListOfSnacks.Add(ViewModel.Product);
                        break;
                    case ProductType.Desert:
                        ProductViewModel.ListOfDesserts.Add(ViewModel.Product);
                        break;
                    default:

                        break;
                }


                if (ViewModel.IsNew)
                {
                    Product ProductFromProductList = new Product();

                    ProductFromProductList.ProductName = ViewModel.ProductName;
                    ProductFromProductList.ProductDescription = ViewModel.ProductDescription;
                    ProductFromProductList.ProdCost = ViewModel.ProdCost;
                    ProductFromProductList.ProductId = ViewModel.ProductId;


                    using (ProductRepos db = new ProductRepos())
                    {
                        db.AddPart(ProductFromProductList);

=======

                    foreach (Product product in ProductViewModel.ListOfProducts)
                    {
                        if (ID == product.ProductId)
                        {
                            //employee.EmpFullName = NameTextBlock.Text;
                            //employee.EmpPhone = PhoneTextBlock.Text;
                            //employee.EmpStatus = (EmpStatus)Enum.Parse(typeof(EmpStatus), SatusBlock.Text);
                            //employee.EmpLogin = LoginTextBlock.Text;
                            //employee.EmpHashPassword = PasswordTextBlock.Text;


                            if (ListOfProducts.IsNew == true)
                            {
                                using (ProductContext db = new ProductContext())
                                {
                                    db.AddPart(product);
                                }

                            }
                        }
>>>>>>> parent of ddd02f0 (NEW)
                    }
                }

                this.Close();
                ListOfProducts taskWindow = new ListOfProducts();
                taskWindow.Show();
            }
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
            if ((sender as TextBox).Text.Length >= 51)
            {
                 MessageBox.Show("Слишком длинный текст");
            }
        }

        private void CostTextBlock_TextChanged(object sender, TextChangedEventArgs e)
        {
            string input = (sender as TextBox).Text;
            if (input != "")
            {
                
                if (!Regex.IsMatch(input, @"[0-9\.]{1,5}"))
                {
                    MessageBox.Show("Формат ввода: XX.XX");
                }
            }
        }

        private void SatusBlock_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.Type = (ProductType)Enum.Parse(typeof(ProductType), (string)SatusBlock.SelectedItem);
        }

        private void HistoryButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LoginTextBlock_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
