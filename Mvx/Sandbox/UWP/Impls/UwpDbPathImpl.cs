using System.IO;
using Windows.Storage;
using MvvxSandboxApp.Core.Services.Interfaces;

namespace MvvxSandboxApp.UWP.Impls
{
   public sealed class UwpDbPathImpl : IDbPath
   {
      public string GetDbPath(string aFileName)
      {
         var dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, aFileName);
         return dbPath;
      }
   }
}