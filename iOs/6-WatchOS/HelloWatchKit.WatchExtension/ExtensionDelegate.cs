using System.Diagnostics;
using Foundation;
using WatchKit;

namespace HelloWatchKit.WatchExtension
{
   [Register(nameof(ExtensionDelegate))]
   public class ExtensionDelegate : WKExtensionDelegate
   {
      public override void ApplicationDidFinishLaunching() => DisplayInfo(nameof(ApplicationDidFinishLaunching));

      public override void ApplicationDidBecomeActive() => DisplayInfo(nameof(ApplicationDidBecomeActive));

      public override void ApplicationWillResignActive() => DisplayInfo(nameof(ApplicationWillResignActive));

      public override void ApplicationWillEnterForeground()
      {
         if (IsEventSupported())
         {
            DisplayInfo(nameof(ApplicationWillEnterForeground));
         }
      }

      public override void ApplicationDidEnterBackground()
      {
         if (IsEventSupported())
         {
            DisplayInfo(nameof(ApplicationDidEnterBackground));
         }
      }

      private static void DisplayInfo(string eventName) => Debug.WriteLine($"App event: {eventName}");

      private static bool IsEventSupported() => WKInterfaceDevice.CurrentDevice.CheckSystemVersion(3, 0);
   }
}