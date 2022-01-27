using System;
using System.Collections.Generic;
using System.IO;
using PaperBoy.Helpers;
using PaperBoy.iOS.DependencyServices;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]

namespace PaperBoy.iOS.DependencyServices
{
   public class FileHelper : IFileHelper
   {
      public string GetLocalFilePath(string fileName)
      {
         var docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
         var libFolder = Path.Combine(docFolder, "..", "Liberay", "Databases");

         if (!Directory.Exists(libFolder))
         {
            Directory.CreateDirectory(libFolder);
         }

         return Path.Combine(libFolder, fileName);
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