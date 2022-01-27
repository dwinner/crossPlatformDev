using FileStorageApp.ViewModels;
using Xamarin.Forms;

namespace FileStorageApp.UI
{
   public class ExtendedContentPage : ContentPage
   {
      protected ExtendedContentPage(ViewModelBase model)
      {
         model.Alert -= HandleAlert;
         model.Alert += HandleAlert;
      }

      private void HandleAlert(object sender, string e) => DisplayAlert("FileStorage", e, "Ok");
   }
}