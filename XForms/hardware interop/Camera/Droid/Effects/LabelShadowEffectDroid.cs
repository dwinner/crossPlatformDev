// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelShadowEffectDroid.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2016 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using Android.Widget;
using Camera.Droid.Effects;
using Camera.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("Camera")]
[assembly: ExportEffect(typeof(LabelShadowEffectDroid), "LabelShadowEffect")]

namespace Camera.Droid.Effects
{
   /// <summary>
   ///    Label shadow effect.
   /// </summary>
   public class LabelShadowEffectDroid : PlatformEffect
   {
      #region Protected Methods

      /// <summary>
      ///    Ons the attached.
      /// </summary>
      protected override void OnAttached()
      {
         try
         {
            var control = Control as TextView;

            var effect = (LabelShadowEffect) Element.Effects.FirstOrDefault(e => e is LabelShadowEffect);

            if (effect != null)
            {
               var adrColor = Xamarin.Forms.Platform.Android.ColorExtensions.ToAndroid(effect.Color);
               control.SetShadowLayer(effect.Radius, effect.DistanceX, effect.DistanceY, adrColor);
            }
         }
         catch (Exception ex)
         {
            Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
         }
      }

      /// <summary>
      ///    Ons the detached.
      /// </summary>
      protected override void OnDetached()
      {
      }

      #endregion
   }
}