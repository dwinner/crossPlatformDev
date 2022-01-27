﻿using System;

namespace Xamarin.Forms.BehaviorsPack
{
   public class DisplayActionSheetRequestEventArgs : EventArgs
   {
      public string Title { get; set; }
      public IActionSheetButton Cancel { get; set; }
      public IActionSheetButton Destruction { get; set; }
      public IActionSheetButton[] ActionSheetButtons { get; set; }
   }
}