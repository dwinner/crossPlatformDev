using System.Threading.Tasks;
using ContactMvvmApp.Interfaces;
using Xamarin.Forms;

namespace ContactMvvmApp.Impl
{
   public class PageService : IPageService
   {
      private static Page MainPage => Application.Current.MainPage;

      public Task DisplayAlertAsync(string title, string message, string ok) =>
         MainPage.DisplayAlert(title, message, ok);

      public Task<bool> DisplayAlertAsync(string title, string message, string ok, string cancel) =>
         MainPage.DisplayAlert(title, message, ok, cancel);

      public Task PushAsync(Page page) => MainPage.Navigation.PushAsync(page);

      public Task<Page> PopAsync() => MainPage.Navigation.PopAsync();
   }
}