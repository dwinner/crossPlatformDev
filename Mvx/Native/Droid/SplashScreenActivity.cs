using Android.App;
using Android.Content.PM;
using MvvmCross.Platforms.Android.Core;
using MvvmCross.Platforms.Android.Views;
using MvvmCrossDemo.Core;

namespace MvvmCrossDemo.Droid
{
   [Activity(
      Label = "Mvx-demo",
      MainLauncher = true,
      NoHistory = true,
      ScreenOrientation = ScreenOrientation.Portrait)]
   public class SplashScreenActivity : MvxSplashScreenActivity<MvxAndroidSetup<App>, App>
   {
      public SplashScreenActivity()
         : base(Resource.Layout.SplashScreen)
      {
      }
   }
}