using System.ComponentModel;

namespace SocialClientApp
{
   [DesignTimeVisible(false)]
   public partial class FriendListPage
   {
      private readonly ApplicationViewModel _viewModel;

      public FriendListPage()
      {
         InitializeComponent();
         _viewModel = new ApplicationViewModel {Navigation = Navigation};
         BindingContext = _viewModel;
      }

      protected override async void OnAppearing()
      {
         await _viewModel.GetFriendsAsync().ConfigureAwait(true);
         base.OnAppearing();
      }
   }
}