using Foundation;
using MvvmCross.Forms.Platforms.Ios.Core;
using CoreApp = MvvxSandboxApp.Core.App;
using UIApp = MvvxSandboxApp.UI.App;

namespace MvvxSandboxApp.iOS
{
   [Register(nameof(AppDelegate))]
   public class AppDelegate : MvxFormsApplicationDelegate<Setup, CoreApp, UIApp>
   {
   }
}