using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using StockList.Core.UI;

namespace StockList.Core.Models
{
   public abstract class ViewModelBase : INotifyPropertyChanged
   {
      protected ViewModelBase(INavigationService navigation) => Navigation = navigation;

      public INavigationService Navigation { get; set; }

      public event PropertyChangedEventHandler PropertyChanged;

      [NotifyPropertyChangedInvocator]
      protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

      public void OnShow(IDictionary<string, object> parameters)
      {
         LoadAsync(parameters).ToObservable().Subscribe(
            result =>
            { /* we can add things to do after we load the view model */
            },
            error =>
            { /* we can handle any areas from the load async function */
            }
         );
      }

      protected abstract Task LoadAsync(IDictionary<string, object> parameters);
   }
}