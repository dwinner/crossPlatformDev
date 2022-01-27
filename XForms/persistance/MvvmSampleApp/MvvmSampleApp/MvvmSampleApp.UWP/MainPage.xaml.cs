namespace MvvmSampleApp.UWP
{
   public sealed partial class MainPage
   {
      public MainPage()
      {
         InitializeComponent();

         LoadApplication(new MvvmSampleApp.App());
      }
   }
}