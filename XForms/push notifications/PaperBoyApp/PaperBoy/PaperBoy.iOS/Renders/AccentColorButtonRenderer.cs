using PaperBoy.iOS.Helpers;
using PaperBoy.iOS.Renders;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Button), typeof(AccentColorButtonRenderer))]

namespace PaperBoy.iOS.Renders
{
   public class AccentColorButtonRenderer : ButtonRenderer
   {
      private Color AccentColor => ColorHelper.PlatformColor;

      protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
      {
         base.OnElementChanged(e);

         if (Control != null)
         {
            var button = e.NewElement;
            button.BackgroundColor = AccentColor;
            button.TextColor = Color.White;
            button.FontAttributes = FontAttributes.Bold;
            button.CornerRadius = 22;
         }
      }
   }
}