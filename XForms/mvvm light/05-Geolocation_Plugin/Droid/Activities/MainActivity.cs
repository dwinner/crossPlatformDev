using System;
using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;
using geolocation_plugin.ViewModel;
using Plugin.Permissions;

namespace geolocation_plugin.Droid.Activities
{
   [Activity(Label = "geolocation_plugin", MainLauncher = true)]
   public partial class MainActivity : Activity
   {
      private const int RequestLocations = 0;
      private static readonly string[] _PermissionsLocation = {Manifest.Permission_group.Location};

      private View _layout;
      private static MainViewModel ViewModel => ViewModelLocator.Main;

      public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
      {
         PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
         base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
      }

      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);

         // Set our view from the "main" layout resource
         SetContentView(Resource.Layout.Main);

         _layout = FindViewById<LinearLayout>(Resource.Id.main_layout);

         if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
         {
            RequestLocationPermission();
         }

         BindTextViews();
         BindButton();
      }

      private void RequestLocationPermission()
      {
         try
         {
            if (ContextCompat.CheckSelfPermission(ApplicationContext, Manifest.Permission_group.Location) != Permission.Granted
                && ActivityCompat.ShouldShowRequestPermissionRationale(this, Manifest.Permission_group.Location))
            {
               RunOnUiThread(() =>
               {
                  Snackbar.Make(_layout, "Location access is required for this app.", Snackbar.LengthIndefinite)
                     .SetAction("OK", v => RequestPermissions(_PermissionsLocation, RequestLocations)).Show();
               });
            }
         }
         catch (Exception ex)
         {
            Console.WriteLine($"ex = {ex.Message}");
         }
      }

      private void BindTextViews()
      {
         this.SetBinding(
            () => ViewModel.Altitude,
            () => TxtAlt.Text,
            BindingMode.TwoWay);

         this.SetBinding(
            () => ViewModel.Heading,
            () => TxtHeading.Text,
            BindingMode.TwoWay);

         this.SetBinding(
            () => ViewModel.Latitude,
            () => TxtLat.Text,
            BindingMode.TwoWay);

         this.SetBinding(
            () => ViewModel.Longitude,
            () => TxtLong.Text,
            BindingMode.TwoWay);

         this.SetBinding(
            () => ViewModel.Speed,
            () => TxtSpeed.Text,
            BindingMode.TwoWay);
      }

      private void BindButton() => BtnListen.SetCommand("Click", ViewModel.BtnStart);
   }
}