using System.Windows.Input;
using Foundation;
using MvvmCross.Binding.Extensions;
using MvvmCross.Platforms.Ios.Binding.Views;
using StarWarsSample.iOS.Views.Cells;
using UIKit;

namespace StarWarsSample.iOS.Sources
{
   public class PeopleTableViewSource : MvxSimpleTableViewSource
   {
      public PeopleTableViewSource(UITableView tableView)
         : base(tableView, typeof(NameTableViewCell)) => DeselectAutomatically = true;

      public ICommand FetchCommand { get; set; }

      protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
      {
         var cell = base.GetOrCreateCellFor(tableView, indexPath, item);

         if (indexPath.Item == ItemsSource.Count() - 5)
         {
            FetchCommand?.Execute(null);
         }

         return cell;
      }
   }
}