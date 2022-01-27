using Windows.UI;
using Windows.UI.ViewManagement;

namespace MvvxSandboxApp.UWP
{
   public sealed partial class MainPage
   {
      public MainPage()
      {
         InitializeComponent();

         var titleBar = ApplicationView.GetForCurrentView().TitleBar;
         titleBar.ForegroundColor = Colors.White;
         titleBar.BackgroundColor = Colors.Black;
         titleBar.ButtonForegroundColor = Colors.White;
         titleBar.ButtonBackgroundColor = Colors.Black;
         titleBar.ButtonHoverBackgroundColor = Colors.Red;
         titleBar.ButtonHoverForegroundColor = Colors.White;
      }
   }
}