using System;
using Foundation;
using UIKit;
using Users.MobileClient.Models;
using Users.MobileClient.ViewControllers;

namespace Users.MobileClient.TableSources
{
   public class UsersTableSource : UITableViewSource
   {
      private const string CellId = "UserCell";
      public UIViewController ParentViewController { get; set; }
      public UsersRepository UsersRepository { get; set; }

      public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
      {
         // Get item to display
         var user = UsersRepository.GetById(GetUserId(indexPath));

         // Try to reuse a cell before creating the new one
         var cell = tableView.DequeueReusableCell(CellId)
                    ?? new UITableViewCell(UITableViewCellStyle.Value1, CellId);

         // Configure cell properties
         cell.TextLabel.Text = user.Name;
         cell.DetailTextLabel.Text = user.Company.Name;

         return cell;
      }

      public override nint RowsInSection(UITableView tableview, nint section) => UsersRepository.Users.Count;

      public override UITableViewRowAction[] EditActionsForRow(UITableView tableView, NSIndexPath indexPath)
      {
         var removeButton = UITableViewRowAction.Create(
            UITableViewRowActionStyle.Destructive,
            "Remove",
            delegate { DeleteUser(tableView, indexPath); });

         var detailsButton = UITableViewRowAction.Create(
            UITableViewRowActionStyle.Normal,
            "Details",
            delegate { DisplayUserDetails(tableView, indexPath); });

         return new[] {removeButton, detailsButton};
      }

      private void DeleteUser(UITableView tableView, NSIndexPath indexPath)
      {
         UsersRepository.Delete(GetUserId(indexPath));
         tableView.DeleteRows(RowIndexToArray(indexPath), UITableViewRowAnimation.Automatic);
      }

      private void DisplayUserDetails(UITableView tableView, NSIndexPath indexPath)
      {
         // Instantiate view controller
         var userDetailsViewController =
            ParentViewController.Storyboard.InstantiateViewController(
               nameof(UserDetailsViewController)) as UserDetailsViewController;

         // Pass selected user
         userDetailsViewController.User = UsersRepository.GetById(GetUserId(indexPath));

         // Present view controller
         ParentViewController.PresentViewController(
            userDetailsViewController,
            true,
            () => ReloadRow(tableView, indexPath));
      }

      private int GetUserId(NSIndexPath indexPath) => UsersRepository.Users[indexPath.Row].Id;

      private static void ReloadRow(UITableView tableView, NSIndexPath indexPath)
      {
         var indexes = RowIndexToArray(indexPath);
         tableView.ReloadRows(indexes, UITableViewRowAnimation.None);
      }

      private static NSIndexPath[] RowIndexToArray(NSIndexPath indexPath) => new[] {indexPath};
   }
}