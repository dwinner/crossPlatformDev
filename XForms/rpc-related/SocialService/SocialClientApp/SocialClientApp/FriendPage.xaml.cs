using MobileClient;
using Xamarin.Forms.Xaml;

namespace SocialClientApp
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class FriendPage
   {
      private FriendPage()
      {
         InitializeComponent();
      }

      public FriendPage(Friend tempFriend, ApplicationViewModel viewModel)
         : this()
      {
         Model = tempFriend;
         ViewModel = viewModel;
         BindingContext = this;
      }

      public Friend Model { get; }

      public ApplicationViewModel ViewModel { get; }
   }
}