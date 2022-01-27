using PaperBoy.Interfaces;
using PaperBoy.iOS.DependencyServices;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(PlatformLabel))]

namespace PaperBoy.iOS.DependencyServices
{
   public class PlatformLabel : IPlatformLabel
   {
      public string GetLabel() => "iOS";

      public string GetLabel(string additionalLabel) => $"{additionalLabel} iOS";

      public string GetLabel(string additionalLabel, bool addOsVersion)
      {
         string label = addOsVersion ? label = $"{additionalLabel} iOS {GetOsVersion()}" : $"{additionalLabel} iOS";
         return label;
      }

      private string GetOsVersion() => UIDevice.CurrentDevice.SystemVersion;
   }
}