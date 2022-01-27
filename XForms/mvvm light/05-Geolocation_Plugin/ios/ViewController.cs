using System;
using System.Globalization;
using GalaSoft.MvvmLight.Helpers;
using geolocation_plugin.ViewModel;
using UIKit;

namespace geolocation_plugin.iOS
{
   public partial class ViewController : UIViewController
   {
      public ViewController(IntPtr handle) : base(handle)
      {
      }

      private static MainViewModel ViewModel => ViewModelLocator.Main;

      public override void ViewDidLoad()
      {
         base.ViewDidLoad();

         BindLabels();
         BindButton();
      }

      private void BindButton() => btnStartListener.SetCommand(nameof(UIButton.TouchUpInside), ViewModel.BtnStart);

      private void BindLabels()
      {
         this.SetBinding(
            () => ViewModel.Altitude,
            () => lblAlt.Text,
            BindingMode.TwoWay).WhenSourceChanges(UpdateAlt);

         this.SetBinding(
            () => ViewModel.Heading,
            () => lblHeading.Text,
            BindingMode.TwoWay).WhenSourceChanges(UpdateHeading);

         this.SetBinding(
            () => ViewModel.Latitude,
            () => lblLat.Text,
            BindingMode.TwoWay).WhenSourceChanges(UpdateLat);

         this.SetBinding(
            () => ViewModel.Longitude,
            () => lblLong.Text,
            BindingMode.TwoWay).WhenSourceChanges(UpdateLong);

         this.SetBinding(
            () => ViewModel.Speed,
            () => lblSpeed.Text,
            BindingMode.TwoWay).WhenSourceChanges(UpdateSpeed);
      }

      private void UpdateAlt()
      {
         lblAlt.Text = ViewModel.Altitude.ToString(CultureInfo.CurrentCulture);
      }

      private void UpdateHeading()
      {
         lblHeading.Text = ViewModel.Heading.ToString(CultureInfo.CurrentCulture);
      }

      private void UpdateLat()
      {
         lblLat.Text = ViewModel.Latitude.ToString(CultureInfo.CurrentCulture);
      }

      private void UpdateLong()
      {
         lblLong.Text = ViewModel.Longitude.ToString(CultureInfo.CurrentCulture);
      }

      private void UpdateSpeed()
      {
         lblSpeed.Text = ViewModel.Speed.ToString(CultureInfo.CurrentCulture);
      }
   }
}