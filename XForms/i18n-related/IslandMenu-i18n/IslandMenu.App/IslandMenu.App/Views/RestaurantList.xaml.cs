using IslandMenu.App.Models;
using IslandMenu.App.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IslandMenu.App.Views
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class RestaurantList
   {
      public RestaurantList()
      {
         InitializeComponent();

         var viewModel = new RestaurantsViewModel {Navigation = Navigation};

         img.BindingContext = viewModel;
         lastUpdateLabel.BindingContext = viewModel;

         BindingContext = viewModel.Restaurants;
      }

      private async void OnItemTapped(object sender, ItemTappedEventArgs e)
      {
         var restaurant = e.Item as Restaurant;
         await Navigation.PushAsync(new MenuList(restaurant)).ConfigureAwait(true);
         ((ListView) sender).SelectedItem = null;
      }
   }
}