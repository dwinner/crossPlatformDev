namespace RealmSampleApp.UWP
{
   public sealed partial class MainPage
   {
      public MainPage()
      {
         InitializeComponent();

         LoadApplication(new RealmSampleApp.App());
      }
   }
}