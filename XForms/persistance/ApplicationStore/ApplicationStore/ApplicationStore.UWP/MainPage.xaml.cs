namespace ApplicationStore.UWP
{
   public sealed partial class MainPage
   {
      public MainPage()
      {
         InitializeComponent();

         LoadApplication(new ApplicationStore.App());
      }
   }
}