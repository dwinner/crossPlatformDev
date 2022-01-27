using Android.App;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace AudioPlayerApp.Droid.Views
{
   [Activity(
      NoHistory = true,
      MainLauncher = true,
      Label = "@string/app_name",
      Theme = "@style/AppTheme.Splash",
      Icon = "@mipmap/ic_launcher",
      RoundIcon = "@mipmap/ic_launcher_round")]
   public class SplashActivity : MvxSplashScreenAppCompatActivity
   {
      public SplashActivity()
         : base(Resource.Layout.splash_screen)
      {
      }
   }
}