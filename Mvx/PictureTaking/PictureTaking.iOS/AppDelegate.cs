using Foundation;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Platform;
using MvvmCross.iOS.Views.Presenters;
using MvvmCross.Platform;
using UIKit;

namespace PictureTaking.Touch
{
   [Register("AppDelegate")]
   public class AppDelegate : MvxApplicationDelegate
   {
      private UIWindow _window;

      public override bool FinishedLaunching(UIApplication app, NSDictionary options)
      {
         _window = new UIWindow(UIScreen.MainScreen.Bounds);

         var presenter = new MvxIosViewPresenter(this, _window);
         var setup = new Setup(this, presenter);
         setup.Initialize();

         var startup = Mvx.Resolve<IMvxAppStart>();
         startup.Start();

         _window.MakeKeyAndVisible();

         return true;
      }
   }
}