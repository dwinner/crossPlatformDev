using System;
using System.Reactive.Subjects;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Locator.Common.Location;
using Application = Android.App.Application;
using Debug = System.Diagnostics.Debug;
using Object = Java.Lang.Object;

namespace Locator.App.Droid.Location
{
   /// <summary>
   ///    Geolocator droid.
   /// </summary>
   public class GeolocatorDroid : Object, IGeolocator, ILocationListener
   {
      /// <summary>
      ///    The location manager.
      /// </summary>
      private readonly LocationManager _locationManager;

      /// <summary>
      ///    The provider.
      /// </summary>
      private readonly string _provider;

      /// <summary>
      ///    Initializes a new instance of the <see cref="GeolocatorDroid" /> class.
      /// </summary>
      public GeolocatorDroid()
      {
         Positions = new Subject<IPosition>();
         _locationManager = (LocationManager) Application.Context.GetSystemService(Context.LocationService);
         _provider = LocationManager.NetworkProvider;
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
         if (_locationManager.IsProviderEnabled(_provider))
         {
            _locationManager.RequestLocationUpdates(_provider, 2000, 1, this);
         }
         else
         {
            Debug.WriteLine(
               $"{_provider} is not available. Does the device have location services enabled?");
         }
      }

      /// <summary>
      ///    Stop this instance.
      /// </summary>
      public void Stop() => _locationManager.RemoveUpdates(this);

      /// <summary>
      ///    Called when the location changed.
      /// </summary>
      /// <returns>The location changed.</returns>
      /// <param name="location">Location.</param>
      public void OnLocationChanged(Android.Locations.Location location)
      {
         Positions.OnNext(new Position
         {
            Latitude = location.Latitude,
            Longitude = location.Longitude
         });
      }

      /// <summary>
      ///    Called when the provider becomes disabled.
      /// </summary>
      /// <returns>The provider disabled.</returns>
      /// <param name="aProvider">Provider.</param>
      public void OnProviderDisabled(string aProvider)
      {
      }

      /// <summary>
      ///    Called when the provider becomes enabled.
      /// </summary>
      /// <returns>The provider enabled.</returns>
      /// <param name="aProvider">Provider.</param>
      public void OnProviderEnabled(string aProvider)
      {
      }

      /// <summary>
      ///    Called when the status changed.
      /// </summary>
      /// <returns>The status changed.</returns>
      /// <param name="aProvider">Provider.</param>
      /// <param name="status">Status.</param>
      /// <param name="extras">Extras.</param>
      public void OnStatusChanged(string aProvider, [GeneratedEnum] Availability status, Bundle extras)
      {
      }
   }
}