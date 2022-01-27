using System;
using IslandMenu.App.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IslandMenu.App.Views
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class MenuList
   {
      public MenuList(Restaurant restaurant)
      {
         BindingContext = restaurant;
         Title = restaurant.Name;
         InitializeComponent();
      }

      private void OnBindingContextChanged(object sender, EventArgs e)
      {
         base.OnBindingContextChanged();

         var cell = (ViewCell) sender;
         switch (cell.BindingContext)
         {
            case Restaurant restaurant:
            {
               var l = restaurant.Name.Length;
               var offset = l / 50 * 20;
               cell.Height = 100 + offset;
               break;
            }

            case RestaurantMenuItem restaurantMenuItem:
            {
               var l = restaurantMenuItem.Description.Length;
               var offset = l / 50 * 20;
               cell.Height = 100 + offset;
               break;
            }
         }
      }
   }
}