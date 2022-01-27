namespace ContextMenuMvvmApp.UWP
{
   public sealed partial class MainPage
   {
      public MainPage()
      {
         InitializeComponent();

         LoadApplication(new ContextMenuMvvmApp.App());
      }
   }
}