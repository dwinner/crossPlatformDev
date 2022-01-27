using System.Collections.Generic;
using Locator.Common.ViewModels;
using Xamarin.Forms;

namespace Locator.App.UI
{
   /// <summary>
   ///    Xamarin navigation extensions.
   /// </summary>
   public static class XamarinNavigationExtensions
   {
      /// <summary>
      ///    Show the specified page and parameters.
      /// </summary>
      /// <param name="page">Page.</param>
      /// <param name="parameters">Parameters.</param>
      public static void Show(this ContentPage page, IDictionary<string, object> parameters)
      {
         if (page.BindingContext is ViewModelBase target)
         {
            target.OnShow(parameters);
         }
      }
   }
}