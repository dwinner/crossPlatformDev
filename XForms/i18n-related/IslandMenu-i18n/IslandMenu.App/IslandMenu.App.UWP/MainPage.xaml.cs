namespace IslandMenu.App.UWP
{
   public sealed partial class MainPage
   {
      public MainPage()
      {
         InitializeComponent();

         LoadApplication(new IslandMenu.App.App());
      }
   }
}