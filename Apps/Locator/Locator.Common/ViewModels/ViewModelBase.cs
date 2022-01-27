using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Locator.Common.UI;

// ReSharper disable UnusedParameter.Global
#pragma warning disable 1998

namespace Locator.Common.ViewModels
{
   /// <summary>
   ///    The base class of all view models
   /// </summary>
   public class ViewModelBase : INotifyPropertyChanged
   {
      /// <summary>
      ///    The navigation.
      /// </summary>
      protected readonly INavigationService Navigation;

      /// <summary>
      ///    Initializes a new instance of the <see cref="T:Locator.Common.ViewModels.ViewModelBase" /> class.
      /// </summary>
      /// <param name="navigation">Navigation.</param>
      protected ViewModelBase(INavigationService navigation) => Navigation = navigation;

      /// <summary>
      ///    The property changed.
      /// </summary>
      public event PropertyChangedEventHandler PropertyChanged;

      /// <summary>
      /// </summary>
      /// <param name="parameters">
      /// </param>
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

      /// <summary>
      ///    Raises the property changed event.
      /// </summary>
      /// <param name="propertyName">Property name.</param>
      protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

      /// <summary>
      ///    Loads the async.
      /// </summary>
      /// <returns>The async.</returns>
      /// <param name="parameters">Parameters.</param>
      protected virtual async Task LoadAsync(IDictionary<string, object> parameters)
      {
      }
   }
}