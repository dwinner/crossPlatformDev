using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using InfiniteScrollDemo.Models;
using InfiniteScrollDemo.Services;
using Xamarin.Forms;

namespace InfiniteScrollDemo.ViewModels
{
   public class BaseViewModel : INotifyPropertyChanged
   {
      private bool _isBusy;
      private string _title = string.Empty;
      protected static IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

      public bool IsBusy
      {
         get => _isBusy;
         set => SetProperty(ref _isBusy, value);
      }

      public string Title
      {
         get => _title;
         set => SetProperty(ref _title, value);
      }

      protected bool SetProperty<T>(ref T backingStore, T value,
         [CallerMemberName] string propertyName = "",
         Action onChanged = null)
      {
         if (EqualityComparer<T>.Default.Equals(backingStore, value))
         {
            return false;
         }

         backingStore = value;
         onChanged?.Invoke();
         OnPropertyChanged(propertyName);

         return true;
      }

      #region INotifyPropertyChanged

      public event PropertyChangedEventHandler PropertyChanged;

      private void OnPropertyChanged([CallerMemberName] string propertyName = "")
      {
         var changed = PropertyChanged;
         changed?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }

      #endregion
   }
}