using FFImageLoading.Forms.Platform;
using Foundation;
using MvvmCross.Forms.Platforms.Ios.Core;
using OxyPlot.Xamarin.Forms.Platform.iOS;
using StarWarsSample.Core;
using UIKit;

namespace StarWarsSample.Forms.iOS
{
   [Register(nameof(AppDelegate))]
   public class AppDelegate : MvxFormsApplicationDelegate<MvxFormsIosSetup<App, UI.App>, App, UI.App>
   {
      public override bool FinishedLaunching(UIApplication app, NSDictionary options)
      {
         CachedImageRenderer.Init();
         PlotViewRenderer.Init();

         return base.FinishedLaunching(app, options);
      }
   }
}