using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab_6_7.CaffeDbContext;
using Lab_6_7.Model;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Collections.ObjectModel;

namespace Lab_6_7.CaffeDbContext
{
    interface IRepository<T> : IDisposable
         where T : class
    {
        ObservableCollection<T> GetParts();// получение всех объектов
        bool AddPart(T item); // создание объекта
        bool Update(int id, T item); // обновление объекта
        bool Delete(int id, T item); // удаление объекта по id
    }
}
