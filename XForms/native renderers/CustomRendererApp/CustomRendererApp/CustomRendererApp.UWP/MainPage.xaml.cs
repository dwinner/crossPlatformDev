namespace CustomRendererApp.UWP
{
   public sealed partial class MainPage
   {
      public MainPage()
      {
         InitializeComponent();

         LoadApplication(new CustomRendererApp.App());
      }
   }
}