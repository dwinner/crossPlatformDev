using Foundation;
using Locator.App.Droid.Modules;
using Locator.App.iOS.Modules;
using Locator.App.Modules;
using Locator.Common.IoC;
using Locator.Common.Modules;
using UIKit;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace Locator.App.iOS
{
   /// <summary>
   ///    App delegate.
   /// </summary>
   [Register(nameof(AppDelegate))]
   public class AppDelegate : FormsApplicationDelegate
   {
      /// <summary>
      ///    Finisheds the launching.
      /// </summary>
      /// <returns>The launching.</returns>
      /// <param name="app">App.</param>
      /// <param name="options">Options.</param>
      public override bool FinishedLaunching(UIApplication app, NSDictionary options)
      {
         FormsMaps.Init();
         Forms.Init();
         InitIoC();
         LoadApplication(new App());

         return base.FinishedLaunching(app, options);
      }

      /// <summary>
      ///    Inits the IoC container and modules
      /// </summary>
      /// <returns>The io c.</returns>
      private static void InitIoC()
      {
         IoC.CreateContainer();
         IoC.RegisterModule(new IosModule());
         IoC.RegisterModule(new SharedModule(false));
         IoC.RegisterModule(new XamFormsModule());
         IoC.RegisterModule(new PortableModule());
         IoC.StartContainer();
      }
   }
}