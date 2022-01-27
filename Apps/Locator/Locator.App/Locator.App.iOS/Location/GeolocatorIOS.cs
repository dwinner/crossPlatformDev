using System.Diagnostics;
using System.Linq;
using System.Reactive.Subjects;
using CoreLocation;
using Locator.Common.Location;
using UIKit;

namespace Locator.App.iOS.Location
{
   /// <summary>
   ///    Geolocator ios.
   /// </summary>
   public class GeolocatorIos : IGeolocator
   {
      /// <summary>
      ///    The location manager.
      /// </summary>
      private readonly CLLocationManager _locationManager;

      /// <summary>
      ///    Initializes a new instance of the <see cref="GeolocatorIos"/> class.
      /// </summary>
      public GeolocatorIos()
      {
         Positions = new Subject<IPosition>();

         _locationManager = new CLLocationManager {PausesLocationUpdatesAutomatically = false};

         // iOS 8 has additional permissions requirements
         if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
         {
            _locationManager.RequestWhenInUseAuthorization();
         }

         if (UIDevice.CurrentDevice.CheckSystemVersion(9, 0))
         {
            _locationManager.AllowsBackgroundLocationUpdates = true;
         }
      }

      /// <summary>
      ///    Gets or sets the positions.
      /// </summary>
      /// <value>The positions.</value>
      public Subject<IPosition> Positions { get; set; }

      /// <summary>
      ///    Start this instance.
      /// </summary>
      public void Start()
      {
         if (CLLocationManager.LocationServicesEnabled)
         {
            //set the desired accuracy, in meters
            _locationManager.DesiredAccuracy = 1;
            _locationManager.LocationsUpdated += OnLocationsUpdated;
            _locationManager.StartUpdatingLocation();
         }
      }

      /// <summary>
      ///    Stop this instance.
      /// </summary>
      public void Stop()
      {
         _locationManager.LocationsUpdated -= OnLocationsUpdated;
         _locationManager.StopUpdatingLocation();
      }

      /// <summary>
      ///    Handles the locations updated.
      /// </summary>
      /// <param name="sender">Sender.</param>
      /// <param name="e">E.</param>
      private void OnLocationsUpdated(object sender, CLLocationsUpdatedEventArgs e)
      {
         var location = e.Locations.LastOrDefault();
         if (location != null)
         {
            Debug.WriteLine(
               $"Location updated, position: {location.Coordinate.Latitude}-{location.Coordinate.Longitude}");

            // fire our custom Location Updated event
            Positions.OnNext(new Position
            {
               Latitude = location.Coordinate.Latitude,
               Longitude = location.Coordinate.Longitude
            });
         }
      }
   }
}