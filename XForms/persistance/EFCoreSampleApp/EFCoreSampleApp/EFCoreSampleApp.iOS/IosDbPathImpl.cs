using System;
using System.IO;
using EFCoreSampleApp.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(IosDbPathImpl))]

namespace EFCoreSampleApp.iOS
{
   public class IosDbPathImpl : IDbPath
   {
      public string GetDbPath(string aFileName) =>
         Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", aFileName);
   }
}