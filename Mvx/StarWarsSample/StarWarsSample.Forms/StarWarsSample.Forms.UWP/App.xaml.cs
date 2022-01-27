using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using FFImageLoading.Forms.Platform;
using MvvmCross.Forms.Platforms.Uap.Core;
using MvvmCross.Forms.Platforms.Uap.Views;
using OxyPlot.Xamarin.Forms.Platform.UWP;

namespace StarWarsSample.Forms.UWP
{
   public sealed partial class App
   {
      public App()
      {
         InitializeComponent();
      }

      protected override void OnLaunched(LaunchActivatedEventArgs activationArgs)
      {
         CachedImageRenderer.Init();
         PlotViewRenderer.Init();

         var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
         coreTitleBar.ExtendViewIntoTitleBar = false;

         base.OnLaunched(activationArgs);
      }
   }

   public abstract class StarWarsSampleApp
      : MvxWindowsApplication<MvxFormsWindowsSetup<Core.App, UI.App>, Core.App, UI.App, MainPage>
   {
   }
}