using System.Collections.Generic;

namespace StockList.Core.UI
{
   /// <summary>
   ///    Navigable xamarin forms page
   /// </summary>
   public interface INavigableXamarinFormsPage
   {
      /// <summary>
      ///    Called when navigated to
      /// </summary>
      /// <param name="navigationParameters">Navigation parameters</param>
      void OnNavigatedTo(IDictionary<string, object> navigationParameters);
   }
}