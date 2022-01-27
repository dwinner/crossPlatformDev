using System;
using CoreGraphics;
using UIKit;

namespace Navigation.Tabs
{
   public partial class ThirdViewController : BaseViewController
   {
      public ThirdViewController(IntPtr handle) : base(handle)
      {
      }

      public override void ViewDidLoad()
      {
         base.ViewDidLoad();

         AddLabel();
      }

      private void AddLabel()
      {
         // Create the label
         var label = new UILabel
         {
            Text = "Third View"
         };

         // Update font size
         nfloat fontSize = 36.0f;
         label.Font = label.Font.WithSize(fontSize);

         // Measure the label
         var labelSize = label.Text.StringSize(label.Font);

         // and adjust the frame accordingly
         label.Frame = new CGRect(View.Frame.Width / 2 - labelSize.Width / 2,
            View.Frame.Height / 2 - labelSize.Height / 2,
            labelSize.Width,
            labelSize.Height);

         Add(label);
      }
   }
}