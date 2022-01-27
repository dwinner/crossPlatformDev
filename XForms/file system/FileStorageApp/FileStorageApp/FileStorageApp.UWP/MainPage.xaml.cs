namespace FileStorageApp.UWP
{
   public sealed partial class MainPage
   {
      public MainPage()
      {
         InitializeComponent();

         LoadApplication(new FileStorageApp.App());
      }
   }
}