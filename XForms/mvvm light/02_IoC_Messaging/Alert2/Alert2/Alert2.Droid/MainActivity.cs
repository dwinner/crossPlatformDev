using Alert2.ViewModel;
using Android.App;
using Android.OS;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Views;

namespace Alert2.Droid
{
   [Activity(Label = "Alert2.Droid", MainLauncher = true, Icon = "@drawable/icon")]
   public class MainActivity : ActivityBase
   {
      public MainViewModel ViewModel => App.Locator.Main;

      protected override void OnCreate(Bundle bundle)
      {
         base.OnCreate(bundle);

         // Set our view from the "main" layout resource
         SetContentView(Resource.Layout.Main);

         // Get our button from the layout resource,
         // and attach an event to it
         var button = FindViewById<Button>(Resource.Id.myButton);
         button.SetCommand(nameof(Button.Click), ViewModel.BtnAlert);
         ViewModel.ButtonText = "Click me the alert";

         this.SetBinding
         (
            () => ViewModel.ButtonText,
            () => button.Text,
            BindingMode.TwoWay
         );
      }
   }
}