using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ShellDuo.Models
{
   public class BaseModel : INotifyPropertyChanged
   {
      #region INotifyPropertyChanged

      protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "",
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

      public event PropertyChangedEventHandler PropertyChanged;

      private void OnPropertyChanged([CallerMemberName] string propertyName = "")
      {
         var changed = PropertyChanged;
         changed?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }

      #endregion
   }
}