using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace i18nSampleApp
{
   public class TranslateExtension : IMarkupExtension
   {
      private const string ResourceId = "i18nSampleApp.Resource";
      private readonly CultureInfo _cultureInfo;

      public TranslateExtension() => _cultureInfo = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();

      public string Text { get; set; }

      public object ProvideValue(IServiceProvider serviceProvider)
      {
         if (Text == null)
         {
            return string.Empty;
         }

         var resourceManager = new ResourceManager(ResourceId, typeof(TranslateExtension).GetTypeInfo().Assembly);
         var translation = resourceManager.GetString(Text, _cultureInfo) ?? Text;

         return translation;
      }
   }
}