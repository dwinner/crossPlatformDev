using CoreAnimation;
using Foundation;
using UIKit;

namespace StarWarsSample.iOS.Extensions
{
   public static class UiViewExtensions
   {
      public static void PulseToSize(this UIView view, float scale, double duration, bool repeat,
         bool autoReverse = false)
      {
         var pulseAnimation = CABasicAnimation.FromKeyPath("transform.scale");
         pulseAnimation.Duration = duration;
         pulseAnimation.To = NSNumber.FromFloat(scale);
         pulseAnimation.TimingFunction = CAMediaTimingFunction.FromName(CAMediaTimingFunction.EaseInEaseOut);
         pulseAnimation.AutoReverses = autoReverse;
         pulseAnimation.RepeatCount = repeat == false ? 0 : float.MaxValue;

         view.Layer.AddAnimation(pulseAnimation, "pulse");
      }
   }
}