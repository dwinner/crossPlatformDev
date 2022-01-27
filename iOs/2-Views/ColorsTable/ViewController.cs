using System;
using ColorsTable.Colors;
using UIKit;

namespace ColorsTable
{
   public partial class ViewController : UIViewController
   {
      protected ViewController(IntPtr handle) : base(handle)
      {
      }

      public override void ViewDidLoad()
      {
         base.ViewDidLoad();

         var table = new UITableView(View.Frame)
         {
            Source = new ColorsTableSource(this)
         };

         Add(table);
      }
   }
}