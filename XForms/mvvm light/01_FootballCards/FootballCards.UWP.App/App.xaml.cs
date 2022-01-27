using System;
using System.Diagnostics;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using FootballCards.SharedLib;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;

namespace FootballCards.UWP.App
{
   public sealed partial class App
   {
      private TransitionCollection _transitions;

      public App()
      {
         InitializeComponent();
         Suspending += OnSuspending;

         // Create an instance of the Navigation service
         var navigation = new NavigationService();

         // configure the service
         navigation.Configure(ViewModelLocator.MainPageKey, typeof(MainPage));
         navigation.Configure(ViewModelLocator.MapPageKey, typeof(MapPage));

         // register the service
         SimpleIoc.Default.Register<INavigationService>(() => navigation);
      }

      /// <summary>
      ///    Invoked when the application is launched normally by the end user. Other entry points
      ///    will be used such as when the application is launched to open a specific file.
      /// </summary>
      /// <param name="e">Details about the launch request and process.</param>
      protected override void OnLaunched(LaunchActivatedEventArgs e)
      {
#if DEBUG
         if (Debugger.IsAttached)
         {
            DebugSettings.EnableFrameRateCounter = true;
         }
#endif

         if (!(Window.Current.Content is Frame rootFrame))
         {
            rootFrame = new Frame {CacheSize = 1};
            Window.Current.Content = rootFrame;
         }

         if (rootFrame.Content == null)
         {
            if (rootFrame.ContentTransitions != null)
            {
               _transitions = new TransitionCollection();
               foreach (var transition in rootFrame.ContentTransitions)
               {
                  _transitions.Add(transition);
               }
            }

            rootFrame.ContentTransitions = null;
            rootFrame.Navigated += OnRootFrame_FirstNavigated;

            if (!rootFrame.Navigate(typeof(MainPage), e.Arguments))
            {
               throw new Exception("Failed to create initial page");
            }
         }

         Window.Current.Activate();
      }

      private void OnRootFrame_FirstNavigated(object sender, NavigationEventArgs e)
      {
         if (sender is Frame rootFrame)
         {
            rootFrame.ContentTransitions = _transitions ?? new TransitionCollection
            {
               new NavigationThemeTransition()
            };
            rootFrame.Navigated -= OnRootFrame_FirstNavigated;
         }
      }

      private static void OnSuspending(object sender, SuspendingEventArgs e)
      {
         var deferral = e.SuspendingOperation.GetDeferral();
         deferral.Complete();
      }
   }
}