using System;
using System.IO;
using FileStorageApp.DataAccess.Storage;
using SQLite.Net.Interop;

namespace FileStorageApp.Droid.DataAccess
{
   public class SqLiteSetup : ISqLiteSetup
   {
      public SqLiteSetup(ISQLitePlatform platform)
      {
         DatabasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "filestorage.db3");
         Platform = platform;
      }

      public string DatabasePath { get; set; }

      public ISQLitePlatform Platform { get; set; }
   }
}