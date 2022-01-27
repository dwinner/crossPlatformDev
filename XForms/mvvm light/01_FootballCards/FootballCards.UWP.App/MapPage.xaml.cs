using System;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Controls.Maps;

namespace FootballCards.UWP.App
{
   public sealed partial class MapPage
   {
      public MapPage()
      {
         InitializeComponent();
      }

      private async Task SetMapViewAsync(Geopoint point)
      {
         await mapView.TrySetViewAsync(point, 15, 0, 0, MapAnimationKind.Bow);
      }
   }
}