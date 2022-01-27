using System;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Views;
using Messages.ViewModel;

namespace Messages.iOS.ViewControllers
{
   public partial class SecondViewController : ControllerBase
   {
      public SecondViewController(IntPtr handle) : base(handle)
      {
      }

      public SecondViewModel ViewModel => AppDelegate.Locator.SecondVM;

      public override void ViewDidLoad()
      {
         base.ViewDidLoad();
         BindTextViews();
      }

      private void BindTextViews()
      {
         this.SetBinding(
            () => ViewModel.TextData1,
            () => txtData1.Text,
            BindingMode.TwoWay);

         this.SetBinding(
            () => ViewModel.TextData2,
            () => txtData2.Text,
            BindingMode.TwoWay);

         this.SetBinding(
            () => ViewModel.TextData3,
            () => txtData3.Text,
            BindingMode.TwoWay);

         this.SetBinding(
            () => ViewModel.TextData4,
            () => txtData4.Text,
            BindingMode.TwoWay);
      }
   }
}