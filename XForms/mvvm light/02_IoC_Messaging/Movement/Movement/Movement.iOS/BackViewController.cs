using System;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Views;
using Movement.iOS.Model;
using Movement.ViewModel;

namespace Movement.iOS
{
   public partial class BackViewController : ControllerBase
   {
      public BackViewController(IntPtr handle) : base(handle)
      {
      }

      public BackViewModel ViewModel => AppDelegate.Locator.Back;

      public override void ViewDidLoad()
      {
         base.ViewDidLoad();

         btnBackward.SetCommand(
            Events.TouchUpInside,
            ViewModel.BackButton
         );
      }
   }
}