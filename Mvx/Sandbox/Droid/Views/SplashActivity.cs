using System.Threading.Tasks;
using Android.App;
using Android.Content.PM;
using Android.OS;
using MvvmCross.Forms.Platforms.Android.Views;
using CoreApp = MvvxSandboxApp.Core.App;
using UIApp = MvvxSandboxApp.UI.App;

namespace MvvxSandboxApp.Droid.Views
{
   [Activity(
      NoHistory = true,
      MainLauncher = true,
      ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
      LaunchMode = LaunchMode.SingleTask,
      Label = "@string/app_name",
      Theme = "@style/AppTheme.Splash")]
   public class SplashActivity : MvxFormsSplashScreenActivity<Setup, CoreApp, UIApp>
   {
      protected override Task RunAppStartAsync(Bundle bundle)
      {
         StartActivity(typeof(MainActivity));
         return Task.CompletedTask;
      }
   }
}