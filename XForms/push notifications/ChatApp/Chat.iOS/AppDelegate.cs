using Chat.Common.Presenter;
using Chat.iOS.Services;
using Chat.iOS.Views;
using Foundation;
using UIKit;

namespace Chat.iOS
{
   /// <summary>
   ///    App delegate.
   /// </summary>
   [Register(nameof(AppDelegate))]
   public class AppDelegate : UIApplicationDelegate
   {
      /// <summary>
      ///    The navigation controller.
      /// </summary>
      private UINavigationController _navigationController;

      // class-level declarations
      /// <summary>
      ///    The window.
      /// </summary>
      private UIWindow _window;

      /// <summary>
      ///    Finisheds the launching.
      /// </summary>
      /// <returns>The launching.</returns>
      /// <param name="app">App.</param>
      /// <param name="options">Options.</param>
      public override bool FinishedLaunching(UIApplication app, NSDictionary options)
      {
         _window = new UIWindow(UIScreen.MainScreen.Bounds);

         _navigationController = new UINavigationController();

         var state = new ApplicationState();

         var presenter = new LoginPresenter(state, new NavigationService(_navigationController));
         var controller = new LoginViewController(presenter);

         _navigationController.PushViewController(controller, false);
         _window.RootViewController = _navigationController;

         // make the window visible
         _window.MakeKeyAndVisible();

         return true;
      }
   }
}