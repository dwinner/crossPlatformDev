using Android.App;
using Android.OS;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Views;
using Messages.ViewModel;

namespace Messages.Droid.Activities
{
   [Activity(Label = "Messages.Droid", MainLauncher = true, Icon = "@drawable/icon")]
   public partial class MainActivity : ActivityBase
   {
      public MainViewModel ViewModel => App.Locator.Main;

      protected override void OnCreate(Bundle bundle)
      {
         base.OnCreate(bundle);

         SetContentView(Resource.Layout.Main);
         BtnClickMe.SetCommand(nameof(Button.Click), ViewModel.BtnClick);
      }
   }
}