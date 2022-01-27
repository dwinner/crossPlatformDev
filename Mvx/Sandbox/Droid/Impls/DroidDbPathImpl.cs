using System.IO;
using MvvxSandboxApp.Core.Services.Interfaces;
using Env = System.Environment;

namespace MvvxSandboxApp.Droid.Impls
{
   public sealed class DroidDbPathImpl : IDbPath
   {
      public string GetDbPath(string aFileName)
      {
         var dbPath = Path.Combine(Env.GetFolderPath(Env.SpecialFolder.Personal), aFileName);
         return dbPath;
      }
   }
}