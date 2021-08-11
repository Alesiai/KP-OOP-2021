using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab_6_7.View;
using Lab_6_7.ViewModel;

namespace Lab_6_7.Model
{
    public enum OrderStatus
    {
        Taked,
        Deleted,
        Closed,
    }

    public class Order : BindableBase
    {
        private Employee _employee;
        private Client _client;
        private int _orderId;
        private DateTime? _createDate;
        private string _time;
        private OrderStatus _orderStatus;
        private decimal _discount;
        private decimal _cost;

        private IList<ProductsInOrder> _productsInOrder;
        public static int size = 1;

        public Employee Employee
        {
            get => _employee;
            set
            {
                if (value == _employee) return;
                _employee = value;
                NotifyPropertyChanged();
                
            }
        }

        public string Time
        {
            get => _time;
            set
            {
                if (value == _time) return;
                _time = value;
                NotifyPropertyChanged();
            }
        }

        public Client Client
        {
            get => _client;
            set
            {
                if (value == _client) return;
                _client = value;
                NotifyPropertyChanged();
            }
        }

        public int OrId
        {
            get => _orderId;
            set
            {
                if (value == _orderId) return;
                _orderId = value;
                NotifyPropertyChanged();
                size++;
            }
        }

        public OrderStatus OrderStatus
        {
            get => _orderStatus;
            set
            {
                if (value == _orderStatus) return;
                _orderStatus = value;
                NotifyPropertyChanged();
            }
        }

        public DateTime? CreateDate
        {
            get => _createDate;
            set
            {
                if (value.Equals(_createDate)) return;
                _createDate = value;
                NotifyPropertyChanged();
            }
        }

        public decimal Discount
        {
            get => _discount;
            set
            {
                if (value == _discount) return;
                _discount = value;
                NotifyPropertyChanged();
            }
        }

        public decimal Cost
        {
            get => _cost;
            set
            {
                if (value == _cost) return;
                _cost = value;
                NotifyPropertyChanged();
            }
        }

        public virtual IList<ProductsInOrder> ProductsInOrder
        {
            get => _productsInOrder;
            set
            {
                if (Equals(value, _productsInOrder)) return;
                _productsInOrder = value;
                NotifyPropertyChanged();
            }
        }

        public Order()
        {
            this.ProductsInOrder = new List<ProductsInOrder>();
            this.CreateDate = DateTime.Now;

            DateTime date = Convert.ToDateTime(this.CreateDate);
            int Hour = date.Hour, Minute = date.Minute, Second = date.Second;
            string str = Convert.ToString(Hour) + ":" + Convert.ToString(Minute) + ":" + Convert.ToString(Second);

            this.Time = str;
            foreach (Client client in ClientViewModel.ListOfClients) 
            {
                if (client.ClPersonId == 3) 
                {
                    this.Client = client;
                }
            }
        }

    }
}
