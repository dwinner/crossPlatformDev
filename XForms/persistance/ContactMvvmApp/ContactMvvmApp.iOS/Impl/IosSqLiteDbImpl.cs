using System;
using System.IO;
using ContactMvvmApp.Interfaces;
using ContactMvvmApp.iOS.Impl;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(IosSqLiteDbImpl))]

namespace ContactMvvmApp.iOS.Impl
{
   public class IosSqLiteDbImpl : ISqLiteDb
   {
      public SQLiteAsyncConnection GetConnection()
      {
         var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
         var dbPath = Path.Combine(documentsPath, "MySQLite.db3");

         return new SQLiteAsyncConnection(dbPath);
      }
   }
}