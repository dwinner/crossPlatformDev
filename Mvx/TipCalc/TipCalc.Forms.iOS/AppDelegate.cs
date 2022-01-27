using Foundation;
using MvvmCross.Forms.Platforms.Ios.Core;
using TipCalc.Core;
using TipCalc.Forms.UI;
using UIKit;

namespace TipCalc.Forms.iOS
{
   [Register(nameof(AppDelegate))]
   public class AppDelegate : MvxFormsApplicationDelegate<MvxFormsIosSetup<App, FormsApp>, App, FormsApp>
   {
      public override bool FinishedLaunching(UIApplication uiApplication, NSDictionary launchOptions) =>
         base.FinishedLaunching(uiApplication, launchOptions);
   }
}