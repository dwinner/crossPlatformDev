using System;
using Realms;
using Xamarin.Forms.Xaml;

namespace RealmSampleApp
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class FriendPage
   {
      private readonly Realm _realm;
      private readonly Transaction _transaction;

      public FriendPage()
      {
         InitializeComponent();
         _realm = Realm.GetInstance();
         _transaction = _realm.BeginWrite();
      }

      private void OnSaveFriend(object sender, EventArgs e)
      {
         var friend = (Friend) BindingContext;
         if (!string.IsNullOrEmpty(friend.Name))
         {
            if (friend.Id == null)
            {
               friend.Id = Guid.NewGuid().ToString();
               _realm.Add(friend);
            }

            _transaction.Commit();
         }

         Navigation.PopAsync();
      }

      private void OnDeleteFriend(object sender, EventArgs e)
      {
         var friend = (Friend) BindingContext;
         _realm.Remove(friend);
         _transaction.Commit();
      }

      protected override void OnDisappearing()
      {
         _transaction?.Dispose();
         base.OnDisappearing();
      }
   }
}