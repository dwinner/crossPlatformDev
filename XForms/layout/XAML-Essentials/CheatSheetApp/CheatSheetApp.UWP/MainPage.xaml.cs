namespace CheatSheetApp.UWP
{
   public sealed partial class MainPage
   {
      public MainPage()
      {
         InitializeComponent();

         LoadApplication(new CheatSheetApp.App());
      }
   }
}