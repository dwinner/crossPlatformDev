using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CommonViews
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class ValueSelectionSample
   {
      public ValueSelectionSample()
      {
         InitializeComponent();
      }

      private void Switch1_Toggled(object sender, ToggledEventArgs e)
      {
         var isToggled = e.Value;
      }

      private void Slider1_ValueChanged(object sender, ValueChangedEventArgs e)
      {
         var value = e.NewValue;
      }

      private void Stepper1_ValueChanged(object sender, ValueChangedEventArgs e)
      {
         var value = e.NewValue;
      }
   }
}