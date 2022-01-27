namespace NavigationSampleApp.UWP
{
   public sealed partial class MainPage
   {
      public MainPage()
      {
         InitializeComponent();

         LoadApplication(new NavigationSampleApp.App());
      }
   }
}