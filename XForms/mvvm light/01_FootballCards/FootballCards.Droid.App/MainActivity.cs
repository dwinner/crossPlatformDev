using Android.App;
using Android.OS;
using FootballCards.Droid.App.Model;
using FootballCards.SharedLib.ViewModel;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Views;

namespace FootballCards.Droid.App
{
   [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
   public partial class MainActivity : ActivityBase
   {
      public MainViewModel ViewModel => App.Locator.Main;

      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);

         SetContentView(Resource.Layout.Main);
         CreateTextViewBindings();
         CreateEditTextBinding();
         CreateButtonBinding();
      }

      private void CreateTextViewBindings()
      {
         this.SetBinding(
            () => ViewModel.TeamName,
            () => TeamNameTextView.Text,
            BindingMode.TwoWay);

         this.SetBinding(
            () => ViewModel.StadiumName,
            () => StadiumTextView.Text,
            BindingMode.TwoWay);

         this.SetBinding(
            () => ViewModel.Capacity,
            () => CapacityTextView.Text,
            BindingMode.TwoWay);
      }

      private void CreateEditTextBinding()
      {
         this.SetBinding(
            () => ViewModel.NumberShuffles,
            () => ShufflesEditText.Text,
            BindingMode.TwoWay);
      }

      private void CreateButtonBinding()
      {
         ShuffleButton.SetCommand(Events.Click, ViewModel.ButtonClicked);
         ShowMapButton.SetCommand(Events.Click, ViewModel.ButtonShowMap);
      }
   }
}