using SpeechTalk.App.IoC;
using SpeechTalk.App.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace SpeechTalk.App
{
   public partial class App
   {
      public App()
      {
         var mainPage = IoCConfuguration.Resolve<MainPage>();
         MainPage = new NavigationPage(mainPage);
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