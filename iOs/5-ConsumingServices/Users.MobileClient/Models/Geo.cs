using CoreLocation;

namespace Users.MobileClient.Models
{
   public class Geo
   {
      public double Lat { get; set; }

      public double Lng { get; set; }

      public CLLocationCoordinate2D ToCLLocationCoordinate2D() => new CLLocationCoordinate2D(Lat, Lng);
   }
}