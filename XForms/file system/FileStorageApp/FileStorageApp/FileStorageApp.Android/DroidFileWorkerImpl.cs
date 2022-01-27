using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileStorageApp.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(DroidFileWorkerImpl))]

namespace FileStorageApp.Droid
{
   public class DroidFileWorkerImpl : IFileWorker
   {
      public Task<bool> ExistsAsync(string fileName)
      {
         var filePath = GetFilePath(fileName);
         var exists = File.Exists(filePath);
         return Task.FromResult(exists);
      }

      public async Task SaveTextAsync(string fileName, string text)
      {
         var filePath = GetFilePath(fileName);
         using (var writer = File.CreateText(filePath))
         {
            await writer.WriteAsync(text).ConfigureAwait(false);
         }
      }

      public async Task<string> LoadTextAsync(string fileName)
      {
         var filePath = GetFilePath(fileName);
         using (var reader = File.OpenText(filePath))
         {
            return await reader.ReadToEndAsync().ConfigureAwait(false);
         }
      }

      public Task<IEnumerable<string>> GetFilesAsync()
      {
         var fileNames =
            from filePath in Directory.EnumerateFiles(GetDocsPath())
            select Path.GetFileName(filePath);
         return Task.FromResult(fileNames);
      }

      public Task DeleteAsync(string fileName)
      {
         File.Delete(GetFilePath(fileName));
         return Task.FromResult(true);
      }

      private string GetDocsPath() => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

      private string GetFilePath(string fileName) => Path.Combine(GetDocsPath(), fileName);
   }
}