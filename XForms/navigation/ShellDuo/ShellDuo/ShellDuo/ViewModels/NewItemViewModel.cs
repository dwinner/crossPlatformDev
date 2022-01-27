using System;
using ShellDuo.Models;
using Xamarin.Forms;

namespace ShellDuo.ViewModels
{
   public class NewItemViewModel : BaseViewModel
   {
      private string _description;
      private string _text;

      public NewItemViewModel()
      {
         SaveCommand = new Command(OnSave, ValidateSave);
         CancelCommand = new Command(OnCancel);
         PropertyChanged += (_, __) => SaveCommand.ChangeCanExecute();
      }

      public string Text
      {
         get => _text;
         set => SetProperty(ref _text, value);
      }

      public string Description
      {
         get => _description;
         set => SetProperty(ref _description, value);
      }

      public Command SaveCommand { get; }

      public Command CancelCommand { get; }

      private bool ValidateSave() =>
         !string.IsNullOrWhiteSpace(_text)
         && !string.IsNullOrWhiteSpace(_description);

      private static async void OnCancel()
      {
         // This will pop the current page off the navigation stack
         await Shell.Current.GoToAsync("..").ConfigureAwait(true);
      }

      private async void OnSave()
      {
         var newItem = new Item
         {
            Id = Guid.NewGuid().ToString(),
            Text = Text,
            Description = Description
         };

         await DataStore.AddItemAsync(newItem).ConfigureAwait(true);

         // This will pop the current page off the navigation stack
         await Shell.Current.GoToAsync("..").ConfigureAwait(true);
      }

      protected override void UpdateLayouts()
      {
         throw new NotImplementedException();
      }
   }
}