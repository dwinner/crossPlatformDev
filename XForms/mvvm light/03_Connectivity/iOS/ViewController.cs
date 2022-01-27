using System;
using connectivity.ViewModel;
using Foundation;
using GalaSoft.MvvmLight.Helpers;
using Plugin.Connectivity;
using UIKit;

namespace connectivity.iOS
{
   public partial class ViewController : UIViewController
   {
      public ViewController(IntPtr handle) : base(handle)
      {
      }

      public MainViewModel ViewModel => AppDelegate.Locator.Main;

      public override void ViewDidLoad()
      {
         base.ViewDidLoad();

         CrossConnectivity.Current.ConnectivityChanged += (sender, e) => { ViewModel.IsOnline = e.IsConnected; };

         // Perform any additional setup after loading the view, typically from a nib.
         DoBindings();

         ViewModel.IsOnline = CrossConnectivity.Current.IsConnected;
      }

      private void DoBindings()
      {
         Button.SetCommand(nameof(UIButton.TouchUpInside), ViewModel.WebsiteCommand);

         ViewModel.PropertyChanged += (sender, e) =>
         {
            if (e.PropertyName == nameof(ViewModel.GoWebsite))
            {
               if (ViewModel.GoWebsite)
               {
                  InvokeOnMainThread(() =>
                     wvWeb.LoadRequest(new NSUrlRequest(new Uri("http://www.liverpoolfc.net"))));
               }
               else
               {
                  InvokeOnMainThread(() =>
                     wvWeb.LoadHtmlString("<html><body><p><b>Connection error</b></p></body></html>", null));
               }
            }
         };
      }
   }
}