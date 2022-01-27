using Locator.Common.IoC;
using Xamarin.Forms;

namespace Locator.App
{
   /// <summary>
   ///    App.
   /// </summary>
   public partial class App
   {
      /// <summary>
      ///    Initializes a new instance of the <see cref="T:Locator.App.App" /> class.
      /// </summary>
      public App() => MainPage = IoC.Resolve<NavigationPage>();

      /// <summary>
      ///    Called when app is started.
      /// </summary>
      /// <returns>The start.</returns>
      protected override void OnStart()
      {
         // Handle when your app starts
      }

      /// <summary>
      ///    Called when app is background.
      /// </summary>
      /// <returns>The sleep.</returns>
      protected override void OnSleep()
      {
         // Handle when your app sleeps
      }

      /// <summary>
      ///    Called when app is foreground.
      /// </summary>
      /// <returns>The resume.</returns>
      protected override void OnResume()
      {
         // Handle when your app resumes
      }
   }
}