using System.ComponentModel;
using Android.Content;
using Android.Util;
using Android.Widget;
using CustomRendererApp;
using CustomRendererApp.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using DroidColor = Android.Graphics.Color;
using XamColor = Xamarin.Forms.Color;

[assembly: ExportRenderer(typeof(HeaderView), typeof(HeaderViewRenderer))]

namespace CustomRendererApp.Droid
{
   public class HeaderViewRenderer : ViewRenderer<HeaderView, TextView>
   {
      public HeaderViewRenderer(Context context)
         : base(context)
      {
      }

      protected override void OnElementChanged(ElementChangedEventArgs<HeaderView> e)
      {
         base.OnElementChanged(e);

         if (Control == null)
         {
            // Создаем и настраиваем элемент
            var textView = new TextView(Context);
            textView.SetTextSize(ComplexUnitType.Dip, 28);
            textView.Touch += OnTouch;

            // Устанавливаем элемент для класса из portable-проекта
            SetNativeControl(textView);

            // Установка свойств
            if (e.NewElement != null)
            {
               SetText();
               SetTextColor();
            }
         }
      }

      private void OnTouch(object sender, TouchEventArgs e) => Element?.FireClick();

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
         var droidColor = DroidColor.Gray;

         if (Element.TextColor != XamColor.Default)
         {
            var xamColor = Element.TextColor;
            const int colorMultiplier = 0xFF;
            droidColor = DroidColor.Argb((byte) (xamColor.A * colorMultiplier),
               (byte) (xamColor.R * colorMultiplier),
               (byte) (xamColor.G * colorMultiplier),
               (byte) (xamColor.B * colorMultiplier));
         }

         Control.SetTextColor(droidColor);
      }

      private void SetText() => Control.Text = Element.Text;
   }
}