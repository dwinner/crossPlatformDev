using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileStorageApp
{
   public interface IFileWorker
   {
      Task<bool> ExistsAsync(string fileName);

      Task SaveTextAsync(string fileName, string text);

      Task<string> LoadTextAsync(string fileName);

      Task<IEnumerable<string>> GetFilesAsync();

      Task DeleteAsync(string fileName);
   }
}