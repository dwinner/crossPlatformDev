using geolocation_plugin.Models;

namespace geolocation_plugin.Interfaces
{
   public interface IGeolocation
   {
      GeoData GetLocationData();

      bool IsListening();

      void StartListening();
   }
}