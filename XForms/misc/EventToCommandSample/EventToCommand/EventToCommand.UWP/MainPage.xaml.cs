namespace EventToCommand.UWP
{
   public sealed partial class MainPage
   {
      public MainPage()
      {
         InitializeComponent();

         LoadApplication(new EventToCommand.App());
      }
   }
}