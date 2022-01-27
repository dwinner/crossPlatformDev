namespace XamSettingsApp.UWP
{
   public sealed partial class MainPage
   {
      public MainPage()
      {
         InitializeComponent();

         LoadApplication(new XamSettingsApp.App());
      }
   }
}