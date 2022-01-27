using System.ComponentModel;
using System.Linq;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;

namespace ServerInteropApp
{
   // Learn more about making custom code visible in the Xamarin.Forms previewer
   // by visiting https://aka.ms/xamarinforms-previewer
   [DesignTimeVisible(false)]
   public partial class MainPage
   {
      public MainPage()
      {
         InitializeComponent();
         CrossConnectivity.Current.ConnectivityChanged += OnConnectivityChanged;
         BindingContext = new RateViewModel();
      }

      protected override void OnAppearing()
      {
         base.OnAppearing();
         CheckConnection();
      }

      private void CheckConnection()
      {
         connectionStateLabel.IsVisible = !CrossConnectivity.Current.IsConnected;
         connectionDetailsLabel.IsVisible = CrossConnectivity.Current.IsConnected;

         if (CrossConnectivity.Current != null
             && CrossConnectivity.Current.ConnectionTypes != null
             && CrossConnectivity.Current.IsConnected)
         {
            var connectionType = CrossConnectivity.Current.ConnectionTypes.FirstOrDefault();
            connectionDetailsLabel.Text = connectionType.ToString();
         }
      }

      private void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs e) => CheckConnection();
   }
}