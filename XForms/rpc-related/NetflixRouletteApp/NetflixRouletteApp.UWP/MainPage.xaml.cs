namespace NetflixRouletteApp.UWP
{
   public sealed partial class MainPage
   {
      public MainPage()
      {
         InitializeComponent();

         LoadApplication(new NetflixRouletteApp.App());
      }
   }
}