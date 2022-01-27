using Android.OS;
using PaperBoy.Droid.DependencyServices;
using PaperBoy.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(PlatformLabel))]

namespace PaperBoy.Droid.DependencyServices
{
   public class PlatformLabel : IPlatformLabel
   {
      public string GetLabel() => "Android";

      public string GetLabel(string additionalLabel) => $"{additionalLabel} Android";

      public string GetLabel(string additionalLabel, bool addOsVersion)
      {
         var label = addOsVersion
            ? $"{additionalLabel} Android {GetOsVersion()}"
            : $"{additionalLabel} Android";
         return label;
      }

      public static void Init()
      {
      }

      private string GetOsVersion() => Build.VERSION.Release;
   }
}