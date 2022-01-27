using Foundation;
using UIKit;

namespace Gallery.iOS
{
   /// <summary>
   ///    App delegate.
   /// </summary>
   [Register(nameof(AppDelegate))]
   public class AppDelegate : UIApplicationDelegate
   {
      /// <summary>
      ///    The window.
      /// </summary>
      private UIWindow _window;

      /// <summary>
      ///    Finisheds the launching.
      /// </summary>
      /// <returns><c>true</c>, if launching was finisheded, <c>false</c> otherwise.</returns>
      /// <param name="application">Application.</param>
      /// <param name="launchOptions">Launch options.</param>
      public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
      {
         _window = new UIWindow(UIScreen.MainScreen.Bounds);
         var mainController = new MainController();
         var rootNavigationController = new UINavigationController();
         rootNavigationController.PushViewController(mainController, false);
         _window.RootViewController = rootNavigationController;
         _window.MakeKeyAndVisible();

         return true;
      }
   }
}