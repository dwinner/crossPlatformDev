using Xamarin.Forms;

namespace i18nSampleApp
{
   public partial class App
   {
      public App()
      {
         var runtimePlatform = Device.RuntimePlatform;
         if (runtimePlatform != "UWP")
         {
            Resource.Culture = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
         }

         InitializeComponent();

         MainPage = new MainPage();
      }

      protected override void OnStart()
      {
         // Handle when your app starts
      }

      protected override void OnSleep()
      {
         // Handle when your app sleeps
      }

      protected override void OnResume()
      {
         // Handle when your app resumes
      }
   }
}