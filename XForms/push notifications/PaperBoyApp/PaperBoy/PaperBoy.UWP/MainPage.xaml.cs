namespace PaperBoy.UWP
{
   public sealed partial class MainPage
   {
      public MainPage()
      {
         InitializeComponent();

         LoadApplication(new PaperBoy.App());
      }
   }
}