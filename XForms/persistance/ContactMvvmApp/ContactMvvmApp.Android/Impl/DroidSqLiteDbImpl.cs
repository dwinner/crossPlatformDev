using System.IO;
using ContactMvvmApp.Droid.Impl;
using ContactMvvmApp.Interfaces;
using SQLite;
using Xamarin.Forms;
using Env = System.Environment;

[assembly: Dependency(typeof(DroidSqLiteDbImpl))]

namespace ContactMvvmApp.Droid.Impl
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