using System;
using System.Diagnostics;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Locator.Common.Location;

// ReSharper disable AvoidAsyncVoid

namespace Locator.App.UWP.Location
{
   public class GeolocatorWinPhone : IGeolocator
   {
      /// <summary>
      ///    The windows geolocator.
      /// </summary>
      private readonly Geolocator _geolocator;

      /// <summary>
      ///    Initializes a new instance of the <see cref="T:Locator.WinPhone.Location.GeolocatorIOS" /> class.
      /// </summary>
      public GeolocatorWinPhone()
      {
         Positions = new Subject<IPosition>();
         _geolocator = new Geolocator {DesiredAccuracyInMeters = 50};
      }

      /// <summary>
      ///    Gets or sets the positions.
      /// </summary>
      /// <value>The positions.</value>
      public Subject<IPosition> Positions { get; set; }

      /// <summary>
      ///    Start this instance.
      /// </summary>
      public async void Start()
      {
         try
         {
            var geoposition =
               (await Task.FromResult(
                     _geolocator.GetGeopositionAsync(TimeSpan.FromMinutes(5), TimeSpan.FromSeconds(10)))
                  .ConfigureAwait(false)).GetResults();

            _geolocator.MovementThreshold = 1;
            _geolocator.DesiredAccuracy = PositionAccuracy.High;
            _geolocator.PositionChanged += GeolocatorPositionChanged;

            // push a new position into the sequence
            var position = geoposition.Coordinate.Point.Position;
            Positions.OnNext(new Position
            {
               Latitude = position.Latitude,
               Longitude = position.Longitude
            });
         }
         catch (Exception ex)
         {
            Debug.WriteLine("Error retrieving geoposition - " + ex);
         }
      }

      /// <summary>
      ///    Stop this instance.
      /// </summary>
      public void Stop()
      {
         // remove event handler
         _geolocator.PositionChanged -= GeolocatorPositionChanged;
      }

      /// <summary>
      ///    Geolocator position changed.
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="args"></param>
      private void GeolocatorPositionChanged(Geolocator sender, PositionChangedEventArgs args)
      {
         // push a new position into the sequence
         var position = args.Position.Coordinate.Point.Position;
         Positions.OnNext(new Position
         {
            Latitude = position.Latitude,
            Longitude = position.Longitude
         });
      }
   }
}