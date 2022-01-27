using Windows.System.Profile;
using PaperBoy.Interfaces;

namespace PaperBoy.UWP.DependencyServices
{
   public class PlatformLabel : IPlatformLabel
   {
      public string GetLabel() => "Windows";

      public string GetLabel(string additionalLabel) => $"{additionalLabel} Windows";

      public string GetLabel(string additionalLabel, bool addOsVersion)
      {
         string label = addOsVersion
            ? label = $"{additionalLabel} Windows {GetOsVersion()}"
            : $"{additionalLabel} Windows";
         return label;
      }

      private string GetOsVersion()
      {
         var systemVersion = AnalyticsInfo.VersionInfo.DeviceFamilyVersion;

         var v = ulong.Parse(systemVersion);
         var v1 = (v & 0xFFFF000000000000L) >> 48;
         var v2 = (v & 0x0000FFFF00000000L) >> 32;
         var v3 = (v & 0x00000000FFFF0000L) >> 16;
         var v4 = v & 0x000000000000FFFFL;
         var version = $"{v1}.{v2}.{v3}.{v4}";

         return version;
      }
   }
}