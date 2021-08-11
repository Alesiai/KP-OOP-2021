using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Lab_6_7.Model;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Collections.ObjectModel;

namespace Lab_6_7.CaffeDbContext
{
    public class EmployeeContext : IDisposable
    {
        string connectionString;
        public SqlConnection connection = null;

        public EmployeeContext()
        {
            connectionString = "server=DESKTOP-9MPO3CO\\MYCOLLECTION;Trusted_Connection=Yes;DataBase=COFFEE;";
            connection = new SqlConnection(connectionString);
            connection.Open();
        }
        public void Dispose()
        {
            if (connection != null)
                connection.Close();
        }

<<<<<<< HEAD
=======
        public List<Employee> GetParts()
        {
            string sql = "Select * From СОТРУДНИК";
            DataTable employeesTable = new DataTable();
            SqlDataAdapter adapter;

            List<Employee> employees = new List<Employee>();
            
            try
            {
                SqlCommand command = new SqlCommand(sql, connection);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(employeesTable);
                for (int i = 0; i < employeesTable.Rows.Count; i++)
                {
                    Employee.size= employeesTable.Rows[i].Field<int>("ИНСотрудника");
                    Employee employee = new Employee() ;
                    employee.EmpPersonId = employeesTable.Rows[i].Field<int>("ИНСотрудника");
                    employee.EmpFullName = employeesTable.Rows[i].Field<string>("ПолноеИмя");
                    employee.EmpPhone = employeesTable.Rows[i].Field<string>("Телефон");
                    employee.EmpLogin = employeesTable.Rows[i].Field<string>("Логин");
                    employee.EmpHashPassword = employeesTable.Rows[i].Field<string>("Пароль");
                    employee.EmpStatus = (EmpStatus)Enum.Parse(typeof(EmpStatus), employeesTable.Rows[i].Field<string>("Статус"));
                   
                    DateTime create = employeesTable.Rows[i].Field<DateTime>("ДатаСоздания");
                    employee.EmpCreateDate = create;
                    
                    employees.Add(employee);

                    DateTime? delate = employeesTable.Rows[i].Field<DateTime?>("ДатаУдаления");
                    if (delate != null)
                    {
                        employee.EmpCreateDate = delate;
                        employees.Remove(employee);
                    }
                }
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message);
            }
            return employees;
        }

        public bool AddPart(Employee p, string date)
        {
            string sql = $"INSERT INTO СОТРУДНИК(ПолноеИмя, Телефон, Логин, Пароль, Статус, ДатаСоздания) VALUES " +
                $"(\'{p.EmpFullName}\', \'{p.EmpPhone}\', \'{p.EmpLogin}\', \'{p.EmpHashPassword}\', \'{Convert.ToString(p.EmpStatus)}\', \'{date}\'); ";
                
                //$"(\'{p.EmpFullName}\', \'{p.EmpPhone}\', \'{p.EmpLogin}\', \'{p.EmpHashPassword}\', \'{Convert.ToString(p.EmpStatus)}\'," +
                //$" \'24.05.2020\')";
            try
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool Update(int id, Employee p)
        {
            string sql = $"UPDATE СОТРУДНИК SET " +
                $"ПолноеИмя = \'{p.EmpFullName}\', " +
                $"Телефон = \'{p.EmpPhone}\', " +
                $"Логин = \'{p.EmpLogin}\', " +
                $"Пароль = \'{p.EmpHashPassword}\', " +
                $"Статус = \'{Convert.ToString(p.EmpStatus)}\', " +
                $"WHERE ИНСотрудника = {id}";
            try
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool Delete(int id, Employee p)
        {
            string sql = $"UPDATE СОТРУДНИК SET " +
                $"ПолноеИмя = \'{p.EmpFullName}\', " +
                $"Телефон = \'{p.EmpPhone}\', " +
                $"Логин = \'{p.EmpLogin}\', " +
                $"Пароль = \'{p.EmpHashPassword}\', " +
                $"Статус = \'{Convert.ToString(p.EmpStatus)}\', " +
                $"ДатаУдаления = GETDATE() " +
                $"WHERE ИНСотрудника = {id}";
            try
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

>>>>>>> parent of ddd02f0 (NEW)
    }
}

