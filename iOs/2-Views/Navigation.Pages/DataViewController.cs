using System;
using System.Collections.Generic;
using UIKit;

namespace Navigation.Pages
{
   public partial class DataViewController : UIViewController
   {
      protected DataViewController(IntPtr handle) : base(handle)
      {
         // Note: this .ctor should not contain any initialization logic.
      }

      public KeyValuePair<string, UIColor> DataObject { get; set; }

      public override void ViewWillAppear(bool animated)
      {
         base.ViewWillAppear(animated);

         dataLabel.Text = DataObject.Key;
         ViewDataPanel.BackgroundColor = DataObject.Value;
      }
   }
}