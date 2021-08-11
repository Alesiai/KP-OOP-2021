using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Lab_6_7.Model
{
    public enum EmpStatus
    {
        [Description("Администратор")]
        Admin,
        [Description("Бариста")]
        Barista,
        [Description("Стажер")]
        Intern,
    }

    public class Employee : BindableBase
    {
        private int _personId;
        private string _fullName;
        private string _phone;
        private string _login;
        private string _hashPassword;
        private DateTime? _createDate;
        private EmpStatus _empStatus;
        private DateTime? _deleteDate;
        public static int size=1;
        public int EmpPersonId
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

        public string EmpFullName
        {
            get => _fullName;
            set
            {
                if (value == _fullName) return;
                _fullName = value;
                NotifyPropertyChanged();
            }
        }

        public string EmpPhone
        {
            get => _phone;
            set
            {
                if (value == _phone) return;
                _phone = value;
                NotifyPropertyChanged();
            }
        }

        public string EmpHashPassword
        {
            get => _hashPassword;
            set
            {
                if (value == _hashPassword) return;
                _hashPassword = value;
                NotifyPropertyChanged();
            }
        }

        public string EmpLogin
        {
            get => _login;
            set
            {
                if (value == _login) return;
                _login = value;
                NotifyPropertyChanged();
            }
        }

        public DateTime? EmpCreateDate
        {
            get => _createDate;
            set
            {
                if (value.Equals(_createDate)) return;
                _createDate = value;
                NotifyPropertyChanged();
            }
        }

        public DateTime? EmpDeleteDate
        {
            get => _deleteDate;
            set
            {
                if (value.Equals(_deleteDate)) return;
                _deleteDate = value;
                NotifyPropertyChanged();
            }
        }

        public EmpStatus EmpStatus
        {
            get => _empStatus;
            set
            {
                if (value == _empStatus) return;
                _empStatus = value;
                NotifyPropertyChanged();
            }
        }


        public Employee() { }

        public Employee(int empPersonId, string empFullName, string empPhone, EmpStatus empStatus, 
          string empLogin, string empHashPassword, DateTime? empCreateDate, DateTime? empDeleteDate)
        {
            EmpPersonId = empPersonId;
            EmpFullName = empFullName;
            EmpPhone = empPhone;
            EmpHashPassword = empHashPassword;
            EmpLogin = empLogin;
            EmpCreateDate = empCreateDate;
            EmpDeleteDate = empDeleteDate;
            EmpStatus = empStatus;
        }
    
    
    }

    
}
