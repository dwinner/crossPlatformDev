using PaperBoy.Common.Commands;
using PaperBoy.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PaperBoy.Pages
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class FavoritesPage : ContentPage
   {
      public FavoritesPage()
      {
         InitializeComponent();
      }

      protected override void OnAppearing()
      {
         LoadFavorites();

         base.OnAppearing();
      }

      private async void LoadFavorites()
      {
         BindingContext = App.ViewModel;
         await App.ViewModel.RefreshFavoritesAsync();
      }

      private void OnItemTapped(object sender, ItemTappedEventArgs e)
      {
         new NavigateToDetailCommand().Execute(e.Item as FavoriteInfromation);
      }
   }
}