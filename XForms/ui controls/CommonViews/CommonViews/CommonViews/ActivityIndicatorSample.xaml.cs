using Xamarin.Forms.Xaml;

namespace CommonViews
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class ActivityIndicatorSample
   {
      public ActivityIndicatorSample()
      {
         InitializeComponent();
      }

      private void EnableIndicator()
      {
         ActivityIndicator1.IsVisible = true;
         ActivityIndicator1.IsRunning = true;

         // Do your stuff here...
         ActivityIndicator1.IsRunning = false;
         ActivityIndicator1.IsVisible = false;
      }
   }
}