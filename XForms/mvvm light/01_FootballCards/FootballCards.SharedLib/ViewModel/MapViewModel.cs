using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;

namespace FootballCards.SharedLib.ViewModel
{
   public class MapViewModel : ViewModelBase
   {
      private double _latitude;
      private double _longitude;
      private INavigationService _navigationService;

      public MapViewModel(INavigationService navigationService) => _navigationService = navigationService;

      public double Latitude
      {
         get => _latitude;
         set { Set(() => Latitude, ref _latitude, value, true); }
      }

      public double Longitude
      {
         get => _longitude;
         set { Set(() => Longitude, ref _longitude, value, true); }
      }
   }
}