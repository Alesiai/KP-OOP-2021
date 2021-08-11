using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lab_6_7.Model;
using Lab_6_7.View;
using Lab_6_7.Command;
using Lab_6_7.Repository;
using Lab_6_7.CaffeDbContext;
using System.Windows.Input;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.ComponentModel;

namespace Lab_6_7.ViewModel
{
    public class OrderViewModel : ViewModelBase
    {
        public bool IsNew
        {
            get;
            set;
        }
        public Order Order
        {
            get; set;
        }

        public OrderViewModel(Order order)
        {
            this.Order = order;
        }

        //get;set
        #region   
        public Employee Employee
        {
            get { return Order.Employee; }
            set
            {
                Order.Employee = value;
                OnPropertyChanged("Employee");
            }
        }
        public string Time
        {
            get { return Order.Time; }
            set
            {
                Order.Time = value;
                OnPropertyChanged("Time");
            }
        }

        public Client Client
        {
            get { return Order.Client; }
            set
            {
                Order.Client = value;
                OnPropertyChanged("Client");
            }
        }

        public int OrId
        {
            get { return Order.OrId; }
            set
            {
                Order.OrId = value;
                OnPropertyChanged("OrId");
            }
        }


        public OrderStatus OrderStatus
        {
            get { return Order.OrderStatus; }
            set
            {
                Order.OrderStatus = value;
                OnPropertyChanged("OrderStatus");
            }
        }

        public DateTime? CreateDate
        {
            get { return Order.CreateDate; }
            set
            {
                Order.CreateDate = value;
                OnPropertyChanged("EmpCreateDate");
            }
        }



        public decimal Discount
        {
            get { return Order.Discount; }
            set
            {
                Order.Discount = value;
                OnPropertyChanged("Discount");
            }
        }

        public decimal Cost
        {
            get { return Order.Cost; }
            set
            {
                Order.Cost = value;
                OnPropertyChanged("Cost");
            }
        }

        public virtual IList<ProductsInOrder> ProductsInOrder
        {
            get { return Order.ProductsInOrder; }
            set
            {
                Order.ProductsInOrder = value;
                OnPropertyChanged("ProductsInOrder");
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

        public static ObservableCollection<Order> ListOfOrders = new ObservableCollection<Order>();
        public static void OrderInit()
        {

            using (OrderRepos db = new OrderRepos())
            {
                ListOfOrders = db.GetParts();
            }

            CV = CollectionViewSource.GetDefaultView(ListOfOrders);
        }

        public static void SeachList(string searchpram)
        {
            try
            {
                CV.Filter = obj => string.IsNullOrEmpty(searchpram) || obj is Order Order &&
            (
            Order.OrId.ToString().ToLower().Contains(searchpram.ToLower()) ||
            Order.Client.ClFullName.ToString().ToLower().Contains(searchpram.ToLower()) ||
            Order.Cost.ToString().ToLower().Contains(searchpram.ToLower()) ||
            Order.Time.ToString().ToLower().Contains(searchpram.ToLower()) ||
            Order.OrderStatus.ToString().ToLower().Contains(searchpram.ToLower())
            );
                CV.Refresh();
            }
            catch { }
        }


    }
}
