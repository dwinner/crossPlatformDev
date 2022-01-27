using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ContactBookApp.Annotations;

namespace ContactBookApp.Models
{
   public class ViewModelBase : INotifyPropertyChanged
   {
      public event PropertyChangedEventHandler PropertyChanged;

      [NotifyPropertyChangedInvocator]
      protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

      protected void SetProperty<T>(ref T referenceProperty, T newProperty,
         [CallerMemberName] string propertyName = null)
      {
         if (!EqualityComparer<T>.Default.Equals(newProperty, referenceProperty))
         {
            referenceProperty = newProperty;
         }

         OnPropertyChanged(propertyName);
      }
   }
}