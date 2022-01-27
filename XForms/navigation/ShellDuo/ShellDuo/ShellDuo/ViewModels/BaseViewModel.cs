using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ShellDuo.Models;
using ShellDuo.Services;
using Xamarin.Forms;
using Xamarin.Forms.DualScreen;

namespace ShellDuo.ViewModels
{
   public class BaseViewModel : INotifyPropertyChanged
   {
      private bool _isBusy;
      private string _title = string.Empty;
      public INavigation Navigation { get; set; }
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

      #region Screens

      protected static bool DeviceIsSpanned => DualScreenInfo.Current.SpanMode != TwoPaneViewMode.SinglePane;

      protected static bool DeviceIsBigScreen =>
         Device.Idiom == TargetIdiom.Tablet || Device.Idiom == TargetIdiom.Desktop ||
         Device.Idiom == TargetIdiom.TV;

      private bool _wasSpanned;

      protected bool IsDetail;

      private TwoPaneViewTallModeConfiguration _tallModeConfiguration;

      public TwoPaneViewTallModeConfiguration TallModeConfiguration
      {
         get => _tallModeConfiguration;
         set => SetProperty(ref _tallModeConfiguration, value);
      }

      private TwoPaneViewWideModeConfiguration _wideModeConfiguration;

      public TwoPaneViewWideModeConfiguration WideModeConfiguration
      {
         get => _wideModeConfiguration;
         set => SetProperty(ref _wideModeConfiguration, value);
      }

      private TwoPaneViewPriority _panePriority;

      public TwoPaneViewPriority PanePriority
      {
         get => _panePriority;
         set => SetProperty(ref _panePriority, value);
      }

      protected virtual void UpdateLayouts() => UpdateLayouts(false, null);

      protected async void UpdateLayouts(bool itemSelected, string route)
      {
         if (IsDetail && DeviceIsSpanned)
         {
            if (Navigation.NavigationStack.Count > 1)
            {
               await Navigation.PopToRootAsync().ConfigureAwait(true);
            }
         }
         else if (DeviceIsSpanned || DeviceIsBigScreen)
         {
            TallModeConfiguration = TwoPaneViewTallModeConfiguration.TopBottom;
            WideModeConfiguration = TwoPaneViewWideModeConfiguration.LeftRight;
            _wasSpanned = true;
         }
         else
         {
            PanePriority = TwoPaneViewPriority.Pane1;
            TallModeConfiguration = TwoPaneViewTallModeConfiguration.SinglePane;
            WideModeConfiguration = TwoPaneViewWideModeConfiguration.SinglePane;

            if (_wasSpanned && itemSelected)
            {
               await Shell.Current.GoToAsync(route).ConfigureAwait(true);
            }

            _wasSpanned = false;
         }
      }

      private void DualScreen_PropertyChanged(object sender, PropertyChangedEventArgs e) => UpdateLayouts();

      protected void OnAppearing(bool isDetail = false)
      {
         IsBusy = true;
         IsDetail = isDetail;
         DualScreenInfo.Current.PropertyChanged += DualScreen_PropertyChanged;
         UpdateLayouts();
      }

      public void OnDisappearing() => DualScreenInfo.Current.PropertyChanged -= DualScreen_PropertyChanged;

      #endregion Screens
   }
}