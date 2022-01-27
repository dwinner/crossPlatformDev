using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;

// ReSharper disable AsyncVoidFunctionExpression
// ReSharper disable RedundantCapturedContext
// ReSharper disable AsyncConverter.AsyncAwaitMayBeElidedHighlighting

namespace Alert2.ViewModel
{
   public class MainViewModel : ViewModelBase
   {
      private RelayCommand _btnAlert;
      private string _buttonText;

      public string ButtonText
      {
         get => _buttonText;
         set
         {
            if (!string.IsNullOrEmpty(value))
            {
               Set(() => ButtonText, ref _buttonText, value);
            }
         }
      }

      public RelayCommand BtnAlert
      {
         get
         {
            return _btnAlert ??
                   (_btnAlert = new RelayCommand(
                      async () =>
                      {
                         var dialog = ServiceLocator.Current.GetInstance<IDialogService>();
                         await dialog.ShowError("A message from the VC", "Hello world!", "OK",
                            () => { ButtonText = "You clicked me"; }).ConfigureAwait(true);
                      }));
         }
      }
   }
}