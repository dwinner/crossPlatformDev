using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace Movement.ViewModel
{
   /// <summary>
   ///    This class contains properties that the main View can data bind to.
   ///    <para>
   ///       Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
   ///    </para>
   ///    <para>
   ///       You can also use Blend to data bind with the tool's support.
   ///    </para>
   ///    <para>
   ///       See http://www.galasoft.ch/mvvm
   ///    </para>
   /// </summary>
   public class MainViewModel : ViewModelBase
   {
      private RelayCommand _forwardButtonCommand;
      private readonly INavigationService _navigationService;

      public MainViewModel(INavigationService navigationService) => _navigationService = navigationService;

      public RelayCommand ForwardButton
      {
         get
         {
            return _forwardButtonCommand ??
                   (_forwardButtonCommand = new RelayCommand(
                      () => { _navigationService.NavigateTo(ViewModelLocator.BackViewKey); }));
         }
      }
   }
}