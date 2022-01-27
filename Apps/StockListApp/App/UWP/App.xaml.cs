using System;
using System.Diagnostics;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Xamarin.Forms;
using Frame = Windows.UI.Xaml.Controls.Frame;
using NavigationEventArgs = Windows.UI.Xaml.Navigation.NavigationEventArgs;

namespace StockListApp.UWP
{
   /// <summary>
   ///    Provides application-specific behavior to supplement the default Application class.
   /// </summary>
   public sealed partial class App
   {
      private TransitionCollection _transitions;

      /// <summary>
      ///    Initializes the singleton application object.  This is the first line of authored code
      ///    executed, and as such is the logical equivalent of main() or WinMain().
      /// </summary>
      public App()
      {
         InitializeComponent();
         Suspending += OnSuspending;
      }

      /// <summary>
      ///    Invoked when the application is launched normally by the end user.  Other entry points
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

         // Do not repeat app initialization when the Window already has content,
         // just ensure that the window is active
         if (!(Window.Current.Content is Frame rootFrame))
         {
            // Create a Frame to act as the navigation context and navigate to the first page
            rootFrame = new Frame {CacheSize = 1};

            Forms.Init(e);

            //if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
            //{
            // Load state from previously suspended application
            //}

            // Place the frame in the current Window
            Window.Current.Content = rootFrame;
         }

         if (e.PrelaunchActivated)
         {
            return;
         }

         if (rootFrame.Content == null)
         {
            // Removes the turnstile navigation for startup.
            if (rootFrame.ContentTransitions != null)
            {
               _transitions = new TransitionCollection();
               foreach (var transition in rootFrame.ContentTransitions)
               {
                  _transitions.Add(transition);
               }
            }

            rootFrame.ContentTransitions = null;
            rootFrame.Navigated += OnFirstNavigated;
            rootFrame.NavigationFailed += OnNavigationFailed;

            // When the navigation stack isn't restored navigate to the first page,
            // configuring the new page by passing required information as a navigation
            // parameter
            rootFrame.Navigate(typeof(MainPage), e.Arguments);
         }

         // Ensure the current window is active
         Window.Current.Activate();
      }

      private void OnFirstNavigated(object sender, NavigationEventArgs e)
      {
         if (sender is Frame rootFrame)
         {
            rootFrame.ContentTransitions = _transitions ?? new TransitionCollection {new NavigationThemeTransition()};
            rootFrame.Navigated -= OnFirstNavigated;
         }
      }

      /// <summary>
      ///    Invoked when Navigation to a certain page fails
      /// </summary>
      /// <param name="sender">The Frame which failed navigation</param>
      /// <param name="e">Details about the navigation failure</param>
      private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
      {
         throw new Exception($"Failed to load Page {e.SourcePageType.FullName}");
      }

      /// <summary>
      ///    Invoked when application execution is being suspended.  Application state is saved
      ///    without knowing whether the application will be terminated or resumed with the contents
      ///    of memory still intact.
      /// </summary>
      /// <param name="sender">The source of the suspend request.</param>
      /// <param name="e">Details about the suspend request.</param>
      private void OnSuspending(object sender, SuspendingEventArgs e)
      {
         var deferral = e.SuspendingOperation.GetDeferral();
         deferral.Complete();
      }
   }
}