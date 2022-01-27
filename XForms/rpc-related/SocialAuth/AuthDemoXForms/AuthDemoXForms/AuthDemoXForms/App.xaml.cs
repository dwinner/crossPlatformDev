using AuthDemoXForms.Services;
using AuthDemoXForms.Views;
using Xamarin.Forms;

namespace AuthDemoXForms
{
   public partial class App : Application
   {
      public App()
      {
         InitializeComponent();

         DependencyService.Register<MockDataStore>();
         //MainPage = new AppShell();
         MainPage = new AuthPage();
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