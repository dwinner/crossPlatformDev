#nullable enable
using Android.App;
using Microsoft.Extensions.Logging;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.IoC;
using MvvxSandboxApp.Core.Services.Interfaces;
using MvvxSandboxApp.Droid.Impls;
using Serilog;
using Serilog.Extensions.Logging;
using CoreApp = MvvxSandboxApp.Core.App;
using UIApp = MvvxSandboxApp.UI.App;

#if DEBUG
[assembly: Application(Debuggable = true)]
#else
[assembly: Application(Debuggable = false)]
#endif

namespace MvvxSandboxApp.Droid
{
   public class Setup : MvxFormsAndroidSetup<CoreApp, UIApp>
   {
      protected override ILoggerProvider CreateLogProvider() => new SerilogLoggerProvider();

      protected override ILoggerFactory CreateLogFactory()
      {
         // serilog configuration
         Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.AndroidLog()
            .CreateLogger();

         return new SerilogLoggerFactory();
      }

      protected override void InitializeFirstChance(IMvxIoCProvider iocProvider)
      {
         //var ioCProvider = Mvx.IoCProvider;
         iocProvider.RegisterSingleton<IDbPath>(() => new DroidDbPathImpl());
         base.InitializeFirstChance(iocProvider);
      }
   }
}