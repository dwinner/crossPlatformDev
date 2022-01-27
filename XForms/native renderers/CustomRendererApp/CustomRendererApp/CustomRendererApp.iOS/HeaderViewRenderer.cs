using System.ComponentModel;
using CustomRendererApp;
using CustomRendererApp.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using IosColor = UIKit.UIColor;
using XamColor = Xamarin.Forms.Color;

[assembly: ExportRenderer(typeof(HeaderView), typeof(HeaderViewRenderer))]

namespace CustomRendererApp.iOS
{
   public class HeaderViewRenderer : ViewRenderer<HeaderView, UILabel>
   {
      private UITapGestureRecognizer _tapGestureRecognizer;

      protected override void OnElementChanged(ElementChangedEventArgs<HeaderView> e)
      {
         base.OnElementChanged(e);

         if (Control == null)
         {
            var uiLabel = new UILabel {Font = UIFont.SystemFontOfSize(25)};
            _tapGestureRecognizer = new UITapGestureRecognizer(OnHeaderViewTapped);
            uiLabel.AddGestureRecognizer(_tapGestureRecognizer);
            SetNativeControl(uiLabel);
         }

         if (e.NewElement != null)
         {
            SetText();
            SetTextColor();
         }
      }

      private void OnHeaderViewTapped() => Element?.FireClick();

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
         var iosColor = IosColor.Gray;

         if (Element.TextColor != XamColor.Default)
         {
            var xamColor = Element.TextColor;
            const int colorMultiplier = 255;
            iosColor = IosColor.FromRGBA((byte) xamColor.R * colorMultiplier,
               (byte) xamColor.G * colorMultiplier,
               (byte) xamColor.B * colorMultiplier,
               (byte) xamColor.A * colorMultiplier);
         }

         Control.TextColor = iosColor;
      }

      private void SetText() => Control.Text = Element.Text;
   }
}