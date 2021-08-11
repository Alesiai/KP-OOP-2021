using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lab_6_7.Model;
using Lab_6_7.View;
using Lab_6_7.Command;
using Lab_6_7.CaffeDbContext;
using Lab_6_7.Repository;
using System.Windows.Input;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.ComponentModel;

namespace Lab_6_7.ViewModel
{
    public class ProductViewModel : ViewModelBase
    {
        public bool IsNew
        {
            get;
            set;
        }
        public Product Product
        {
            get; set;
        }

        public ProductViewModel(Product product)
        {
            this.Product = product;
        }

        //get;set
        #region 

        public ProductType Type
        {
            get { return Product.Type; }
            set
            {
                Product.Type = value;
                OnPropertyChanged("Type");
            }
        }

        public int ProductId
        {
            get { return Product.ProductId; }
            set
            {
                Product.ProductId = value;
                OnPropertyChanged("ProductId");
            }
        }

        public string ProductName
        {
            get { return Product.ProductName; }
            set
            {
                Product.ProductName = value;
                OnPropertyChanged("ProductName");
            }
        }

        public string ProductDescription
        {
            get { return Product.ProductDescription; }
            set
            {
                Product.ProductDescription = value;
                OnPropertyChanged("ProductDescription");
            }
        }

        public float ProdCost
        {
            get { return Product.ProdCost; }
            set
            {
                Product.ProdCost = value;
                OnPropertyChanged("ProdCost");
            }
        }

        
        public DateTime? DeleteDate
        {
            get { return Product.DeleteDate; }
            set
            {
                Product.DeleteDate = value;
                OnPropertyChanged("EmpCreateDate");
            }
        }
        #endregion

        static ICollectionView collectionView;
        public static ICollectionView CV
        {
            get => collectionView;
            set
            {
                collectionView = value;

            }

        }
        
        public static ObservableCollection<Product> ListOfProducts = new ObservableCollection<Product>();
        
        public static ObservableCollection<Product> ListOfCoffee  ;
        public static ObservableCollection<Product> ListOfTea     ;
        public static ObservableCollection<Product> ListOfDesserts;
        public static ObservableCollection<Product> ListOfSnacks  ;
        public static void ProductInit()
        {
            ListOfCoffee = new ObservableCollection<Product>();
            ListOfTea = new ObservableCollection<Product>();
            ListOfDesserts = new ObservableCollection<Product>();
            ListOfSnacks = new ObservableCollection<Product>();

            using (ProductRepos db = new ProductRepos())
            {
                ListOfProducts = db.GetParts();
            }
            CV = CollectionViewSource.GetDefaultView(ListOfProducts);
        }

        public static void SeachList(string searchpram)
        {
            try
            {
                CV.Filter = obj => string.IsNullOrEmpty(searchpram) || obj is Product product &&
            (
            product.ProductName.ToLower().Contains(searchpram.ToLower()) ||
            product.ProductDescription.ToLower().Contains(searchpram.ToLower()) ||
            product.ProductId.ToString().ToLower().Contains(searchpram.ToLower()) ||
            product.ProdCost.ToString().ToLower().Contains(searchpram.ToLower()) ||
            product.Type.ToString().ToLower().Contains(searchpram.ToLower())
            );
                CV.Refresh();
                    }
            catch { }
        }
    }
}