using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Messages.Models;

namespace Messages.ViewModel
{
   public class MainViewModel : ViewModelBase
   {
      private RelayCommand _btnClick;
      private readonly INavigationService _navService;

      public MainViewModel(INavigationService nav) => _navService = nav;

      public RelayCommand BtnClick
      {
         get
         {
            return _btnClick ??
                   (_btnClick = new RelayCommand(
                      () =>
                      {
                         // create the instance of the model
                         var dataModel = new MessageData("This is line 1", "Line 2 contains this", "MVVM Light",
                            "Xamarin rocks!");

                         // Send the message. We are going to specify a key. We don't need it for this code
                         // but if we had more viewmodels and don't want them all to listen for this broadcast,
                         // the key will tell those viewmodels who aren't listening for this key to ignore it.
                         Messenger.Default.Send(new NotificationMessage<MessageData>(dataModel, "SelectData"));

                         // go to the next viewmodel
                         _navService.NavigateTo(ViewModelLocator.SecondViewKey);
                      }));
         }
      }
   }
}