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
    public class ProductsInOrderViewModel : ViewModelBase
    {
        public ProductsInOrder ProductsInOrder;

        public ProductsInOrderViewModel(ProductsInOrder ProductsInOrder)
        {
            this.ProductsInOrder = ProductsInOrder;
        }

        //get;set
        #region 

        public int ProductId
        {
            get { return ProductsInOrder.ProductId; }
            set
            {
                ProductsInOrder.ProductId = value;
                OnPropertyChanged("ProductId");
            }
        }

        public string ProductName
        {
            get { return ProductsInOrder.ProductName; }
            set
            {
                ProductsInOrder.ProductName = value;
                OnPropertyChanged("EmpFullName");
            }
        }

        public int OrderId
        {
            get { return ProductsInOrder.OrderId; }
            set
            {
                ProductsInOrder.OrderId = value;
                OnPropertyChanged("OrderId");
            }
        }

        public decimal Price
        {
            get { return ProductsInOrder.Price; }
            set
            {
                ProductsInOrder.Price = value;
                OnPropertyChanged("Price");
            }
        }

        public int NumOfPos
        {
            get { return ProductsInOrder.NumOfPos; }
            set
            {
                ProductsInOrder.NumOfPos = value;
                OnPropertyChanged("NumOfPos");
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

        public static ObservableCollection<ProductsInOrder> ListOfProductsInOrders = new ObservableCollection<ProductsInOrder>();
        public static void ProductsInOrderInit()
        {

            using (ProductsInOrderRepos db = new ProductsInOrderRepos())
            {
                ListOfProductsInOrders = db.GetParts();

            }
        }
    }
}
