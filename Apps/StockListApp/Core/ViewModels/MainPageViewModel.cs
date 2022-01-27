using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using StockList.Core.Enums;
using StockList.Core.Extras;
using StockList.Core.Models;
using StockList.Core.UI;

namespace StockList.Core.ViewModels
{
   public class MainPageViewModel : ViewModelBase
   {
      private ICommand _exitCommand;
      private bool _inProgress;
      private ICommand _stocklistCommand;

      public MainPageViewModel(INavigationService navigation, Func<Action, ICommand> commandFactory, IMethods methods)
         : base(navigation)
      {
         _exitCommand = commandFactory(methods.Exit);
         _stocklistCommand = commandFactory(async () =>
            await Navigation.NavigateAsync(PageNames.StocklistPage, null).ConfigureAwait(true));
      }

      public bool InProgress
      {
         get => _inProgress;
         set
         {
            if (value.Equals(_inProgress))
            {
               return;
            }

            _inProgress = value;
            OnPropertyChanged();
         }
      }

      public ICommand StocklistCommand
      {
         get => _stocklistCommand;
         set
         {
            if (value.Equals(_stocklistCommand))
            {
               return;
            }

            _stocklistCommand = value;
            OnPropertyChanged();
         }
      }

      public ICommand ExitCommand
      {
         get => _exitCommand;
         set
         {
            if (value.Equals(_exitCommand))
            {
               return;
            }

            _exitCommand = value;
            OnPropertyChanged();
         }
      }

      protected override Task LoadAsync(IDictionary<string, object> parameters) => Task.FromResult(true);
   }
}