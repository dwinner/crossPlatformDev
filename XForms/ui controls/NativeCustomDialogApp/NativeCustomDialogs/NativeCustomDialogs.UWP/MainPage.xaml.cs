namespace NativeCustomDialogs.UWP
{
   public sealed partial class MainPage
   {
      public MainPage()
      {
         InitializeComponent();

         LoadApplication(new NativeCustomDialogs.App());
      }
   }
}