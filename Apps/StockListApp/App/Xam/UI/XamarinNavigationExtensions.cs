using System.Collections.Generic;
using StockList.Core.Models;
using Xamarin.Forms;

namespace StockListApp.Xam.UI
{
   /// <summary>
   ///    Xamarin navigation extensions
   /// </summary>
   public static class XamarinNavigationExtensions
   {
      /// <summary>
      ///    Show the specified page and parameters
      /// </summary>
      /// <param name="page">Page</param>
      /// <param name="parameters">Parameters</param>
      public static void Show(this ContentPage page, IDictionary<string, object> parameters) =>
         (page.BindingContext as ViewModelBase)?.OnShow(parameters);
   }
}