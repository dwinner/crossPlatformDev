using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IslandMenu.App.Helpers
{
   // Define the default content property
   [ContentProperty(nameof(Text))]
   public class TranslateExtension : IMarkupExtension
   {
      /// <summary>
      ///    Default property for translated key
      /// </summary>
      public string Text { get; set; }

      /// <summary>
      ///    Return the localized resource if available, default resource - if not
      /// </summary>
      /// <param name="serviceProvider">Service provider</param>
      /// <returns></returns>
      public object ProvideValue(IServiceProvider serviceProvider) => Text == null
         ? null
         : Resources.IslandMenu.ResourceManager.GetString(Text, Resources.IslandMenu.Culture);
   }
}