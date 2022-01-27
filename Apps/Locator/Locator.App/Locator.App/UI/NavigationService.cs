using System.Collections.Generic;
using System.Threading.Tasks;
using Locator.App.Pages;
using Locator.Common.Enums;
using Locator.Common.IoC;
using Locator.Common.UI;
using Xamarin.Forms;

namespace Locator.App.UI
{
   /// <summary>
   ///    Navigation service.
   /// </summary>
   public class NavigationService : INavigationService
   {
      /// <summary>
      ///    Navigate the specified pageName and navigationParameters.
      /// </summary>
      /// <param name="pageName">Page name.</param>
      /// <param name="navigationParameters">Navigation parameters.</param>
      public async Task NavigateAsync(PageNames pageName, IDictionary<string, object> navigationParameters)
      {
         var page = GetPage(pageName);
         if (page is INavigableXamarinFormsPage navigablePage)
         {
            await IoC.Resolve<NavigationPage>().PushAsync(page).ConfigureAwait(true);
            navigablePage.OnNavigatedTo(navigationParameters);
         }
      }

      /// <summary>
      ///    Gets the page.
      /// </summary>
      /// <returns>The page.</returns>
      /// <param name="page">Page.</param>
      private Page GetPage(PageNames page)
      {
         switch (page)
         {
            case PageNames.MainPage:
               return IoC.Resolve<MainPage>();

            case PageNames.MapPage:
               /* BUG:
                Unhandled Exception:
                  Autofac.Core.DependencyResolutionException:
                  An exception was thrown while activating Locator.Pages.MapPage
                     -> Locator.Portable.ViewModels.MapPageViewModel
                     -> Locator.iOS.Location.GeolocatorIOS. occurred */
               return IoC.Resolve<MapPage>();

            default:
               return null;
         }
      }
   }
}