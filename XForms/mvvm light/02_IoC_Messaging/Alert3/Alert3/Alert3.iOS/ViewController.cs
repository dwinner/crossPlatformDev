using System;
using Alert3.ViewModel;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Views;
using UIKit;

namespace Alert3.iOS
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

         Button.AccessibilityIdentifier = "myButton";
         Button.SetCommand(nameof(UIButton.TouchUpInside), ViewModel.BtnAlert);
      }
   }
}