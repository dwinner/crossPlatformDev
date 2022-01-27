namespace EFCoreSampleApp.UWP
{
   public sealed partial class MainPage
   {
      public MainPage()
      {
         InitializeComponent();

         LoadApplication(new EFCoreSampleApp.App());
      }
   }
}