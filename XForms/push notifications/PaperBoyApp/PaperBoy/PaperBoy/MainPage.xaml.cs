using PaperBoy.ViewModels;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PaperBoy
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class MainPage : TabbedPage
   {
      public MainPage()
      {
         InitializeComponent();
      }

      protected override void OnAppearing()
      {
         if (App.ViewModel == null)
         {
            App.ViewModel = new MainViewModel();
            App.ViewModel.RefreshNewsAsync();
         }

         App.MainNavigation = Navigation;

         CrossConnectivity.Current.ConnectivityChanged += OnConnectivityChannged;
         base.OnAppearing();
      }

      private void OnConnectivityChannged(object sender, ConnectivityChangedEventArgs e)
      {
         //TODO: Method Impilimentation
      }
   }
}