using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;

// ReSharper disable AsyncVoidFunctionExpression
// ReSharper disable AsyncConverter.AsyncAwaitMayBeElidedHighlighting

namespace Alert.ViewModel
{
   public class MainViewModel : ViewModelBase
   {
      /// <summary>
      ///    Initializes a new instance of the MainViewModel class.
      /// </summary>
      private RelayCommand _btnAlert;

      public RelayCommand BtnAlert
      {
         get
         {
            return _btnAlert ??
                   (_btnAlert = new RelayCommand(
                      async () =>
                      {
                         var dialog = ServiceLocator.Current.GetInstance<IDialogService>();
                         await dialog.ShowError("A message from the VC", "Hello world!", "OK", null)
                            .ConfigureAwait(true);
                      }));
         }
      }
   }
}