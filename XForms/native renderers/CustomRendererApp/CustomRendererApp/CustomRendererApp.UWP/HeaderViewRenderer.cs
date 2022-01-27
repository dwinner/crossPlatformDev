using System.ComponentModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using CustomRendererApp;
using CustomRendererApp.UWP;
using Xamarin.Forms.Platform.UWP;
using UwpColor = Windows.UI.Color;
using UwpColors = Windows.UI.Colors;
using XamColor = Xamarin.Forms.Color;

[assembly: ExportRenderer(typeof(HeaderView), typeof(HeaderViewRenderer))]

namespace CustomRendererApp.UWP
{
   public class HeaderViewRenderer : ViewRenderer<HeaderView, TextBlock>
   {
      protected override void OnElementChanged(ElementChangedEventArgs<HeaderView> e)
      {
         base.OnElementChanged(e);

         if (Control == null)
         {
            var textBlock = new TextBlock {FontSize = 28};
            textBlock.Tapped += TextBlock_Tapped;
            SetNativeControl(textBlock);

            if (e.NewElement != null)
            {
               SetText();
               SetTextColor();
            }
         }
      }

      private void TextBlock_Tapped(object sender, TappedRoutedEventArgs e) => Element?.FireClick();

      protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
      {
         base.OnElementPropertyChanged(sender, e);

         if (e.PropertyName == HeaderView.TextColorProperty.PropertyName)
         {
            SetTextColor();
         }
         else if (e.PropertyName == HeaderView.TextProperty.PropertyName)
         {
            SetText();
         }
      }

      private void SetTextColor()
      {
         var winColor = UwpColors.Black;

         if (Element.TextColor != XamColor.Default)
         {
            var color = Element.TextColor;
            const int colorMultiplier = 255;
            winColor = UwpColor.FromArgb(
               (byte) (color.A * colorMultiplier),
               (byte) (color.R * colorMultiplier),
               (byte) (color.G * colorMultiplier),
               (byte) (color.B * colorMultiplier));
         }

         Control.Foreground = new SolidColorBrush(winColor);
      }

      private void SetText() => Control.Text = Element.Text;
   }
}