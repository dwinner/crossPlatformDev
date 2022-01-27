using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Gcm.Client;
using ImageCircle.Forms.Plugin.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Themes;
using Xamarin.Forms.Themes.Android;

namespace PaperBoy.Droid
{
   [Activity(Label = "PaperBoy",
      Icon = "@drawable/icon",
      Theme = "@style/MainTheme",
      MainLauncher = false,
      ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
   public class MainActivity : FormsAppCompatActivity
   {
      public static MainActivity CurrentActivity { get; private set; }

      protected override void OnCreate(Bundle bundle)
      {
         CurrentActivity = this;

         TabLayoutResource = Resource.Layout.Tabbar;
         ToolbarResource = Resource.Layout.Toolbar;

         base.OnCreate(bundle);

         Forms.Init(this, bundle);
         ImageCircleRenderer.Init();

         RegisterForNotifications();

         LoadApplication(new App());
      }

      private void RegisterForNotifications()
      {
         try
         {
            GcmClient.CheckDevice(this);
            GcmClient.CheckManifest(this);
            GcmClient.Register(this, PushHandlerBroadcastReceiver.SenderIds);
         }
         catch (Exception e)
         {
            Console.WriteLine(e);
            throw;
         }
      }
   }
}