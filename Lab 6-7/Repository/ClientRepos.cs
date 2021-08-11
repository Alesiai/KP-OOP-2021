using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab_6_7.Model;
using Lab_6_7.CaffeDbContext;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Collections.ObjectModel;


namespace Lab_6_7.Repository
{
    class ClientRepos : IRepository<Client>
    {
        public ClientContext cl;

        public ClientRepos()
        {
            this.cl = new ClientContext();

        }

        public ClientRepos(ClientContext context)
        {
            this.cl = context;
        }


        public void Dispose()
        {
            if (cl != null)
                cl.connection.Close();
        }

        public ObservableCollection<Client> GetParts()
        {
            string sql = "Select * From КЛИЕНТ";
            DataTable ClientsTable = new DataTable();
            SqlDataAdapter adapter;

            ObservableCollection<Client> Clients = new ObservableCollection<Client>();

            try
            {
                SqlCommand command = new SqlCommand(sql, cl.connection);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ClientsTable);
                for (int i = 0; i < ClientsTable.Rows.Count; i++)
                {
                    Client.size = ClientsTable.Rows[i].Field<int>("ИНКлиента");
                    Client client = new Client();
                    client.ClPersonId = ClientsTable.Rows[i].Field<int>("ИНКлиента");
                    client.ClFullName = ClientsTable.Rows[i].Field<string>("ПолноеИмя");
                    client.ClPhone = ClientsTable.Rows[i].Field<string>("Телефон");

                    Clients.Add(client);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return Clients;
        }

        public bool AddPart(Client p)
        {
            string sql = $"INSERT INTO КЛИЕНТ(ПолноеИмя, Телефон) VALUES " +
                $"(\'{p.ClFullName}\', \'{p.ClPhone}\'); ";
            try
            {
                SqlCommand command = new SqlCommand(sql, cl.connection);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool Update(int id, Client p)
        {
            string sql = $"UPDATE КЛИЕНТ SET " +
                $"ПолноеИмя = \'{p.ClFullName}\', " +
                $"Телефон = \'{p.ClPhone}\' " +
                $"WHERE ИНКлиента = {id}";
            try
            {
                SqlCommand command = new SqlCommand(sql, cl.connection);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool Delete(int id, Client p)
        {
            return true;
        }

    }
}