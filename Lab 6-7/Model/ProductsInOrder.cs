using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_6_7.Model
{
    public class ProductsInOrder:BindableBase
    {
        
        private int _productId;
       
        private string _productName;
        private int _orderId;
        private decimal _price;
        private int _numOfPos;
        public static int size = 1;

       
        public int ProductId
        {
            get => _productId;
            set
            {
                if (value == _productId) return;
                _productId = value;
                NotifyPropertyChanged();
            }
        }

        public string ProductName
        {
            get => _productName;
            set
            {
                if (value == _productName) return;
                _productName = value;
                NotifyPropertyChanged();
            }
        }

        public int OrderId
        {
            get => _orderId;
            set
            {
                if (value == _orderId) return;
                _orderId = value;
                NotifyPropertyChanged();
            }
        }

        public decimal Price
        {
            get => _price;
            set
            {
                if (value == _price) return;
                _price = value;
                NotifyPropertyChanged();
            }
        }


        public int NumOfPos
        {
            get => _numOfPos;
            set
            {
                if (value.Equals(_numOfPos)) return;
                _numOfPos = value;
                NotifyPropertyChanged();
            }
        }
       
    }
}
