using InfiniteScrollDemo.Services;
using Xamarin.Forms;

namespace InfiniteScrollDemo
{
   public partial class App
   {
      public App()
      {
         InitializeComponent();

         DependencyService.Register<MockDataStore>();
         MainPage = new AppShell();
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