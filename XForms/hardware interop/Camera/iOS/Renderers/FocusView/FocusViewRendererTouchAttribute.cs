// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FocusViewRendererTouchAttribute.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2016 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Camera.Controls;
using Camera.iOS.Renderers.FocusView;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(FocusView),
   typeof(FocusViewRendererTouchAttribute))]

namespace Camera.iOS.Renderers.FocusView
{
   /// <summary>
   ///    Focus view renderer touch attribute.
   /// </summary>
   public class FocusViewRendererTouchAttribute : VisualElementRenderer<Controls.FocusView>
   {
      #region Public Methods

      /// <summary>
      ///    Toucheses the began.
      /// </summary>
      /// <param name="touches">Touches.</param>
      /// <param name="evt">Evt.</param>
      public override void TouchesBegan(NSSet touches, UIEvent evt)
      {
         base.TouchesBegan(touches, evt);

         var focusView = Element;

         // Get the current touch
         var touch = touches.AnyObject as UITouch;

         if (touch != null)
         {
            var posc = touch.LocationInView(touch.View);
            focusView.NotifyFocus(new Point(posc.X, posc.Y));
         }
      }

      #endregion
   }
}