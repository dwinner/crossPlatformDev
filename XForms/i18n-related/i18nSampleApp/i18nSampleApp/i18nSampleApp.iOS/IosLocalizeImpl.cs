using System.Globalization;
using Foundation;
using i18nSampleApp.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(IosLocalizeImpl))]

namespace i18nSampleApp.iOS
{
   public class IosLocalizeImpl : ILocalize
   {
      private const string DefaultPreferredLanguage = "en-US";

      public CultureInfo GetCurrentCultureInfo()
      {
         var netLanguage = "en";

         if (NSLocale.PreferredLanguages.Length > 0)
         {
            var pref = NSLocale.PreferredLanguages[0];
            netLanguage = pref.Replace("_", "-");
         }

         CultureInfo cultureInfo;
         try
         {
            cultureInfo = new CultureInfo(netLanguage);
         }
         catch
         {
            cultureInfo = new CultureInfo(DefaultPreferredLanguage);
         }

         return cultureInfo;
      }
   }
}