using System.Collections.Generic;
using FileStorageApp.ViewModels;

namespace FileStorageApp.UI
{
   public static class XamarinNavigationExtensions
   {
      /// <summary>
      ///    Show the specified page and parameters
      /// </summary>
      /// <param name="page">Page</param>
      /// <param name="parameters">Navigation parameters</param>
      public static void Show(this ExtendedContentPage page, IDictionary<string, object> parameters)
      {
         var target = page.BindingContext as ViewModelBase;
         target?.OnShow(parameters);
      }
   }
}