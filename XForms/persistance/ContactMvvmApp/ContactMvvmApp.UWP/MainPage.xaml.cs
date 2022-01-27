namespace ContactMvvmApp.UWP
{
   public sealed partial class MainPage
   {
      public MainPage()
      {
         InitializeComponent();

         LoadApplication(new ContactMvvmApp.App());
      }
   }
}