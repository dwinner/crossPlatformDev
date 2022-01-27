namespace TelephonyApp.UWP
{
   public sealed partial class MainPage
   {
      public MainPage()
      {
         InitializeComponent();

         LoadApplication(new TelephonyApp.App());
      }
   }
}