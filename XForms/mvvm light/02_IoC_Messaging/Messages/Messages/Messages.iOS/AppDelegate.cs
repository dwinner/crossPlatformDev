using Foundation;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using UIKit;

namespace Messages.iOS
{
   // The UIApplicationDelegate for the application. This class is responsible for launching the
   // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
   [Register(nameof(AppDelegate))]
   public class AppDelegate : UIApplicationDelegate
   {
      private static ViewModelLocator _locator;
      // class-level declarations

      public override UIWindow Window { get; set; }

      public static ViewModelLocator Locator => _locator ?? (_locator = new ViewModelLocator());

      public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
      {
         // Override point for customization after application launch.
         // If not required for your application you can safely delete this method

         // create the instance of the navigation service
         var navigationService = new NavigationService();

         navigationService.Configure(ViewModelLocator.MainViewKey, "MainPage");
         navigationService.Configure(ViewModelLocator.SecondViewKey, "SecondPage");

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