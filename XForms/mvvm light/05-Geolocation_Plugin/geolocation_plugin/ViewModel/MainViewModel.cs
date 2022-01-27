using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using geolocation_plugin.Interfaces;

namespace geolocation_plugin.ViewModel
{
   public class MainViewModel : ViewModelBase
   {
      private double _altitude;
      private RelayCommand _btnStart;
      private IDialogService _diaService;
      private readonly IGeolocation _geoService;
      private double _heading;
      private double _latitude;
      private double _longitude;
      private INavigationService _navService;
      private double _speed;

      public MainViewModel(IDialogService dialog, INavigationService nav, IGeolocation geo)
      {
         _diaService = dialog;
         _navService = nav;
         _geoService = geo;

         PropertyChanged += (sender, e) =>
         {
            if (e.PropertyName == "Location")
            {
               var posn = _geoService.GetLocationData();
               Longitude = posn.Longitude;
               Latitude = posn.Latitude;
               Altitude = posn.Altitude;
               Speed = posn.Speed;
               Heading = posn.Heading;
            }
         };
      }

      public RelayCommand BtnStart
      {
         get
         {
            return _btnStart ??
                   (
                      _btnStart = new RelayCommand(() =>
                      {
                         if (!_geoService.IsListening())
                         {
                            _geoService.StartListening();
                         }
                      })
                   );
         }
      }

      public double Longitude
      {
         get => _longitude;
         set { Set(() => Longitude, ref _longitude, value, true); }
      }

      public double Latitude
      {
         get => _latitude;
         set { Set(() => Latitude, ref _latitude, value, true); }
      }

      public double Altitude
      {
         get => _altitude;
         set { Set(() => Altitude, ref _altitude, value, true); }
      }

      public double Speed
      {
         get => _speed;
         set { Set(() => Speed, ref _speed, value, true); }
      }

      public double Heading
      {
         get => _heading;
         set { Set(() => Heading, ref _heading, value, true); }
      }
   }
}