using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_6_7.Model
{
    public class Client : BindableBase
    {
        private int _personId;
        private string _fullName;
        private string _phone;
        public static int size=1;

        public int ClPersonId
        {
            get => _personId;
            set
            {
                if (value == _personId) return;
                _personId = value;
                NotifyPropertyChanged();
                size++;
            }
        }

        public string ClFullName
        {
            get => _fullName;
            set
            {
                if (value == _fullName) return;
                _fullName = value;
                NotifyPropertyChanged();
            }
        }

        public string ClPhone
        {
            get => _phone;
            set
            {
                if (value == _phone) return;
                _phone = value;
                NotifyPropertyChanged();
            }
        }

    }
}
