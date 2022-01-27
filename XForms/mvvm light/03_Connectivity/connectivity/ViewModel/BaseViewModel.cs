using GalaSoft.MvvmLight;

namespace connectivity.ViewModel
{
   public class BaseViewModel : ViewModelBase
   {
      private static bool _isOnline;

      public bool IsOnline
      {
         get => _isOnline;
         set { Set(() => IsOnline, ref _isOnline, value, true); }
      }
   }
}