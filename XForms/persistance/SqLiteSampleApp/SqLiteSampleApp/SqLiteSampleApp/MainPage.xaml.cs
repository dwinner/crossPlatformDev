using System;
using System.ComponentModel;
using Xamarin.Forms;

// ReSharper disable AvoidAsyncVoid

namespace SqLiteSampleApp
{
   [DesignTimeVisible(false)]
   public partial class MainPage
   {
      public MainPage()
      {
         InitializeComponent();
      }

      protected override async void OnAppearing()
      {
         await App.FriendRepository.CreateTableAsync().ConfigureAwait(true);
         friendList.ItemsSource = await App.FriendRepository.GetItemsAsync().ConfigureAwait(true);
         base.OnAppearing();
      }

      private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
      {
         if (e.SelectedItem is Friend selectedFriend)
         {
            var friendPage = new FriendPage {BindingContext = selectedFriend};
            await Navigation.PushAsync(friendPage).ConfigureAwait(true);
         }
      }

      private void OnCreateFriend(object sender, EventArgs e)
      {
         var newFriend = new Friend();
         var friendPage = new FriendPage {BindingContext = newFriend};
         Navigation.PushAsync(friendPage);
      }
   }
}