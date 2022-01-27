using AudioPlayerApp.Core;
using Foundation;
using MvvmCross.Platforms.Ios.Core;

namespace AudioPlayerApp.iOS
{
   [Register(nameof(AppDelegate))]
   public class AppDelegate : MvxApplicationDelegate<Setup, App>
   {
   }
}