using System;
using System.Globalization;
using CoreLocation;
using Foundation;
using WatchKit;

namespace HelloWatchKit.WatchExtension
{
   public partial class CityGeolocationController : WKInterfaceController
   {
      private CLLocation _location;

      public CityGeolocationController(IntPtr handle) : base(handle)
      {
      }

      public override void Awake(NSObject context)
      {
         base.Awake(context);

         if (context is CLLocation location)
         {
            GetLocation(location);
         }
      }

      public override void WillActivate()
      {
         LabelLat.SetText(_location.Coordinate.Latitude.ToString(CultureInfo.CurrentCulture));
         LabelLng.SetText(_location.Coordinate.Longitude.ToString(CultureInfo.CurrentCulture));
      }

      private void GetLocation(CLLocation location) =>
         _location = location ?? LocationHelper.GetLocationForCity(City.SanFrancisco);
   }
}