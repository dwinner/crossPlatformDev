using System;
using System.Collections.Generic;
using System.IO;
using PaperBoy.Droid.DependencyServices;
using PaperBoy.Helpers;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]

namespace PaperBoy.Droid.DependencyServices
{
   public class FileHelper : IFileHelper
   {
      public string GetLocalFilePath(string fileName)
      {
         var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
         return Path.Combine(path, fileName);
      }

      public List<string> GetSpecialFolders()
      {
         var folders = new List<string>();
         foreach (var folder in Enum.GetValues(typeof(Environment.SpecialFolder)))
         {
            if (!string.IsNullOrEmpty(Environment.GetFolderPath((Environment.SpecialFolder) folder)))
            {
               folders.Add($"{folder}={Environment.GetFolderPath((Environment.SpecialFolder) folder)}");
            }
         }

         return folders;
      }
   }
}