using Foundation;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using UIKit;

namespace connectivity.iOS
{
   [Register(nameof(AppDelegate))]
   public class AppDelegate : UIApplicationDelegate
   {
      private static ViewModelLocator _locator;

      public static AppDelegate SelfDelegate { get; private set; }

      public static ViewModelLocator Locator => _locator ?? (_locator = new ViewModelLocator());

      public override UIWindow Window { get; set; }

      public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
      {
         SelfDelegate = this;

         var navigationService = new NavigationService();

         navigationService.Configure(ViewModelLocator.MainKey, "MainViewController");

         navigationService.Initialize(Window.RootViewController as UINavigationController);

         SimpleIoc.Default.Register<INavigationService>(() => navigationService);
         SimpleIoc.Default.Register<IDialogService, DialogService>();

         return true;
      }
   }
}