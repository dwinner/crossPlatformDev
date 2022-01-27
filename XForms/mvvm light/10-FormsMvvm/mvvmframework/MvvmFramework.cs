using Xamarin.Forms;

namespace MvvmFramework
{
   public class MvvmFramework : ContentPage
   {
      public MvvmFramework()
      {
         var button = new Button
         {
            Text = "Click Me!",
            VerticalOptions = LayoutOptions.CenterAndExpand,
            HorizontalOptions = LayoutOptions.CenterAndExpand
         };

         var clicked = 0;
         button.Clicked += (s, e) => button.Text = "Clicked: " + clicked++;

         Content = button;
      }
   }
}