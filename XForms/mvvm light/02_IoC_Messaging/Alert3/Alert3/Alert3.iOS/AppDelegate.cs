using Foundation;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using UIKit;

namespace Alert3.iOS
{
   [Register(nameof(AppDelegate))]
   public class AppDelegate : UIApplicationDelegate
   {
      private static ViewModelLocator _locator;

      public override UIWindow Window { get; set; }

      public static ViewModelLocator Locator => _locator ?? (_locator = new ViewModelLocator());

      public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
      {
         SimpleIoc.Default.Register<IDialogService, DialogService>();
         return true;
      }
   }
}