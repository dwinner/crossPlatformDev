using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using FileStorageApp.Droid.Modules;
using FileStorageApp.IoC;
using FileStorageApp.Modules;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Platform = Xamarin.Essentials.Platform;

namespace FileStorageApp.Droid
{
   [Activity(
      Label = "FileStorage",
      Icon = "@drawable/icon",
      Theme = "@style/MainTheme",
      MainLauncher = true,
      ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
   public class MainActivity : FormsAppCompatActivity
   {
      protected override void OnCreate(Bundle savedInstanceState)
      {
         TabLayoutResource = Resource.Layout.Tabbar;
         ToolbarResource = Resource.Layout.Toolbar;

         base.OnCreate(savedInstanceState);
         InitIoC();
         Platform.Init(this, savedInstanceState);
         Forms.Init(this, savedInstanceState);
         LoadApplication(new App());
      }

      private void InitIoC()
      {
         IoCConfig.CreateContainer();
         IoCConfig.RegisterModule(new DroidModule());
         IoCConfig.RegisterModule(new XamFormsModule());
         IoCConfig.RegisterModule(new PortableModule());
         IoCConfig.StartContainer();
      }

      public override void OnRequestPermissionsResult(int requestCode, string[] permissions,
         [GeneratedEnum] Permission[] grantResults)
      {
         Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

         base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
      }
   }
}