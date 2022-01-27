using AudioPlayerApp.Core;
using AudioPlayerApp.Core.Sound;
using AudioPlayerApp.iOS.Sound;
using MvvmCross;
using MvvmCross.Platforms.Ios.Core;

namespace AudioPlayerApp.iOS
{
   public class Setup : MvxIosSetup<App>
   {
      protected override void InitializeFirstChance()
      {
         base.InitializeFirstChance();
         Mvx.IoCProvider.RegisterType<ISoundHandler, IosSoundHandlerImpl>();
      }
   }
}