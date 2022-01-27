using System;
using System.Collections.Generic;
using CoreLocation;
using FootballCards.SharedLib.ViewModel;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using MapKit;
using UIKit;

namespace FootballCards.iOs.App
{
   public partial class MapViewController : UIViewController
   {
      public MapViewController(IntPtr handle) : base(handle)
      {
      }

      public MapViewModel ViewModel => AppDelegate.Locator.Map;

      public override void ViewDidLoad()
      {
         base.ViewDidLoad();
         GetDataFromViewModel();
         GenerateMap();
      }

      private void GetDataFromViewModel()
      {
         var navigationService = (NavigationService) SimpleIoc.Default.GetInstance<INavigationService>();
         if (navigationService.GetAndRemoveParameter(NavigationController) is List<double> data)
         {
            ViewModel.Latitude = data[0];
            ViewModel.Longitude = data[1];
         }
      }

      private void GenerateMap()
      {
         mapView.ZoomEnabled = mapView.ScrollEnabled = mapView.UserInteractionEnabled = true;
         mapView.MapType = MKMapType.Standard;
         mapView.Region = new MKCoordinateRegion(
            new CLLocationCoordinate2D(ViewModel.Latitude, ViewModel.Longitude),
            new MKCoordinateSpan(.5, .5));
      }
   }
}