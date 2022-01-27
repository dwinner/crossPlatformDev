using FootballCards.SharedLib;
using Foundation;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using UIKit;

namespace FootballCards.iOs.App
{
   [Register(nameof(AppDelegate))]
   public class AppDelegate : UIApplicationDelegate
   {
      private static ViewModelLocator _locator;
      public override UIWindow Window { get; set; }

      public static ViewModelLocator Locator => _locator ?? (_locator = new ViewModelLocator());

      public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
      {
         var navigation = new NavigationService();
         navigation.Configure(ViewModelLocator.MainPageKey, "MainPage");
         navigation.Configure(ViewModelLocator.MapPageKey, "MapPage");

         // iOs uses the UINavigationController to move between pages, so will we
         navigation.Initialize(Window.RootViewController as UINavigationController);

         // finally register the service with SimpleIoc
         SimpleIoc.Default.Register<INavigationService>(() => navigation);

         return true;
      }
   }
}