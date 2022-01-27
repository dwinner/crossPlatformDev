using System.Collections.Generic;
using Xamarin.Forms;

namespace PaperBoy.Helpers
{
   public static class StorageHelper
   {
      public static List<string> GetSpecialFolders() => DependencyService.Get<IFileHelper>().GetSpecialFolders();

      public static string GetLocalFilePath(string dbName) =>
         DependencyService.Get<IFileHelper>().GetLocalFilePath(dbName);
   }
}