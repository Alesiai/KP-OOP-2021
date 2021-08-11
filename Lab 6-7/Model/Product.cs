using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_6_7.Model
{
    public enum ProductType
    {
        Coffee,
        Tea, 
        Desert,
        Snacks
    }

    public class Product:BindableBase
    {
        private int _productId;
        private string _productName;
        private float _prodCost;
        private DateTime? _deleteDate;
        private ProductType _type;
        private string _productDescription;
        public static int size = 1;

        public ProductType Type
        {
            get => _type;
            set
            {
                if (value == _type) return;
                _type = value;
                NotifyPropertyChanged();
            }
        }

        public int ProductId
        {
            get => _productId;
            set
            {
                if (value == _productId) return;
                _productId = value;
                NotifyPropertyChanged();
                size++;
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

        public string ProductDescription
        {
            get => _productDescription;
            set
            {
                if (value == _productDescription) return;
                _productDescription = value;
                NotifyPropertyChanged();
            }
        }

        public float ProdCost
        {
            get => _prodCost;
            set
            {
                if (value == _prodCost) return;
                _prodCost = value;
                NotifyPropertyChanged();
            }
        }

        public DateTime? DeleteDate
        {
            get => _deleteDate;
            set
            {
                if (value.Equals(_deleteDate)) return;
                _deleteDate = value;
                NotifyPropertyChanged();
            }
        }
    }
}
