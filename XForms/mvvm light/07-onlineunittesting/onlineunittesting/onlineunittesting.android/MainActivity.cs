using Android.App;
using Android.OS;
using Android.Widget;
using Layouts = OnlineUnitTesting.Android.Resources.Resource.Layout;
using Ids = OnlineUnitTesting.Android.Resources.Resource.Id;

namespace OnlineUnitTesting.Android
{
   [Activity(Label = "OnlineUnitTesting.Android", MainLauncher = true, Icon = "@drawable/icon")]
   public class MainActivity : Activity
   {
      private int _count = 1;

      protected override void OnCreate(Bundle bundle)
      {
         base.OnCreate(bundle);

         // Set our view from the "main" layout resource
         SetContentView(Layouts.Main);

         // Get our button from the layout resource,
         // and attach an event to it
         var button = FindViewById<Button>(Ids.myButton);

         button.Click += delegate { button.Text = $"{_count++} clicks!"; };
      }
   }
}