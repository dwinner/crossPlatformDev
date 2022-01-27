using System.Collections.Generic;
using System.Linq;
using SQLite.Net;
using SQLiteExample.Interfaces;
using SQLiteExample.Models;

namespace SQLiteExample.Helpers
{
   public class SqLiteRepository : IRepository
   {
      private readonly SQLiteConnection _connection;

      public SqLiteRepository(ISqLiteConnectionFactory connectionFactory)
      {
         _connection = connectionFactory.GetConnection();
         CreateTables();
      }

      public void SaveData<T>(T toStore) => _connection.InsertOrReplace(toStore);

      public List<T> GetList<T>(int top = 0) where T : class, new()
      {
         var sql = $"SELECT * FROM {GetName(typeof(T).ToString())}";
         var list = _connection.Query<T>(sql, string.Empty);
         if (list.Count != 0 && top != 0)
         {
            list = list.Take(top).ToList();
         }

         return list;
      }

      public T GetData<T>() where T : class, new()
      {
         var sql = $"SELECT * FROM {GetName(typeof(T).ToString())}";
         var list = _connection.Query<T>(sql, string.Empty);
         return list?.FirstOrDefault();
      }

      public T GetData<T, TU>(string para, TU val) where T : class, new()
      {
         var sql = $"SELECT * FROM {GetName(typeof(T).ToString())} WHERE {para}=?";
         var list = _connection.Query<T>(sql, val);
         return list?.FirstOrDefault();
      }

      public void Delete<T>(T stored) => _connection.Delete(stored);

      public T GetData<T, TU, TV>(string para1, TU val1, string para2, TV val2) where T : class, new()
      {
         var sql = $"SELECT * FROM {GetName(typeof(T).ToString())} WHERE {para1}=? AND {para2}=?";
         var list = _connection.Query<T>(sql, val1, val2);
         return list?.FirstOrDefault();
      }

      public int GetId<T>() where T : class, new()
      {
         var sql = $"SELECT last_insert_rowid() FROM {GetName(typeof(T).ToString())}";
         var id = _connection.ExecuteScalar<int>(sql, string.Empty);
         return id;
      }

      public void SaveData<T>(IEnumerable<T> toStore) => _connection.InsertOrReplaceAll(toStore);

      public int Count<T>() where T : class, new() => GetList<T>().Count;

      private void CreateTables()
      {
         _connection.CreateTable<Hobbies>();
         _connection.CreateTable<Pets>();
         _connection.CreateTable<PersonalInfo>();
      }

      private static string GetName(string name)
      {
         var list = name.Split('.').ToList();
         if (list.Count == 1)
         {
            return list[0];
         }

         var last = list[list.Count - 1];
         return last;
      }
   }
}