using MvvmSampleApp.Views;
using Xamarin.Forms;

namespace MvvmSampleApp
{
   public partial class App
   {
      public App()
      {
         InitializeComponent();
         MainPage = new NavigationPage(new FriendListPage());
      }
   }
}