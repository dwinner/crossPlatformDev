using System;
using GalaSoft.MvvmLight.Helpers;
using SQLiteExample.ViewModel;
using UIKit;

namespace SQLiteExample.iOS.ViewControllers
{
   public partial class AddPets : UIViewController
   {
      public AddPets(IntPtr handle) : base(handle)
      {
      }

      private static AddPetViewModel ViewModel => AppDelegate.Locator.AddPetsVm;

      public override void ViewDidLoad()
      {
         base.ViewDidLoad();
         BindTextViews();
         BindSwitch();
         BindButton();
      }

      private void BindButton()
      {
         btnAddToDatabase.SetCommand(Events.Events.TouchUpInside, ViewModel.BtnCommit);
      }

      private void BindTextViews()
      {
         this.SetBinding(
            () => ViewModel.Age,
            () => txtAge.Text,
            BindingMode.TwoWay);

         this.SetBinding(
            () => ViewModel.Breed,
            () => txtBreed.Text,
            BindingMode.TwoWay);

         this.SetBinding(
            () => ViewModel.Name,
            () => txtPetName.Text,
            BindingMode.TwoWay);
      }

      private void BindSwitch()
      {
         this.SetBinding(
            () => ViewModel.IsDog,
            () => swchIsDog.On,
            BindingMode.TwoWay);
      }
   }
}