using Microsoft.Extensions.Logging;
using MvvmCross.Forms.Platforms.Ios.Core;
using MvvmCross.IoC;
using MvvxSandboxApp.Core.Services.Interfaces;
using MvvxSandboxApp.iOS.Impls;
using Serilog;
using Serilog.Extensions.Logging;
using CoreApp = MvvxSandboxApp.Core.App;
using UIApp = MvvxSandboxApp.UI.App;

namespace MvvxSandboxApp.iOS
{
   public class Setup : MvxFormsIosSetup<CoreApp, UIApp>
   {
      protected override ILoggerProvider CreateLogProvider() => new SerilogLoggerProvider();

      protected override ILoggerFactory CreateLogFactory()
      {
         // serilog configuration
         Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.NSLog()
            .CreateLogger();

         return new SerilogLoggerFactory();
      }

      protected override void InitializeFirstChance(IMvxIoCProvider iocProvider)
      {
         // var ioCProvider = Mvx.IoCProvider;
         iocProvider.RegisterSingleton<IDbPath>(() => new IosDbPathImpl());
         base.InitializeFirstChance(iocProvider);
      }
   }
}