namespace SocialClientApp.UWP
{
   public sealed partial class MainPage
   {
      public MainPage()
      {
         InitializeComponent();

         LoadApplication(new SocialClientApp.App());
      }
   }
}