using Android.App;
using Android.OS;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Views;
using Movement.Droid.Model;
using Movement.ViewModel;

namespace Movement.Droid
{
   [Activity(Label = "Movement.Droid", MainLauncher = true, Icon = "@drawable/icon")]
   public class MainActivity : ActivityBase
   {
      public MainViewModel ViewModel => App.Locator.Main;

      protected override void OnCreate(Bundle bundle)
      {
         base.OnCreate(bundle);

         // Set our view from the "main" layout resource
         SetContentView(Resource.Layout.Main);

         // Get our button from the layout resource, and attach an event to it
         var btnForward = FindViewById<Button>(Resource.Id.btnForward);
         btnForward.SetCommand(
            Events.Click,
            ViewModel.ForwardButton);
      }
   }
}