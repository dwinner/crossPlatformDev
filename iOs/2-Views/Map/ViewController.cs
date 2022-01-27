using System;
using System.Linq;
using CoreLocation;
using MapKit;
using UIKit;

namespace Map
{
   public partial class ViewController : UIViewController
   {
      private const double SpanDelta = 0.005d;
      private CLLocationManager _locationManager;
      private MKMapView _map;

      protected ViewController(IntPtr handle) : base(handle)
      {
         // Note: this .ctor should not contain any initialization logic.
      }

      public override void ViewDidLoad()
      {
         base.ViewDidLoad();
         InitMap();
         InitLocationManager();
      }

      public override void ViewDidAppear(bool animated) => _locationManager.StartUpdatingLocation();

      public override void ViewDidDisappear(bool animated)
      {
         base.ViewDidDisappear(animated);
         _locationManager.StopUpdatingLocation();
      }

      private void InitMap()
      {
         _map = new MKMapView(View.Frame)
         {
            ZoomEnabled = true,
            ScrollEnabled = true,
            ShowsUserLocation = true,
            MapType = MKMapType.HybridFlyover
         };

         Add(_map);
      }

      private void InitLocationManager()
      {
         _locationManager = new CLLocationManager();

         // Request authorization
         if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
         {
            _locationManager.RequestWhenInUseAuthorization();
         }

         // Handle LocationsUpdated event
         _locationManager.LocationsUpdated += (sender, e) => { UpdateMap(e); };
      }

      private void UpdateMap(CLLocationsUpdatedEventArgs e)
      {
         var location = e.Locations.LastOrDefault();

         if (location != null)
         {
            _map.CenterCoordinate = location.Coordinate;
            SetMapRegion(location.Coordinate);
         }
      }

      private void SetMapRegion(CLLocationCoordinate2D centerCoordinate)
      {
         var span = new MKCoordinateSpan(SpanDelta, SpanDelta);
         var region = new MKCoordinateRegion(centerCoordinate, span);
         _map.SetRegion(region, false);
      }
   }
}