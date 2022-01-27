using Foundation;
using SpeechTalk.App.iOS.Modules;
using SpeechTalk.App.IoC;
using SpeechTalk.App.Modules;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace SpeechTalk.App.iOS
{
   [Register(nameof(AppDelegate))]
   public class AppDelegate : FormsApplicationDelegate
   {
      public override bool FinishedLaunching(UIApplication app, NSDictionary options)
      {
         Forms.Init();
         InitIoC();
         LoadApplication(new App());

         return base.FinishedLaunching(app, options);
      }

      private static void InitIoC()
      {
         IoCConfuguration.CreateContainer();
         IoCConfuguration.RegisterModule(new IosModule());
         IoCConfuguration.RegisterModule(new PclModule());
         IoCConfuguration.StartContainer();
      }
   }
}