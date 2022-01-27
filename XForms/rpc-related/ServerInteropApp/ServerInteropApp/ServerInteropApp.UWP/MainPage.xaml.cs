namespace ServerInteropApp.UWP
{
   public sealed partial class MainPage
   {
      public MainPage()
      {
         InitializeComponent();

         LoadApplication(new ServerInteropApp.App());
      }
   }
}