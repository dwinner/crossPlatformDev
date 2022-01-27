using System;
using System.IO;
using SQLite.Net;
using SQLite.Net.Interop;
using SQLite.Net.Platform.XamarinAndroid;
using SQLiteExample.Interfaces;

namespace SQLiteExample.Droid.Interfaces
{
   public class SqLiteConnectionFactory : ISqLiteConnectionFactory
   {
      private const string Filename = "personalinfo.db";

      private static ISQLitePlatform SqLitePlatform => new SQLitePlatformAndroid();

      public SQLiteConnection GetConnection()
      {
         var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
         path = Path.Combine(path, Filename);
         return new SQLiteConnection(SqLitePlatform, path);
      }
   }
}