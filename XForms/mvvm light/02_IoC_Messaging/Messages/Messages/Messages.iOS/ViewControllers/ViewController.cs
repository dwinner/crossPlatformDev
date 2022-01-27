using System;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Views;
using Messages.ViewModel;
using UIKit;

namespace Messages.iOS.ViewControllers
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
         // Perform any additional setup after loading the view, typically from a nib.
         Button.AccessibilityIdentifier = "myButton";
         Button.SetCommand(nameof(UIButton.TouchUpInside), ViewModel.BtnClick);
      }
   }
}