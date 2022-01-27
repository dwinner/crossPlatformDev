using System.IO;
using ContactBookApp.Droid.Impl;
using ContactBookApp.Interfaces;
using SQLite;
using Xamarin.Forms;
using Env = System.Environment;

[assembly: Dependency(typeof(DroidSqLiteDbImpl))]

namespace ContactBookApp.Droid.Impl
{
   public class DroidSqLiteDbImpl : ISqLiteDb
   {
      public SQLiteAsyncConnection GetConnection()
      {
         var documentsPath = Env.GetFolderPath(Env.SpecialFolder.MyDocuments);
         var dbPath = Path.Combine(documentsPath, "MySQLite.db3");

         return new SQLiteAsyncConnection(dbPath);
      }
   }
}