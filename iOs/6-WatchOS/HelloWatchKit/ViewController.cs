using System;
using Foundation;
using JetBrains.Annotations;
using UIKit;

namespace HelloWatchKit
{
   public partial class ViewController : UIViewController
   {
      protected ViewController(IntPtr handle) : base(handle)
      {
         // Note: this .ctor should not contain any initialization logic.
      }

      [UsedImplicitly]
      partial void ButtonLocalNotification_TouchUpInside(UIButton sender)
      {
         var notification = new UILocalNotification
         {
            FireDate = (NSDate) DateTime.Now.AddSeconds(10),
            AlertTitle = "Hello!",
            AlertAction = "Alert Action",
            AlertBody = "Local notification was fired"
         };

         UIApplication.SharedApplication.ScheduleLocalNotification(notification);
      }
   }
}