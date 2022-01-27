using System.Collections.Generic;

namespace FileStorageApp.UI
{
   /// <summary>
   ///    Navigable xamarin forms page
   /// </summary>
   public interface INavigableXamarinFormsPage
   {
      /// <summary>
      ///    Called when page is navigated to
      /// </summary>
      /// <param name="navigationParameters">Navigation parameters</param>
      void OnNavigatedTo(IDictionary<string, object> navigationParameters);
   }
}