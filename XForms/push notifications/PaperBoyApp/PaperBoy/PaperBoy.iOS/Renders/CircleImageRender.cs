using System;
using System.ComponentModel;
using PaperBoy.iOS.Renders;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Image), typeof(CircleImageRender))]

namespace PaperBoy.iOS.Renders
{
   public class CircleImageRender : ImageRenderer
   {
      protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
      {
         base.OnElementChanged(e);

         if (e.OldElement == null || Element == null)
         {
            return;
         }

         CreateCircle();
      }

      protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
      {
         base.OnElementPropertyChanged(sender, e);
         if (e.PropertyName == VisualElement.HeightProperty.PropertyName ||
             e.PropertyName == VisualElement.WidthProperty.PropertyName)
         {
            CreateCircle();
         }
      }

      private void CreateCircle()
      {
         try
         {
            var min = Math.Min(Element.Width, Element.Height);
            Control.Layer.CornerRadius = (float) (min / 2.0);
            Control.Layer.MasksToBounds = false;
            Control.Layer.BorderColor = Color.White.ToCGColor();
            Control.Layer.BorderWidth = 3;
            Control.ClipsToBounds = true;
         }
         catch (Exception e)
         {
            Console.WriteLine(e);
            throw;
         }
      }
   }
}