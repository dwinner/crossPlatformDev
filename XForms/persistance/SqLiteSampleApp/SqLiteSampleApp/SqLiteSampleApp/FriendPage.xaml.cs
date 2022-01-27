using System;
using Xamarin.Forms.Xaml;

namespace SqLiteSampleApp
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class FriendPage
   {
      private readonly FriendRepository _repository = App.FriendRepository;

      public FriendPage()
      {
         InitializeComponent();
      }

      private async void OnSaveFriend(object sender, EventArgs e)
      {
         var friend = (Friend) BindingContext;
         if (!string.IsNullOrEmpty(friend.Name))
         {
            await _repository.SaveItemAsync(friend).ConfigureAwait(true);
         }

         await Navigation.PopAsync().ConfigureAwait(true);
      }

      private async void OnDeleteFriend(object sender, EventArgs e)
      {
         var friend = (Friend) BindingContext;
         await _repository.DeleteItemAsync(friend.Id).ConfigureAwait(true);
         await Navigation.PopAsync().ConfigureAwait(true);
      }

      private void OnCancel(object sender, EventArgs e)
      {
         Navigation.PopAsync();
      }
   }
}