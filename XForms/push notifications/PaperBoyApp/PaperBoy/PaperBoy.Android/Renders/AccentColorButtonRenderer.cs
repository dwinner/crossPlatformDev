using Android.Content;
using PaperBoy.Droid.Helpers;
using PaperBoy.Droid.Renders;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Button), typeof(AccentColorButtonRenderer))]

namespace PaperBoy.Droid.Renders
{
   public class AccentColorButtonRenderer : ButtonRenderer
   {
      public AccentColorButtonRenderer(Context context) : base(context)
      {
      }

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