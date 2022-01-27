using System.IO;
using Windows.Storage;
using EFCoreSampleApp.UWP;
using Xamarin.Forms;

[assembly: Dependency(typeof(UwpDbPathImpl))]

namespace EFCoreSampleApp.UWP
{
   public class UwpDbPathImpl : IDbPath
   {
      public string GetDbPath(string aFileName) => Path.Combine(ApplicationData.Current.LocalFolder.Path, aFileName);
   }
}