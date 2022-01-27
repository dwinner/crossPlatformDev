using System;
using System.ComponentModel;
using Realms;
using Xamarin.Forms;

namespace RealmSampleApp
{
   // Learn more about making custom code visible in the Xamarin.Forms previewer
   // by visiting https://aka.ms/xamarinforms-previewer
   [DesignTimeVisible(false)]
   public partial class MainPage
   {
      private readonly Realm _realm;

      public MainPage()
      {
         InitializeComponent();
         _realm = Realm.GetInstance();
      }

      protected override void OnAppearing()
      {
         friendList.ItemsSource = _realm.All<Friend>();
         base.OnAppearing();
      }

      private async void OnItemTapped(object sender, ItemTappedEventArgs e)
      {
         var selectedFriend = e.Item as Friend;
         var friendPage = new FriendPage {BindingContext = selectedFriend};
         await Navigation.PushAsync(friendPage).ConfigureAwait(true);
      }

      private async void OnCreateFriend(object sender, EventArgs e)
      {
         var friendPage = new FriendPage {BindingContext = new Friend()};
         await Navigation.PushAsync(friendPage).ConfigureAwait(true);
      }
   }
}