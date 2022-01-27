using System;
using BehaviorsSampleApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BehaviorsSampleApp.Views
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class ClearSelectedItemAttachedPropertyPage : ContentPage
   {
      public ClearSelectedItemAttachedPropertyPage()
      {
         InitializeComponent();
      }

      private void OnNavigateNextPageReceived(object sender, EventArgs e)
      {
         var nextPage = new SelectionResultPage();
         var nextPageViewModel = (SelectionResultPageViewModel) nextPage.BindingContext;
         nextPageViewModel.Fruit = ((SelectedFruitEventArgs) e).SelectedFruit;
         Navigation.PushAsync(nextPage);
      }
   }
}