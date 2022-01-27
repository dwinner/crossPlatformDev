using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Microsoft.Extensions.Logging;
using MvvmCross.Forms.Platforms.Uap.Core;
using MvvmCross.Forms.Platforms.Uap.Views;
using MvvmCross.IoC;
using MvvxSandboxApp.Core.Services.Interfaces;
using MvvxSandboxApp.UWP.Impls;
using Serilog;
using Serilog.Extensions.Logging;
using CoreApp = MvvxSandboxApp.Core.App;
using UIApp = MvvxSandboxApp.UI.App;

namespace MvvxSandboxApp.UWP
{
   public sealed partial class App
   {
      public App()
      {
         InitializeComponent();
      }

      protected override void OnLaunched(LaunchActivatedEventArgs activationArgs)
      {
         var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
         coreTitleBar.ExtendViewIntoTitleBar = false;
         base.OnLaunched(activationArgs);
      }
   }

   public class Setup : MvxFormsWindowsSetup<CoreApp, UIApp>
   {
      protected override ILoggerProvider CreateLogProvider() => new SerilogLoggerProvider();

      protected override ILoggerFactory CreateLogFactory()
      {
         // serilog configuration
         Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Trace()
            .CreateLogger();

         return new SerilogLoggerFactory();
      }

      protected override void InitializeFirstChance(IMvxIoCProvider iocProvider)
      {
         //var ioCProvider = Mvx.IoCProvider;
         iocProvider.RegisterSingleton<IDbPath>(() => new UwpDbPathImpl());
         base.InitializeFirstChance(iocProvider);
      }
   }

   public abstract class MvxFormsApp : MvxWindowsApplication<Setup, CoreApp, UIApp, MainPage>
   {
   }
}