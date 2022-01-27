using CoreLocation;
using MapKit;

namespace Users.MobileClient.Helpers
{
   public class PinMapAnnotation : MKAnnotation
   {
      private CLLocationCoordinate2D coordinate;

      public PinMapAnnotation(CLLocationCoordinate2D coordinate, string title)
      {
         this.coordinate = coordinate;
         Title = title;
      }

      public override CLLocationCoordinate2D Coordinate => coordinate;

      public override string Title { get; }

      public override void SetCoordinate(CLLocationCoordinate2D value) => coordinate = value;
   }
}