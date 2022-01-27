using Foundation;
using ImageCircle.Forms.Plugin.iOS;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using PaperBoy.Data;
using PaperBoy.iOS.Helpers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms.Themes;
using Xamarin.Forms.Themes.iOS;

namespace PaperBoy.iOS
{
   // The UIApplicationDelegate for the application. This class is responsible for launching the 
   // User Interface of the application, as well as listening (and optionally responding) to 
   // application events from iOS.
   [Register("AppDelegate")]
   public class AppDelegate : FormsApplicationDelegate
   {
      //
      // This method is invoked when the application has loaded and is ready to run. In this 
      // method you should instantiate the window, load the UI into it and then make the window
      // visible.
      //
      // You have 17 seconds to return from this method, or iOS will terminate your application.
      //
      public override bool FinishedLaunching(UIApplication app, NSDictionary options)
      {
         Forms.Init();

         var accentColor = UIColor.FromRGB(0, 89, 178);

         UISlider.Appearance.TintColor = accentColor;
         UISlider.Appearance.ThumbTintColor = accentColor;

         UITabBar.Appearance.TintColor = accentColor;
         UITabBar.Appearance.SelectedImageTintColor = accentColor;

         UINavigationBar.Appearance.BarTintColor = accentColor;
         UINavigationBar.Appearance.TintColor = accentColor;
         UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes {TextColor = UIColor.White});

         ImageCircleRenderer.Init();

         RegisterForPushNotifications();
         LoadApplication(new App());
         var x = typeof(DarkThemeResources);
         x = typeof(LightThemeResources);
         x = typeof(UnderlineEffect);
         return base.FinishedLaunching(app, options);
      }

      private void RegisterForPushNotifications()
      {
         var settings = UIUserNotificationSettings.GetSettingsForTypes(
            UIUserNotificationType.Alert
            | UIUserNotificationType.Badge
            | UIUserNotificationType.Sound,
            new NSSet());

         UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
         UIApplication.SharedApplication.RegisterForRemoteNotifications();
      }

      public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
      {
         const string templateBodyApns = "{\"aps\":{\"alert\":\"$(messageParam)\"}}";

         var templates = new JObject();
         templates["genericMessage"] = new JObject
         {
            {"body", templateBodyApns}
         };

         var push = FavoriteManager.DefaultManager.CurrentClient.GetPush();

#pragma warning disable CS1701 // Assuming assembly reference matches identity
         push.RegisterAsync(deviceToken, templates);
#pragma warning restore CS1701 // Assuming assembly reference matches identity
      }

      public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
      {
      }

      public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
      {
         ToastHelper.ProcessNotification(userInfo);
      }
   }
}