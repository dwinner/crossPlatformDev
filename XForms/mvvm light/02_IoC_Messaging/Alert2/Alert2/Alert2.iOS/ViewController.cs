using System;
using Alert2.ViewModel;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Views;
using UIKit;

namespace Alert2.iOS
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
         ViewModel.ButtonText = "Click me the alert";
         this.SetBinding(
            () => ViewModel.ButtonText,
            () => Button.Title(UIControlState.Normal),
            BindingMode.TwoWay);
      }
   }
}