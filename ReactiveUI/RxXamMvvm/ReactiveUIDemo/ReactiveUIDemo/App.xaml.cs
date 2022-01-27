namespace ReactiveUIDemo
{
   public partial class App
   {
      public App()
      {
         InitializeComponent();

         var bootstrapper = new AppBootsrapper();
         MainPage = bootstrapper.CreateMainPage();
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