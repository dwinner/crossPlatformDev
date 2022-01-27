using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileStorageApp.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(IosFileWorkerImpl))]

namespace FileStorageApp.iOS
{
   public class IosFileWorkerImpl : IFileWorker
   {
      public Task DeleteAsync(string filename)
      {
         File.Delete(GetFilePath(filename));
         return Task.FromResult(true);
      }

      public Task<bool> ExistsAsync(string filename)
      {
         var filepath = GetFilePath(filename);
         var exists = File.Exists(filepath);
         return Task.FromResult(exists);
      }

      public Task<IEnumerable<string>> GetFilesAsync()
      {
         var filenames =
            from filepath in Directory.EnumerateFiles(GetDocsPath())
            select Path.GetFileName(filepath);
         return Task.FromResult(filenames);
      }

      public async Task<string> LoadTextAsync(string filename)
      {
         var filepath = GetFilePath(filename);
         using (var reader = File.OpenText(filepath))
         {
            return await reader.ReadToEndAsync().ConfigureAwait(false);
         }
      }

      public async Task SaveTextAsync(string filename, string text)
      {
         var filepath = GetFilePath(filename);
         using (var writer = File.CreateText(filepath))
         {
            await writer.WriteAsync(text).ConfigureAwait(false);
         }
      }

      private string GetFilePath(string filename) => Path.Combine(GetDocsPath(), filename);

      private static string GetDocsPath() => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
   }
}