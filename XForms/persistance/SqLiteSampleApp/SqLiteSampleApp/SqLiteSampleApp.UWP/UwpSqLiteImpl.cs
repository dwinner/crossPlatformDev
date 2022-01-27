using System.IO;
using Windows.Storage;
using SqLiteSampleApp.UWP;
using Xamarin.Forms;

[assembly: Dependency(typeof(UwpSqLiteImpl))]

namespace SqLiteSampleApp.UWP
{
   public class UwpSqLiteImpl : ISqLite
   {
      public string GetDatabasePath(string sqliteFileName) =>
         Path.Combine(ApplicationData.Current.LocalFolder.Path, sqliteFileName);
   }
}