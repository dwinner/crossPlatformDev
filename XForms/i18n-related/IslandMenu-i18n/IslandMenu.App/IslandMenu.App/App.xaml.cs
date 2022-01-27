using IslandMenu.App.Views;
using Xamarin.Forms;

namespace IslandMenu.App
{
   public partial class App
   {
      public App()
      {
         InitializeComponent();
         MainPage = new NavigationPage(new RestaurantList());
      }

      protected override void OnStart()
      {
      }

      protected override void OnSleep()
      {
      }

      protected override void OnResume()
      {
      }
   }
}