using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using ResLayout = AppDevUnited.CannonGame.App.Resource.Layout;

namespace AppDevUnited.CannonGame.App
{
   [Activity(
      Label = "@string/app_name",
      Theme = "@style/AppTheme.NoActionBar",
      MainLauncher = true,
      ScreenOrientation = ScreenOrientation.Landscape,
      WindowSoftInputMode = SoftInput.StateAlwaysVisible)]
   public class MainActivity : AppCompatActivity
   {
      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);
         SetContentView(ResLayout.ActivityMain);
      }
   }
}