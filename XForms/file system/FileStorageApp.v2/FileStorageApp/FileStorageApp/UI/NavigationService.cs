using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FileStorageApp.Enums;
using FileStorageApp.Pages;
using Xamarin.Forms;

namespace FileStorageApp.UI
{
   public class NavigationService : INavigationService
   {
      public async Task NavigateAsync(PageNames pageName, IDictionary<string, object> navigationParameters)
      {
         var page = GetPage(pageName);
         if (page != null && page is INavigableXamarinFormsPage navigablePage)
         {
            await IoC.IoCConfig.Resolve<NavigationPage>().PushAsync(page).ConfigureAwait(true);
            navigablePage.OnNavigatedTo(navigationParameters);
         }
      }

      public Task PopAsync() => IoC.IoCConfig.Resolve<NavigationPage>().PopAsync();

      private static Page GetPage(PageNames page)
      {
         switch (page)
         {
            case PageNames.MainPage:
               return IoC.IoCConfig.Resolve<MainPage>();

            case PageNames.FilesPage:
               return IoC.IoCConfig.Resolve<FilesPage>();

            case PageNames.EditFilePage:
               return IoC.IoCConfig.Resolve<EditFilePage>();

            default:
               throw new ArgumentOutOfRangeException(nameof(page), page, null);
         }
      }
   }
}