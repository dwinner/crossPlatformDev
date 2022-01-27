using Xamarin.Forms;

namespace SqLiteSampleApp
{
   public partial class App
   {
      public const string DatabaseName = "friends2.db";

      private static FriendRepository _FriendRepository;

      public App()
      {
         InitializeComponent();
         MainPage = new NavigationPage(new MainPage());
      }

      public static FriendRepository FriendRepository =>
         _FriendRepository ?? (_FriendRepository = new FriendRepository(DatabaseName));
   }
}