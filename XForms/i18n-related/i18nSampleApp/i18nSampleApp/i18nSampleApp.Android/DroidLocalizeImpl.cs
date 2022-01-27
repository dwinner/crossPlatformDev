using System.Globalization;
using i18nSampleApp.Droid;
using Java.Util;
using Xamarin.Forms;

[assembly: Dependency(typeof(DroidLocalizeImpl))]

namespace i18nSampleApp.Droid
{
   public class DroidLocalizeImpl : ILocalize
   {
      public CultureInfo GetCurrentCultureInfo()
      {
         var androidLocale = Locale.Default;
         var netLanguage = androidLocale.ToString().Replace("_", "-");
         return new CultureInfo(netLanguage);
      }
   }
}