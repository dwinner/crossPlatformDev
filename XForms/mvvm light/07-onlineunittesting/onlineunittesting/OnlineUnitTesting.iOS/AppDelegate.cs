﻿using Foundation;
using UIKit;

namespace OnlineUnitTesting.iOS
{
   [Register(nameof(AppDelegate))]
   public class AppDelegate : UIApplicationDelegate
   {
      // class-level declarations

      public override UIWindow Window { get; set; }

      public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions) => true;
   }
}