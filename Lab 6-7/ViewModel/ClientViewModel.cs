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
    public class ClientViewModel : ViewModelBase
    {
        public bool IsNew
        {
            get;
            set;
        }
        public Client Client
        {
            get; set;
        }

        public ClientViewModel(Client client)
        {
            this.Client = client;
        }

        //get;set
        #region 
        public int ClPersonId
        {
            get { return Client.ClPersonId; }
            set
            {
                Client.ClPersonId = value;
                OnPropertyChanged("ClPersonId");
            }
        }

        public string ClFullName
        {
            get { return Client.ClFullName; }
            set
            {
                Client.ClFullName = value;
                OnPropertyChanged("ClFullName");
            }
        }

        public string ClPhone
        {
            get { return Client.ClPhone; }
            set
            {
                Client.ClPhone = value;
                OnPropertyChanged("EmpPhone");
            }
        }


        #endregion


        static ICollectionView collectionView;

        public static ICollectionView CV
        {
            get => collectionView;
            set
            {collectionView = value;}
        }

        public static ObservableCollection<Client> ListOfClients = new ObservableCollection<Client>();
        public static void ClientInit()
        {
            using (ClientRepos db = new ClientRepos())
            {
                ListOfClients = db.GetParts();
            }
            CV = CollectionViewSource.GetDefaultView(ListOfClients);
        }

        public static void SeachList(string searchpram)
        {
            try
            {
                CV.Filter = obj => string.IsNullOrEmpty(searchpram) || obj is Client client &&
                (
                client.ClFullName.ToLower().Contains(searchpram.ToLower()) ||
                client.ClPersonId.ToString().ToLower().Contains(searchpram.ToLower()) ||
                client.ClPhone.ToString().ToLower().Contains(searchpram.ToLower())
                );
                CV.Refresh();
            }
            catch { }
        }

    }
}
