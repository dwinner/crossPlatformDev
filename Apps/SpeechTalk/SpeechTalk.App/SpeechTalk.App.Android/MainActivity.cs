using Android.App;
using Android.Content.PM;
using Android.OS;
using SpeechTalk.App.Droid.Modules;
using SpeechTalk.App.IoC;
using SpeechTalk.App.Modules;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace SpeechTalk.App.Droid
{
   [Activity(
      Label = "SpeechTalk.App",
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

         Forms.Init(this, savedInstanceState);
         InitIoc();
         LoadApplication(new App());
      }

      private static void InitIoc()
      {
         IoCConfuguration.CreateContainer();
         IoCConfuguration.RegisterModule(new DroidModule());
         IoCConfuguration.RegisterModule(new PclModule());
         IoCConfuguration.StartContainer();
      }
   }
}