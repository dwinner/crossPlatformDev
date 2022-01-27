using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using FootballCards.SharedLib.ViewModel;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using AndroidUri = Android.Net.Uri;

namespace FootballCards.Droid.App
{
   [Activity(Label = nameof(MappingActivity))]
   public partial class MappingActivity : Activity
   {
      public MapViewModel ViewModel => App.Locator.Map;

      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);
         GetDataFromViewModel();
      }

      private void LaunchMap()
      {
         var geoUri = AndroidUri.Parse($"geo:{ViewModel.Latitude},{ViewModel.Longitude}");
         var mapIntent = new Intent(Intent.ActionView, geoUri);
         StartActivity(mapIntent);
      }

      private void GetDataFromViewModel()
      {
         var navigationService = (NavigationService) SimpleIoc.Default.GetInstance<INavigationService>();
         var data = navigationService.GetAndRemoveParameter<List<double>>(Intent);
         if (data != null)
         {
            ViewModel.Latitude = data[0];
            ViewModel.Longitude = data[1];
            LaunchMap();
         }
      }
   }
}