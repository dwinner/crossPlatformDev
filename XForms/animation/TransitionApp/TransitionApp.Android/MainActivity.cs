using Android.App;
using Android.Content.PM;
using Android.OS;
using FFImageLoading.Forms.Platform;
using Prism;
using Prism.Ioc;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace TransitionApp.Droid
{
   [Activity(Label = "TransitionApp", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true,
      ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
   public class MainActivity : FormsAppCompatActivity
   {
      protected override void OnCreate(Bundle bundle)
      {
         TabLayoutResource = Resource.Layout.Tabbar;
         ToolbarResource = Resource.Layout.Toolbar;

         base.OnCreate(bundle);

         Forms.SetFlags("CollectionView_Experimental");
         Forms.Init(this, bundle);
         CachedImageRenderer.Init(true);
         LoadApplication(new App(new AndroidInitializer()));
      }
   }

   public class AndroidInitializer : IPlatformInitializer
   {
      public void RegisterTypes(IContainerRegistry containerRegistry)
      {
         // Register any platform specific implementations
      }
   }
}