using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace connectivity.ViewModel
{
   public class MainViewModel : BaseViewModel
   {
      private readonly IDialogService _dialogService;
      // ReSharper disable NotAccessedField.Local
      private readonly INavigationService _navigation;
      // ReSharper restore NotAccessedField.Local
      private bool _goWebsite;
      private RelayCommand _websiteCommand;

      public MainViewModel(IDialogService dialog, INavigationService navigation)
      {
         _dialogService = dialog;
         _navigation = navigation;
      }

      public bool GoWebsite
      {
         get => _goWebsite;
         set { Set(() => GoWebsite, ref _goWebsite, value, true); }
      }

      public RelayCommand WebsiteCommand => _websiteCommand ?? (_websiteCommand = new RelayCommand(async () =>
      {
         if (!IsOnline)
         {
            await _dialogService.ShowMessage(
               "You are not connected to a network",
               "Network error",
               "OK",
               () => GoWebsite = false).ConfigureAwait(true);
         }
         else
         {
            GoWebsite = true;
         }

         // the following line resets websiteCommand - if you don't, you'll only be able to fire the command once
         _websiteCommand = null;
      }));
   }
}