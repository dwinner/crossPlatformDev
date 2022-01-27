using System;
using System.Timers;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using MvvmCrossDemo.Core.Infrastructure.Messages;
using MvvmCrossDemo.Core.ViewModels;

namespace MvvmCrossDemo.Core
{
   public class App : MvxApplication
   {
      public override void Initialize()
      {
         CreatableTypes().EndingWith("Service").AsInterfaces().RegisterAsLazySingleton();
         RegisterAppStart<FirstViewModel>();

         // Register Launch time message to be subscribed
         Mvx.IoCProvider.RegisterSingleton<IMvxMessenger>(new MvxMessengerHub());
         var messenger = Mvx.IoCProvider.Resolve<IMvxMessenger>();
         var dtStart = DateTime.Now;
         var timer = new Timer {Interval = 1000};
         timer.Elapsed += (sender, args) =>
         {
            var message = new LaunchTimeMessage(this, DateTime.Now.Subtract(dtStart));
            messenger.Publish(message);
         };
         timer.Start();
      }
   }
}