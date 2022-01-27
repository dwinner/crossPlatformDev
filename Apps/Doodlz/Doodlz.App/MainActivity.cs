using Android.App;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
// ReSharper disable BitwiseOperatorOnEnumWithoutFlags

namespace Doodlz.App
{
   /// <summary>
   ///    Подготовка макета MainActivity
   /// </summary>
   [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
   public class MainActivity : AppCompatActivity
   {
      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);
         SetContentView(Resource.Layout.activity_main);

         var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
         SetSupportActionBar(toolbar);
         
         var screenSize = Resources.Configuration.ScreenLayout & ScreenLayout.SizeMask;
         RequestedOrientation = screenSize == ScreenLayout.SizeXlarge
            ? ScreenOrientation.Landscape
            : ScreenOrientation.Portrait;
      }
   }
}