using System;
using GalaSoft.MvvmLight.Helpers;
using SQLiteExample.ViewModel;
using UIKit;

namespace SQLiteExample.iOS.ViewControllers
{
   public partial class ViewController : UIViewController
   {
      public ViewController(IntPtr handle) : base(handle)
      {
      }

      private MainViewModel ViewModel => AppDelegate.Locator.MainVm;

      public override void ViewDidLoad()
      {
         base.ViewDidLoad();

         BindLabels();
         BindTextViews();
         BindButton();
      }

      private void BindLabels()
      {
         this.SetBinding(
            () => ViewModel.GetCurrentId.ToString(),
            () => lblCurrentId.Text,
            BindingMode.TwoWay);

         this.SetBinding(
            () => ViewModel.GetTotal.ToString(),
            () => lblRecords.Text,
            BindingMode.TwoWay);
      }

      private void BindTextViews()
      {
         this.SetBinding(
            () => ViewModel.UserName,
            () => txtName.Text,
            BindingMode.TwoWay);

         this.SetBinding(
            () => ViewModel.AddressOne,
            () => txtAddressOne.Text,
            BindingMode.TwoWay);

         this.SetBinding(
            () => ViewModel.AddressTwo,
            () => txtAddressTwo.Text,
            BindingMode.TwoWay);

         this.SetBinding(
            () => ViewModel.AddressThree,
            () => txtAddressThree.Text,
            BindingMode.TwoWay);

         this.SetBinding(
            () => ViewModel.Email,
            () => txtEmail.Text,
            BindingMode.TwoWay);

         this.SetBinding(
            () => ViewModel.MobileNumber,
            () => txtMobile.Text,
            BindingMode.TwoWay);

         this.SetBinding(
            () => ViewModel.Postcode,
            () => txtPostcode.Text,
            BindingMode.TwoWay);
      }

      private void BindButton()
      {
         ViewModel.SetParentId = ViewModel.GetCurrentId;
         btnAddToDB.SetCommand(Events.Events.TouchUpInside, ViewModel.BtnCommit);
      }
   }
}