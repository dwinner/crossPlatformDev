using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FileStorageApp.Methods;
using FileStorageApp.UI;

namespace FileStorageApp.ViewModels
{
   /// <summary>
   ///    The base class of all view models
   /// </summary>
   public abstract class ViewModelBase : INotifyPropertyChanged
   {
      private readonly IMethods _methods; // methods
      protected readonly INavigationService Navigation; // navigation

      /// <summary>
      ///    Initializes a new instance of the <see cref="ViewModelBase" /> class
      /// </summary>
      /// <param name="navigation">Navigation</param>
      /// <param name="methods">Methods</param>
      protected ViewModelBase(INavigationService navigation, IMethods methods)
      {
         Navigation = navigation;
         _methods = methods;
      }

      public event PropertyChangedEventHandler PropertyChanged;

      /// <summary>
      ///    Occurs when alert
      /// </summary>
      public event EventHandler<string> Alert;

      /// <summary>
      ///    Loads the async
      /// </summary>
      /// <returns>The async</returns>
      /// <param name="parameters">Parameters</param>
      protected abstract Task LoadAsync(IDictionary<string, object> parameters);

      /// <summary>
      ///    Shows the alert.
      /// </summary>
      /// <returns>The alert.</returns>
      protected Task<string> ShowEntryAlertAsync(string message)
      {
         var puppetTask = new TaskCompletionSource<string>();
         _methods.DisplayEntryAlert(puppetTask, message);
         return puppetTask.Task;
      }

      /// <summary>
      ///    Notifies the alert
      /// </summary>
      /// <returns>The alert</returns>
      /// <param name="message">Message.</param>
      protected void NotifyAlert(string message) => Alert?.Invoke(this, message);

      /// <summary>
      ///    Show after navigation
      /// </summary>
      /// <param name="parameters">Navigation parameters</param>
      public void OnShow(IDictionary<string, object> parameters)
      {
         LoadAsync(parameters).ToObservable().Subscribe(
            result =>
            {
               // we can add things to do after we load the view model
            },
            ex =>
            {
               // we can handle any areas from the load async function
            });
      }

      private void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

      protected void SetProperty<T>(ref T propertyValue, T newValue, [CallerMemberName] string propertyName = null)
      {
         if (!EqualityComparer<T>.Default.Equals(newValue, propertyValue))
         {
            propertyValue = newValue;
         }

         OnPropertyChanged(propertyName);
      }
   }
}