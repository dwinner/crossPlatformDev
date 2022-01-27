using System;
using System.IO;
using NativeAccess.Droid;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(DatabaseConnection))]

namespace NativeAccess.Droid
{
   public class DatabaseConnection : IDatabaseConnection
   {
      public SQLiteConnection DbConnection()
      {
         const string dbName = "MyDatabase.db3";
         var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), dbName);
         return new SQLiteConnection(path);
      }
   }
}