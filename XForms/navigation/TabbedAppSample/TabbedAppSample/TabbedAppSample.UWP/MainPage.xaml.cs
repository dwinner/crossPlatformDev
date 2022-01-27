namespace TabbedAppSample.UWP
{
   public sealed partial class MainPage
   {
      public MainPage()
      {
         InitializeComponent();

         LoadApplication(new TabbedAppSample.App());
      }
   }
}