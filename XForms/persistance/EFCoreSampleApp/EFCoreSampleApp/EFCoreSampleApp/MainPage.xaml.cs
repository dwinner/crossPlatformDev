using System;
using System.Linq;
using Xamarin.Forms;

namespace EFCoreSampleApp
{
   public partial class MainPage
   {
      public MainPage()
      {
         InitializeComponent();
      }

      protected override void OnAppearing()
      {
         var dbPath = DependencyService.Get<IDbPath>().GetDbPath(App.DbFileName);
         using (var dbContext = new ApplicationContext(dbPath))
         {
            friendList.ItemsSource = dbContext.Friends.ToList();
         }

         base.OnAppearing();
      }

      private void OnCreateFriend(object sender, EventArgs e)
      {
         var friendPage = new FriendPage {BindingContext = new Friend()};
         Navigation.PushAsync(friendPage);
      }

      private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
      {
         var selectedFriend = (Friend) e.SelectedItem;
         var friendPage = new FriendPage {BindingContext = selectedFriend};
         Navigation.PushAsync(friendPage);
      }
   }
}