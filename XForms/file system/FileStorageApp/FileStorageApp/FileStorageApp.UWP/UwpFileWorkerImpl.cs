using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using FileStorageApp.UWP;
using XamDependency = Xamarin.Forms.DependencyAttribute;

[assembly: XamDependency(typeof(UwpFileWorkerImpl))]

namespace FileStorageApp.UWP
{
   public class UwpFileWorkerImpl : IFileWorker
   {
      public async Task<bool> ExistsAsync(string fileName)
      {
         var localFolder = ApplicationData.Current.LocalFolder;
         try
         {
            await localFolder.GetFileAsync(fileName);
         }
         catch
         {
            return false;
         }

         return true;
      }

      public async Task SaveTextAsync(string fileName, string text)
      {
         var localFolder = ApplicationData.Current.LocalFolder;
         var storageFile = await localFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
         await FileIO.WriteTextAsync(storageFile, text);
      }

      public async Task<string> LoadTextAsync(string fileName)
      {
         var localFolder = ApplicationData.Current.LocalFolder;
         var storageFile = await localFolder.GetFileAsync(fileName);
         var text = await FileIO.ReadTextAsync(storageFile);
         return text;
      }

      public async Task<IEnumerable<string>> GetFilesAsync()
      {
         var localFolder = ApplicationData.Current.LocalFolder;
         var fileNames =
            from storageFile in await localFolder.GetFilesAsync()
            select storageFile.Name;
         return fileNames;
      }

      public async Task DeleteAsync(string fileName)
      {
         var localFolder = ApplicationData.Current.LocalFolder;
         var storageFile = await localFolder.GetFileAsync(fileName);
         await storageFile.DeleteAsync();
      }
   }
}