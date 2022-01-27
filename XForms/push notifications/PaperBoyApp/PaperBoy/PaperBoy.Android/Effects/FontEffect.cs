using System;
using System.ComponentModel;
using Android.Graphics;
using Android.Widget;
using PaperBoy.Droid.Effects;
using PaperBoy.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

#pragma warning disable 618

[assembly: ResolutionGroupName("Xamarin")]
[assembly: ExportEffect(typeof(FontEffect), nameof(FontEffect))]

namespace PaperBoy.Droid.Effects
{
   public class FontEffect : PlatformEffect
   {
      private TextView _control;

      protected override void OnAttached()
      {
         try
         {
            _control = Control as TextView;
            var fontPath = $"Fonts/{CustomFontEffect.GetFontFileName(Element)}.ttf";
            var context = Forms.Context;
            var font = Typeface.CreateFromAsset(context.Assets, fontPath);
            _control.Typeface = font;
         }
         catch (Exception e)
         {
            Console.WriteLine($"Cannot set property on attached control. Error: {e.Message}");
         }
      }

      protected override void OnDetached()
      {
      }

      protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
      {
         if (args.PropertyName == CustomFontEffect.FontFileNameProperty.PropertyName)
         {
            var context = Forms.Context;
            var font = Typeface.CreateFromAsset(context.ApplicationContext.Assets,
               $"Fonts/{CustomFontEffect.GetFontFileName(Element)}.ttf");
            _control.Typeface = font;
         }
      }
   }
}