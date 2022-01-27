using System;
using System.IO;
using NativeAccess.iOS;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(DatabaseConnection))]

namespace NativeAccess.iOS
{
   public class DatabaseConnection : IDatabaseConnection
   {
      public SQLiteConnection DbConnection()
      {
         const string dbName = "MyDatabase.db3";
         var personalFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
         var libraryFolder = Path.Combine(personalFolder, "..", "Library");
         var path = Path.Combine(libraryFolder, dbName);
         return new SQLiteConnection(path);
      }
   }
}