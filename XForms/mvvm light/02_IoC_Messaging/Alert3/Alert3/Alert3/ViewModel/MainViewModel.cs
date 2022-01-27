using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;

// ReSharper disable AsyncVoidFunctionExpression

namespace Alert3.ViewModel
{
   public class MainViewModel : ViewModelBase
   {
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
                         var result = await dialog.ShowMessage("Do you want to take the blue pill or red pill?",
                            "Reality time",
                            "Red pill",
                            "Blue pill",
                            async boResponse =>
                            {
                               await dialog.ShowMessage($"You picked the {(boResponse ? "red" : "blue")} pill", "Yummy")
                                  .ConfigureAwait(true);
                            }).ConfigureAwait(true);

                         if (result)
                         {
                            await dialog.ShowMessage("Welcome Nero...", "Message from the Matrix")
                               .ConfigureAwait(false);
                         }
                      }));
         }
      }
   }
}