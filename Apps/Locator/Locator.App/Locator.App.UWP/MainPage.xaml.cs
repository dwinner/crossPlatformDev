using Windows.UI.Xaml.Navigation;
using Locator.App.Droid.Modules;
using Locator.App.Modules;
using Locator.App.UWP.Modules;
using Locator.Common.IoC;
using Locator.Common.Modules;

namespace Locator.App.UWP
{
   /// <summary>
   ///    Main page.
   /// </summary>
   public sealed partial class MainPage
   {
      public MainPage()
      {
         InitializeComponent();
         InitIoC();
         NavigationCacheMode = NavigationCacheMode.Required;
         LoadApplication(new Locator.App.App());
      }

      private static void InitIoC()
      {
         IoC.CreateContainer();
         IoC.RegisterModule(new WinPhoneModule());
         IoC.RegisterModule(new SharedModule(true));
         IoC.RegisterModule(new XamFormsModule());
         IoC.RegisterModule(new PortableModule());
         IoC.StartContainer();
      }

      /// <summary>
      ///    Invoked when this page is about to be displayed in a Frame.
      /// </summary>
      /// <param name="e">
      ///    Event data that describes how this page was reached.
      ///    This parameter is typically used to configure the page.
      /// </param>
      protected override void OnNavigatedTo(NavigationEventArgs e)
      {
         // TODO: Prepare page for display here.

         // TODO: If your application contains multiple pages, ensure that you are
         // handling the hardware Back button by registering for the
         // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
         // If you are using the NavigationHelper provided by some templates,
         // this event is handled for you.
      }
   }
}