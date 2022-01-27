using System.Collections.Generic;
using System.IO;
using Windows.Storage;
using PaperBoy.Helpers;
using PaperBoy.UWP.DependencyServices;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]

namespace PaperBoy.UWP.DependencyServices
{
   public class FileHelper : IFileHelper
   {
      public string GetLocalFilePath(string fileName) =>
         Path.Combine(ApplicationData.Current.LocalFolder.Path, fileName);

      public List<string> GetSpecialFolders() => new List<string>();
   }
}