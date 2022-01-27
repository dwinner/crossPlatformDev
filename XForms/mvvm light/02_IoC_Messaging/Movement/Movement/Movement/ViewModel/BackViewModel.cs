using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace Movement.ViewModel
{
   public class BackViewModel
   {
      private readonly INavigationService _navigationService;
      private RelayCommand _backButtonCommand;

      public BackViewModel(INavigationService navigationService) => _navigationService = navigationService;

      public RelayCommand BackButton
      {
         get
         {
            return _backButtonCommand ??
                   (_backButtonCommand = new RelayCommand(
                      () => { _navigationService.GoBack(); }));
         }
      }
   }
}