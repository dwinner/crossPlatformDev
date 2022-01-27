using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Xamarin.Auth;
using Xamarin.Auth.Presenters.XamarinAndroid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Platform = Xamarin.Essentials.Platform;

namespace AuthDemoXForms.Droid
{
   [Activity(Label = "AuthDemoXForms", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true,
      ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
   public class MainActivity : FormsAppCompatActivity
   {
      protected override void OnCreate(Bundle savedInstanceState)
      {
         TabLayoutResource = Resource.Layout.Tabbar;
         ToolbarResource = Resource.Layout.Toolbar;

         base.OnCreate(savedInstanceState);

         Forms.SetFlags("CollectionView_Experimental");
         AuthenticationConfiguration.Init(this, savedInstanceState);
         CustomTabsConfiguration.CustomTabsClosingMessage = null;
         CustomTabsConfiguration.IsActionButtonUsed = false;
         CustomTabsConfiguration.IsActionBarToolbarIconUsed = false;
         CustomTabsConfiguration.IsCloseButtonIconUsed = false;
         CustomTabsConfiguration.IsShowTitleUsed = false;
         CustomTabsConfiguration.IsDefaultShareMenuItemUsed = false;
         CustomTabsConfiguration.IsPrefetchUsed = false;
         CustomTabsConfiguration.IsUrlBarHidingUsed = false;
         CustomTabsConfiguration.IsWarmUpUsed = false;
         CustomTabsConfiguration.MenuItemTitle = null;

         Platform.Init(this, savedInstanceState);
         Forms.Init(this, savedInstanceState);
         LoadApplication(new App());
      }

      public override void OnRequestPermissionsResult(int requestCode, string[] permissions,
         [GeneratedEnum] Permission[] grantResults)
      {
         Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

         base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
      }
   }
}