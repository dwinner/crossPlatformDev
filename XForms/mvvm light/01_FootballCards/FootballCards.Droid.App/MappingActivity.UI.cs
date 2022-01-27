using Android.Gms.Maps;

namespace FootballCards.Droid.App
{
   public partial class MappingActivity
   {
      private MapView _mapView;

      public MapView MapView => _mapView ?? (_mapView = FindViewById<MapView>(Resource.Id.mapView));
   }
}