using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using StockList.Core.Ioc;
using StockList.Core.Modules;
using StockList.Shared;
using StockListApp.Droid.Modules;
using StockListApp.Xam;
using StockListApp.Xam.Modules;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using DroidRes = StockListApp.Droid.Resource;
using Platform = Xamarin.Essentials.Platform;

namespace StockListApp.Droid
{
   [Activity(
      Label = "StockListApp",
      Icon = "@mipmap/icon",
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

      public override void OnRequestPermissionsResult(int requestCode, string[] permissions,
         [GeneratedEnum] Permission[] grantResults)
      {
         Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
         base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
      }

      private void InitIoC()
      {
         IoC.CreateContainer();
         IoC.RegisterModule(new DroidModule());
         IoC.RegisterModule(new SharedModule(false));
         IoC.RegisterModule(new XamFormsModule());
         IoC.RegisterModule(new PortableModule());
         IoC.StartContainer();
      }
   }
}