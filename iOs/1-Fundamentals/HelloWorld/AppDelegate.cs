using System.Diagnostics;
using Foundation;
using UIKit;

namespace HelloWorld
{
   [Register(nameof(AppDelegate))]
   public class AppDelegate : UIApplicationDelegate
   {
      public override UIWindow Window { get; set; }

      public override bool WillFinishLaunching(UIApplication application, NSDictionary launchOptions)
      {
         DisplayInfo(nameof(WillFinishLaunching));
         return true;
      }

      public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
      {
         DisplayInfo(nameof(FinishedLaunching));
         return true;
      }

      public override void OnResignActivation(UIApplication application) => DisplayInfo(nameof(OnResignActivation));

      public override void DidEnterBackground(UIApplication application) => DisplayInfo(nameof(DidEnterBackground));

      public override void WillEnterForeground(UIApplication application) => DisplayInfo(nameof(WillEnterForeground));

      public override void OnActivated(UIApplication application) => DisplayInfo(nameof(OnActivated));

      public override void WillTerminate(UIApplication application) => DisplayInfo(nameof(WillTerminate));

      private static void DisplayInfo(string eventName)
      {
         Debug.WriteLine($"App event: {eventName}. " +
                         "App state: {application.ApplicationState}");
      }
   }
}