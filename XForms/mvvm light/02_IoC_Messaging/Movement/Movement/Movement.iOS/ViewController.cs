using System;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Views;
using Movement.iOS.Model;
using Movement.ViewModel;

namespace Movement.iOS
{
   public partial class ViewController : ControllerBase
   {
      public ViewController(IntPtr handle) : base(handle)
      {
      }

      public MainViewModel ViewModel => AppDelegate.Locator.Main;

      public override void ViewDidLoad()
      {
         base.ViewDidLoad();

         Button.AccessibilityIdentifier = "btnForward";
         Button.SetCommand(
            Events.TouchUpInside,
            ViewModel.ForwardButton
         );
      }
   }
}