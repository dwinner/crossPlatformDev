using System.IO;
using MvvxSandboxApp.Core.Services.Interfaces;
using Env = System.Environment;

namespace MvvxSandboxApp.iOS.Impls
{
   public sealed class IosDbPathImpl : IDbPath
   {
      public string GetDbPath(string aFileName)
      {
         var dbPath = Path.Combine(Env.GetFolderPath(Env.SpecialFolder.MyDocuments), "..", "Library", aFileName);
         return dbPath;
      }
   }
}