using System.Collections.Generic;
using System.Threading.Tasks;
using StockList.Core.Enums;
using StockList.Core.Ioc;
using StockList.Core.UI;
using Xamarin.Forms;
using XamPages = StockListApp.Xam.Pages;

namespace StockListApp.Xam.UI
{
   public class NavigationService : INavigationService
   {
      public async Task NavigateAsync(PageNames pageName, IDictionary<string, object> navigationParameters)
      {
         var page = GetPage(pageName);
         if (page is INavigableXamarinFormsPage navigablePage)
         {
            await IoC.Resolve<NavigationPage>().PushAsync(page).ConfigureAwait(true);
            navigablePage.OnNavigatedTo(navigationParameters);
         }
      }

      private static Page GetPage(PageNames pageName)
      {
         switch (pageName)
         {
            case PageNames.MainPage:
               return IoC.Resolve<XamPages.MainPage>();

            case PageNames.StocklistPage:
               return IoC.Resolve<XamPages.StocklistPage>();

            case PageNames.StockItemDetailsPage:
               return IoC.Resolve< /*Func<*/XamPages.StockItemDetailsPage /*>*/>();

            default:
               return null;
         }
      }
   }
}