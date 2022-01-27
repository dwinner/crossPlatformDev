using System;
using Android.Content;
using Android.Graphics;
using StockListApp.Droid.Renderers;
using StockListApp.Xam.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomLabel), typeof(CustomLabelRenderer))]

namespace StockListApp.Droid.Renderers
{
   public class CustomLabelRenderer : LabelRenderer
   {
      public CustomLabelRenderer(Context context)
         : base(context)
      {
      }

      protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
      {
         base.OnElementChanged(e);

         var customLabel = e.NewElement as CustomLabel;
         if (!string.IsNullOrEmpty(customLabel?.AndroidFontStyle))
         {
            try
            {
               var font = Typeface.CreateFromAsset(Context.ApplicationContext.Assets,
                  $"{customLabel.AndroidFontStyle}.ttf");
               if (Control != null)
               {
                  Control.Typeface = font;
                  Control.TextSize = (float) e.NewElement.FontSize;
               }
            }
            catch (Exception ex)
            {
               System.Diagnostics.Debug.WriteLine(ex.Message);
            }
         }
      }
   }
}