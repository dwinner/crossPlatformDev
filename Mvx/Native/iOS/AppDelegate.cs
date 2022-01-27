using Foundation;
using MvvmCross.Platforms.Ios.Core;
using MvvmCrossDemo.Core;

namespace MvvmCrossDemo.iOS
{
   [Register(nameof(AppDelegate))]
   public class AppDelegate : MvxApplicationDelegate<MvxIosSetup<App>, App>
   {
   }
}