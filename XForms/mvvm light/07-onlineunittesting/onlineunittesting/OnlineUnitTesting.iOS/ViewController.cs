using System;
using UIKit;

namespace OnlineUnitTesting.iOS
{
   public partial class ViewController : UIViewController
   {
      private int _count = 1;

      public ViewController(IntPtr handle) : base(handle)
      {
      }

      public override void ViewDidLoad()
      {
         base.ViewDidLoad();
         // Perform any additional setup after loading the view, typically from a nib.
         Button.AccessibilityIdentifier = "myButton";
         Button.TouchUpInside += delegate
         {
            var title = $"{_count++} clicks!";
            Button.SetTitle(title, UIControlState.Normal);
         };
      }
   }
}