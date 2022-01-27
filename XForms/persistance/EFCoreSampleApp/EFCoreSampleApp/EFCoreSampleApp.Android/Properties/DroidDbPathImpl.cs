using System;
using System.IO;
using EFCoreSampleApp.Droid.Properties;
using Xamarin.Forms;

[assembly: Dependency(typeof(DroidDbPathImpl))]

namespace EFCoreSampleApp.Droid.Properties
{
   public class DroidDbPathImpl : IDbPath
   {
      public string GetDbPath(string aFileName) =>
         Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), aFileName);
   }
}