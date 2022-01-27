using System;
using System.Linq;
using CoreGraphics;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using MvvmCross.Platforms.Ios.Views;
using MvvmCrossDemo.Core.ViewModels;
using UIKit;

namespace MvvmCrossDemo.iOS.Views
{
   [MvxFromStoryboard(nameof(PostListView))]
   public partial class PostListView : MvxTableViewController<PostListViewModel>
   {
      private PostListTableSource _source;

      public PostListView(IntPtr handle) : base(handle)
      {
      }

      public override void ViewDidLoad()
      {
         base.ViewDidLoad();
         _source = new PostListTableSource(TableView);
         TableView.Source = _source;

         var set = this.CreateBindingSet<PostListView, PostListViewModel>();
         set.Bind(_source).To(vm => vm.PostList);
         set.Apply();
         TableView.ReloadData();
      }
   }

   public class PostListTableSource : MvxTableViewSource
   {
      internal const string PostCell = nameof(PostCell);
      private static readonly NSString PostCellIdentifier = new NSString(PostCell);

      public PostListTableSource(UITableView tableView) : base(tableView)
      {
         // tableView.RegisterNibForCellReuse(UINib.FromName(PostCell, NSBundle.MainBundle), PostCellIdentifier);
         // tableView.RegisterClassForCellReuse(typeof(PostListTableCell), PostCellIdentifier);
      }

      protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
      {
         // You must set the identifier of the table cell in the designer. Otherwise, you must register the table cell first in the constructor.
         var cell = TableView.DequeueReusableCell(PostCellIdentifier, indexPath);
         cell.TextLabel.Text = ((WrapperPostViewModel) item).Post.Title;
         cell.DetailTextLabel.Text = ((WrapperPostViewModel) item).Post.Body;
         cell.Accessory = UITableViewCellAccessory.DetailDisclosureButton;
         return cell;
      }

      public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
      {
         base.RowSelected(tableView, indexPath);
         var item = SelectedItem;
         ((WrapperPostViewModel) item)?.ShowPostDetailCommand.Execute((WrapperPostViewModel) item);
      }

      public override void AccessoryButtonTapped(UITableView tableView, NSIndexPath indexPath)
      {
         base.AccessoryButtonTapped(tableView, indexPath);
         var item = ItemsSource.Cast<WrapperPostViewModel>().ToList()[indexPath.Row];
         item?.EditPostCommand.Execute(item);
      }
   }

   [Register(PostListTableSource.PostCell)]
   internal sealed class PostListTableCell : MvxTableViewCell
   {
      private UILabel _lablePostBody;
      private UILabel _lablePostTitle;

      public PostListTableCell(IntPtr handle) : base(handle)
      {
         CreateLayout();
         InitializeBindings();
      }

      public override void LayoutSubviews()
      {
         base.LayoutSubviews();
         // var width = ContentView.Frame.Width;
         _lablePostTitle.Frame = new CGRect(20, 7, 100, 30);
      }

      private void CreateLayout()
      {
         Accessory = UITableViewCellAccessory.DisclosureIndicator;
         _lablePostTitle = new UILabel();
         _lablePostBody = new UILabel();
         Accessory = UITableViewCellAccessory.DisclosureIndicator;
         ContentView.AddSubviews(_lablePostTitle, _lablePostBody);
      }

      private void InitializeBindings()
      {
         this.DelayBind(() =>
         {
            var set = this.CreateBindingSet<PostListTableCell, WrapperPostViewModel>();
            set.Bind(_lablePostTitle).To(vm => vm.Post.Title).TwoWay();
            set.Bind(_lablePostBody).To(vm => vm.Post.Body).TwoWay();
            set.Apply();
         });
      }
   }
}