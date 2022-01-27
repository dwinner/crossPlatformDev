namespace ContactBookApp.UWP
{
   public sealed partial class MainPage
   {
      public MainPage()
      {
         InitializeComponent();

         LoadApplication(new ContactBookApp.App());
      }
   }
}