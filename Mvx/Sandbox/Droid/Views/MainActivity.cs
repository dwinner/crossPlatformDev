using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using MvvmCross.Forms.Platforms.Android.Views;
using MvvxSandboxApp.Core.ViewModels.Main;

namespace MvvxSandboxApp.Droid.Views
{
   [Activity(Label = "@string/app_name",
      //Icon = "@drawable/icon",
      ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
      LaunchMode = LaunchMode.SingleTask,
      Theme = "@style/AppTheme")]
   public class MainActivity : MvxFormsAppCompatActivity<MainViewModel>
   {
      protected override void OnCreate(Bundle bundle)
      {
         TabLayoutResource = Resource.Layout.Tabbar;
         ToolbarResource = Resource.Layout.Toolbar;

         base.OnCreate(bundle);

         UserDialogs.Init(this);
      }
   }
}