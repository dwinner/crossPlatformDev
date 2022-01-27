using System.Windows.Input;
using Xamarin.Forms;

namespace EventToCommand.ViewModels
{
   public class MainViewModel : BaseViewModel
   {
      public MainViewModel()
      {
         EntryFocusedCommand = new Command(FocusedAlert);
         EntryTextChangedCommand = new Command(TextChangedAlert);
      }

      public ICommand EntryFocusedCommand { get; }
      public ICommand EntryTextChangedCommand { get; }

      private void FocusedAlert()
      {
         MessagingCenter.Send(this, "Focused");
      }

      private void TextChangedAlert()
      {
         MessagingCenter.Send(this, "TextChanged");
      }
   }
}