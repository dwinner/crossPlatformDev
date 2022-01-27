using Foundation;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using UIKit;

namespace Movement.iOS
{
   [Register(nameof(AppDelegate))]
   public class AppDelegate : UIApplicationDelegate
   {
      private static ViewModelLocator _locator;

      public override UIWindow Window { get; set; }

      public static ViewModelLocator Locator => _locator ?? (_locator = new ViewModelLocator());

      public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
      {
         var navigationService = new NavigationService();

         navigationService.Configure(ViewModelLocator.MainViewKey, "Main");
         navigationService.Configure(ViewModelLocator.BackViewKey, "Back");

         // iOS uses the UINavigtionController to move between pages, so will we
         navigationService.Initialize(Window.RootViewController as UINavigationController);

         // finally register the service with SimpleIoc
         SimpleIoc.Default.Register<INavigationService>(() => navigationService);

         // Override point for customization after application launch.
         // If not required for your application you can safely delete this method
         return true;
      }
   }
}