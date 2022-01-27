using Xamarin.Forms;

namespace IntroducingPages
{
   public partial class App
   {
      public App()
      {
         InitializeComponent();

         // Replace with for an example about navigation
         MainPage = new NavigationPage(new NavigationSample());

         //MainPage = new IntroducingPages.MainPage();
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