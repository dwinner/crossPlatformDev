using CoreLocation;

namespace HelloWatchKit.WatchExtension
{
   public static class LocationHelper
   {
      public static CLLocation GetLocationForCity(City city)
      {
         CLLocation result;

         switch (city)
         {
            case City.NewYork:
               result = new CLLocation(40.7127837, -74.0059413);
               break;

            case City.SanFrancisco:
               result = new CLLocation(37.77493, -122.419416);
               break;

            default:
               goto case City.SanFrancisco;
         }

         return result;
      }
   }

   public enum City
   {
      NewYork,
      SanFrancisco
   }
}