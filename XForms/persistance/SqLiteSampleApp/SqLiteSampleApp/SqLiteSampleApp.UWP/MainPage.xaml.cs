namespace SqLiteSampleApp.UWP
{
   public sealed partial class MainPage
   {
      public MainPage()
      {
         InitializeComponent();

         LoadApplication(new SqLiteSampleApp.App());
      }
   }
}