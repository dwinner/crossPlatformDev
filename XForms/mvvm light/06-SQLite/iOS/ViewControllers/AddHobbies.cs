using System;
using GalaSoft.MvvmLight.Helpers;
using SQLiteExample.iOS.Helpers;
using SQLiteExample.ViewModel;
using UIKit;

namespace SQLiteExample.iOS.ViewControllers
{
   public partial class AddHobbies : UIViewController
   {
      private PickerDataModel _pickerDataModel;

      public AddHobbies(IntPtr handle) : base(handle)
      {
      }

      private static AddHobbiesViewModel ViewModel => AppDelegate.Locator.AddHobbiesVm;

      public override void ViewDidLoad()
      {
         base.ViewDidLoad();
         BindTextViews();
         BindButton();
         BindSpinner();
      }

      private void BindTextViews()
      {
         this.SetBinding(
            () => ViewModel.HobbyCost,
            () => txtCost.Text,
            BindingMode.TwoWay);

         this.SetBinding(
            () => ViewModel.HobbyDesc,
            () => txtDescription.Text,
            BindingMode.TwoWay);

         this.SetBinding(
            () => ViewModel.HobbyName,
            () => txtHobbyName.Text,
            BindingMode.TwoWay);
      }

      private void BindButton()
      {
         btnAddToDatabase.SetCommand(Events.Events.TouchUpInside, ViewModel.BtnCommit);
      }

      private void BindSpinner()
      {
         _pickerDataModel = new PickerDataModel();
         foreach (var i in ViewModel.GetFrequencies)
         {
            _pickerDataModel.Items.Add(i);
         }

         pckFrequency.Model = _pickerDataModel;

         _pickerDataModel.ValueChanged += delegate { ViewModel.Frequency = _pickerDataModel.SelectedItem; };
      }
   }
}