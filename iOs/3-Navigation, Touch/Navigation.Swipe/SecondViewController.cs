using System;
using JetBrains.Annotations;
using UIKit;

namespace Navigation.Swipe
{
   public partial class SecondViewController : BaseViewController
   {
      public SecondViewController(IntPtr handle) : base(handle)
      {
      }

      [UsedImplicitly]
      partial void SwipeDetected(UISwipeGestureRecognizer sender)
      {
         DismissViewController(true, null);
      }
   }
}