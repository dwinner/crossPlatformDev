using System.Collections.Generic;

namespace Locator.App.UI
{
   /// <summary>
   ///    Navigable xamarin forms page.
   /// </summary>
   internal interface INavigableXamarinFormsPage
   {
      /// <summary>
      ///    Called when page is navigated to.
      /// </summary>
      /// <returns>The navigated to.</returns>
      /// <param name="navigationParameters">Navigation parameters.</param>
      void OnNavigatedTo(IDictionary<string, object> navigationParameters);
   }
}