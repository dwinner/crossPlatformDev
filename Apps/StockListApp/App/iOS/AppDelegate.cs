using Foundation;
using StockList.Core.Ioc;
using StockList.Core.Modules;
using StockList.Shared;
using StockListApp.iOS.Modules;
using StockListApp.Xam.Modules;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamApp = StockListApp.Xam.App;

namespace StockListApp.iOS
{
   // The UIApplicationDelegate for the application. This class is responsible for launching the 
   // User Interface of the application, as well as listening (and optionally responding) to 
   // application events from iOS.
   [Register(nameof(AppDelegate))]
   public class AppDelegate : FormsApplicationDelegate
   {
      //
      // This method is invoked when the application has loaded and is ready to run. In this 
      // method you should instantiate the window, load the UI into it and then make the window
      // visible.
      //
      // You have 17 seconds to return from this method, or iOS will terminate your application.
      //
      public override bool FinishedLaunching(UIApplication app, NSDictionary options)
      {
         Forms.Init();
         InitIoC();
         LoadApplication(new XamApp());

         return base.FinishedLaunching(app, options);
      }

      private void InitIoC()
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