namespace ReactiveUIDemo.UWP
{
   public sealed partial class MainPage
   {
      public MainPage()
      {
         InitializeComponent();

         LoadApplication(new ReactiveUIDemo.App());
      }
   }
}