using System;
using System.IO;
using SqLiteSampleApp.Droid;
using Xamarin.Forms;
using Application = Android.App.Application;

[assembly: Dependency(typeof(DroidSqLiteImpl))]

namespace SqLiteSampleApp.Droid
{
   public class DroidSqLiteImpl : ISqLite
   {
      public string GetDatabasePath(string sqliteFileName)
      {
         var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
         var sqliteDbPath = Path.Combine(documentsPath, sqliteFileName);

         if (!File.Exists(sqliteDbPath))
         {
            var context = Application.Context;
            using (var dbAssetStream = context.Assets.Open(sqliteFileName))
            using (var dbFileStream = new FileStream(sqliteDbPath, FileMode.OpenOrCreate))
            {
               var buffer = new byte[1024];
               var bufferLen = buffer.Length;
               int length;
               while ((length = dbAssetStream.Read(buffer, 0, bufferLen)) > 0)
               {
                  dbFileStream.Write(buffer, 0, length);
               }

               dbFileStream.Flush();
            }
         }

         return sqliteDbPath;
      }
   }
}