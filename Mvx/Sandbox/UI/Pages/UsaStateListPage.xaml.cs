using MvvmCross.Forms.Presenters.Attributes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MvvxSandboxApp.UI.Pages
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   [MvxContentPagePresentation(
      WrapInNavigationPage = true,
      NoHistory = true,
      Title = "USA States")]
   public partial class UsaStateListPage
   {
      public UsaStateListPage()
      {
         InitializeComponent();
      }

      protected override void OnAppearing()
      {
         base.OnAppearing();

         if (Application.Current.MainPage is NavigationPage navigationPage)
         {
            navigationPage.BarTextColor = Color.White;
            navigationPage.BarBackgroundColor = (Color) Application.Current.Resources["primaryColor"];
         }
      }
   }
}