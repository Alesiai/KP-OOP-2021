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
    public class EmployeeViewModel : ViewModelBase
    {
        public bool IsNew
        {
            get;
            set;
        }
        public Employee Employee
        {
            get; set;
        }

        public EmployeeViewModel(Employee employee)
        {
            this.Employee = employee;
        }

        //get;set
        #region 
        public int EmpPersonId
        {
            get { return Employee.EmpPersonId; }
            set
            {
                Employee.EmpPersonId = value;
                OnPropertyChanged("EmpPersonId");
            }
        }

        public string EmpFullName
        {
            get { return Employee.EmpFullName; }
            set
            {
                Employee.EmpFullName = value;
                OnPropertyChanged("EmpFullName");
            }
        }

        public string EmpPhone
        {
            get { return Employee.EmpPhone; }
            set
            {
                Employee.EmpPhone = value;
                OnPropertyChanged("EmpPhone");
            }
        }

        public string EmpHashPassword
        {
            get { return Employee.EmpHashPassword; }
            set
            {
                Employee.EmpHashPassword = value;
                OnPropertyChanged("EmpHashPassword");
            }
        }

        public string EmpLogin
        {
            get { return Employee.EmpLogin; }
            set
            {
                Employee.EmpLogin = value;
                OnPropertyChanged("EmpLogin");
            }
        }

        public DateTime? EmpCreateDate
        {
            get { return Employee.EmpCreateDate; }
            set
            {
                Employee.EmpCreateDate = value;
                OnPropertyChanged("EmpCreateDate");
            }
        }


        public DateTime? EmpDeleteDate
        {
            get { return Employee.EmpDeleteDate; }
            set
            {
                Employee.EmpDeleteDate = value;
                OnPropertyChanged("EmpDeleteDate");
            }
        }

        public EmpStatus EmpStatus
        {
            get { return Employee.EmpStatus; }
            set
            {
                Employee.EmpStatus = value;
                OnPropertyChanged("EmpStatus");
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

        public static ObservableCollection<Employee> ListOfEmployees = new ObservableCollection<Employee>();
        public static void EmployeeInit() 
        {
           
            using (EmployeeRepos db = new EmployeeRepos())
            {
                ListOfEmployees = db.GetParts();
                
            }

            CV = CollectionViewSource.GetDefaultView(ListOfEmployees);
        }

        public static void SeachList(string searchpram)
        {
            try
            {
                CV.Filter = obj => string.IsNullOrEmpty(searchpram) || obj is Employee employee &&
                (
                employee.EmpFullName.ToLower().Contains(searchpram.ToLower()) ||
                employee.EmpPersonId.ToString().ToLower().Contains(searchpram.ToLower()) ||
                employee.EmpPhone.ToString().ToLower().Contains(searchpram.ToLower())
                );
                CV.Refresh();
            }
            catch{ }
        }


    }
}
