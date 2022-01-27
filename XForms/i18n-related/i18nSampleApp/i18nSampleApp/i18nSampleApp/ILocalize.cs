using System.Globalization;

namespace i18nSampleApp
{
   public interface ILocalize
   {
      CultureInfo GetCurrentCultureInfo();
   }
}