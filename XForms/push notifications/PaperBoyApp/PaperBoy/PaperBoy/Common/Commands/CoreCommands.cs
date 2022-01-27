using System;
using System.Windows.Input;
using PaperBoy.Extensions;
using PaperBoy.Helpers;
using PaperBoy.Models.News;
using PaperBoy.Pages;

namespace PaperBoy.Common.Commands
{
   public class NavigateToSettingsCommand : ICommand
   {
      public event EventHandler CanExecuteChanged;

      public bool CanExecute(object parameter) => true;

      public void Execute(object parameter)
      {
         NavigateAsync();
      }

      public void RaiseCamExecuteChanged()
      {
         CanExecuteChanged?.Invoke(this, EventArgs.Empty);
      }

      private async void NavigateAsync()
      {
         await App.MainNavigation.PushAsync(new SettingPage(), true);
      }
   }

   public class RefreshNewsCommand : ICommand
   {
      private bool _isBusy;

      public event EventHandler CanExecuteChanged;

      public bool CanExecute(object parameter) => !_isBusy;

      public void Execute(object parameter)
      {
         RefreshNewsAsync((string) parameter);
      }

      public void RaiseCanChange()
      {
         CanExecuteChanged?.Invoke(this, EventArgs.Empty);
      }

      public async void RefreshNewsAsync(string newsType)
      {
         _isBusy = true;
         RaiseCanChange();
         App.ViewModel.IsBusy = true;

         switch (newsType)
         {
            case "Search":
               await App.ViewModel.RefreshSearchResultAsync();
               break;
            case "Trending":
               await App.ViewModel.RefreshTrendingNewsAsync();
               break;
            case "Technology":
               await App.ViewModel.RefreshTechnologyNewsAsync();
               break;
            case "Favorites":
               await App.ViewModel.RefreshFavoritesAsync();
               break;
         }

         _isBusy = false;
         RaiseCanChange();
         App.ViewModel.IsBusy = false;
      }
   }

   public class NavigateToDetailCommand : ICommand
   {
      public event EventHandler CanExecuteChanged;

      public bool CanExecute(object parameter) => true;

      public void Execute(object parameter)
      {
         NavigateToDetailAsync(parameter as NewsInformation);
      }

      public void RaiseCanExecuteChanged()
      {
         CanExecuteChanged?.Invoke(this, EventArgs.Empty);
      }

      private async void NavigateToDetailAsync(NewsInformation article)
      {
         await App.MainNavigation.PushAsync(new ItemDetailPage(article), true);
      }
   }

   public class SpeakCommand : ICommand
   {
      public event EventHandler CanExecuteChanged;

      public bool CanExecute(object parameter) => true;

      public void Execute(object parameter)
      {
         GeneralHelper.Speak((string) parameter);
      }

      public void RaiseCanExecuteChanged()
      {
         CanExecuteChanged?.Invoke(this, EventArgs.Empty);
      }
   }

   public class ToggleFavoriteCommand : ICommand
   {
      private bool _isBusy;
      public event EventHandler CanExecuteChanged;

      public bool CanExecute(object parameter) => !_isBusy;

      public void Execute(object parameter)
      {
         ToggleFavoriteAsync(parameter as NewsInformation);
      }

      public void RaiseCanExecuteChanged()
      {
         CanExecuteChanged?.Invoke(this, EventArgs.Empty);
      }

      public async void ToggleFavoriteAsync(NewsInformation articel)
      {
         _isBusy = true;
         RaiseCanExecuteChanged();
         App.ViewModel.IsBusy = true;

         await App.ViewModel.Favorites.AddAsync(await articel.AsFavorite("Technology"));

         _isBusy = false;
         RaiseCanExecuteChanged();
         App.ViewModel.IsBusy = false;
      }
   }
}