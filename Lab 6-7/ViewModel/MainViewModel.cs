using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Lab_6_7.Model;
using Lab_6_7.ViewModel;

namespace Lab_6_7.ViewModel
{
    public class MainViewModel
    {
        public ObservableCollection<EmployeeViewModel> EmployeeList { get; set; }
        public ObservableCollection<ProductViewModel> ProductList { get; set; }
        public ObservableCollection<ClientViewModel> ClientList { get; set; }
        public ObservableCollection<OrderViewModel> OrderList { get; set; }
        public ObservableCollection<ProductsInOrderViewModel> ProductsInOrderList { get; set; }

        #region Constructor

        public MainViewModel(ObservableCollection<Employee> employee)
        {
            EmployeeList = new ObservableCollection<EmployeeViewModel>(employee.Select(e => new EmployeeViewModel(e)));
        }

        public MainViewModel(ObservableCollection<Product> product)
        {
            ProductList = new ObservableCollection<ProductViewModel>(product.Select(p => new ProductViewModel(p)));
        }

        public MainViewModel(ObservableCollection<Client> client)
        {
            ClientList = new ObservableCollection<ClientViewModel>(client.Select(c => new ClientViewModel(c)));
        }

        public MainViewModel(ObservableCollection<Order> order)
        {
            OrderList = new ObservableCollection<OrderViewModel>(order.Select(o => new OrderViewModel(o)));
        }

        public MainViewModel(ObservableCollection<ProductsInOrder> productsInOrder)
        {
            ProductsInOrderList = new ObservableCollection<ProductsInOrderViewModel>(productsInOrder.Select(o => new ProductsInOrderViewModel(o)));
        }

        #endregion
    }
}
