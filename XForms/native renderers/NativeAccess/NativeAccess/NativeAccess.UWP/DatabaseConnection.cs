using System.IO;
using Windows.Storage;
using NativeAccess.UWP;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(DatabaseConnection))]

namespace NativeAccess.UWP
{
   public class DatabaseConnection : IDatabaseConnection
   {
      public SQLiteConnection DbConnection()
      {
         const string dbName = "MyDatabase.db3";
         var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, dbName);
         return new SQLiteConnection(path);
      }
   }
}