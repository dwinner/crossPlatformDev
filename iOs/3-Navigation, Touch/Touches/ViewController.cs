using System;
using System.Diagnostics;
using System.Linq;
using Foundation;
using UIKit;

namespace Touches
{
   public partial class ViewController : UIViewController
   {
      protected ViewController(IntPtr handle) : base(handle)
      {
         // Note: this .ctor should not contain any initialization logic.
      }

      public override void TouchesBegan(NSSet touches, UIEvent evt)
      {
         base.TouchesBegan(touches, evt);

         Debug.WriteLine("TouchesBegan");
      }

      public override void TouchesMoved(NSSet touches, UIEvent evt)
      {
         base.TouchesMoved(touches, evt);

         Debug.WriteLine("TouchesMoved");

         foreach (var touch in touches.Cast<UITouch>())
         {
            Debug.WriteLine(touch.GetPreciseLocation(View));
         }
      }

      public override void TouchesCancelled(NSSet touches, UIEvent evt)
      {
         base.TouchesCancelled(touches, evt);

         Debug.WriteLine("TouchesCancelled");
      }

      public override void TouchesEnded(NSSet touches, UIEvent evt)
      {
         base.TouchesEnded(touches, evt);

         Debug.WriteLine("TouchesEnded");
      }
   }
}