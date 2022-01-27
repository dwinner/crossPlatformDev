using System.Globalization;
using i18nSampleApp.UWP;
using Xamarin.Forms;

[assembly: Dependency(typeof(UwpLocalizeImpl))]

namespace i18nSampleApp.UWP
{
   public class UwpLocalizeImpl : ILocalize
   {
      public CultureInfo GetCurrentCultureInfo() => CultureInfo.CurrentUICulture;
   }
}