using System.IO;
using Windows.Storage;
using ContactBookApp.Interfaces;
using ContactBookApp.UWP.Impl;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(UwpSqLiteDbImpl))]

namespace ContactBookApp.UWP.Impl
{
   public class UwpSqLiteDbImpl : ISqLiteDb
   {
      public SQLiteAsyncConnection GetConnection()
      {
         var documentsPath = ApplicationData.Current.LocalFolder.Path;
         var dbPath = Path.Combine(documentsPath, "MySQLite.db3");

         return new SQLiteAsyncConnection(dbPath);
      }
   }
}