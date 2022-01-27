using PaperBoy.UWP.Helpers;
using PaperBoy.UWP.Renders;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(Button), typeof(AccentColorButtonRenderer))]

namespace PaperBoy.UWP.Renders
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