using Foundation;
using MvvmCross.Platforms.Ios.Core;
using TipCalc.Core;
using UIKit;

namespace TipCalc.iOS
{
   [Register(nameof(AppDelegate))]
   public class AppDelegate : MvxApplicationDelegate<MvxIosSetup<App>, App>
   {
      public override UIWindow Window { get; set; }

      public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
      {
         var result = base.FinishedLaunching(application, launchOptions);

         return result;
      }
   }
}