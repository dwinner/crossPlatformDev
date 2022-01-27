using System;
using System.IO;
using SqLiteSampleApp.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(IosSqLiteImpl))]

namespace SqLiteSampleApp.iOS
{
   public class IosSqLiteImpl : ISqLite
   {
      public string GetDatabasePath(string sqliteFileName)
      {
         var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
         var libraryPath = Path.Combine(documentsPath, "..", "Library");
         var sqlDbPath = Path.Combine(libraryPath, sqliteFileName);

         if (!File.Exists(sqlDbPath))
         {
            File.Copy(sqliteFileName, sqlDbPath);
         }

         return sqlDbPath;
      }
   }
}