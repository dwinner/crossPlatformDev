// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelShadowEffectiOS.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2016 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using Camera.Droid.Effects;
using Camera.Effects;
using CoreGraphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("Camera")]
[assembly: ExportEffect(typeof(LabelShadowEffectiOS), "LabelShadowEffect")]

namespace Camera.Droid.Effects
{
   /// <summary>
   ///    Label shadow effect.
   /// </summary>
   public class LabelShadowEffectiOS : PlatformEffect
   {
      #region Protected Methods

      /// <summary>
      ///    Ons the attached.
      /// </summary>
      protected override void OnAttached()
      {
         try
         {
            var effect = (LabelShadowEffect) Element.Effects.FirstOrDefault(e => e is LabelShadowEffect);

            if (effect != null)
            {
               Control.Layer.CornerRadius = effect.Radius;
               Control.Layer.ShadowColor = effect.Color.ToCGColor();
               Control.Layer.ShadowOffset = new CGSize(effect.DistanceX, effect.DistanceY);
               Control.Layer.ShadowOpacity = 1.0f;
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