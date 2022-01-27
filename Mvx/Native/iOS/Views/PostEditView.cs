using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using MvvmCrossDemo.Core.ViewModels;

namespace MvvmCrossDemo.iOS.Views
{
   [MvxFromStoryboard(nameof(PostEditView))]
   public partial class PostEditView : MvxViewController<PostEditViewModel>
   {
      public PostEditView(IntPtr handle) : base(handle)
      {
      }

      public override void ViewDidLoad()
      {
         base.ViewDidLoad();
         var set = this.CreateBindingSet<PostEditView, PostEditViewModel>();
         set.Bind(txtTitle).To(vm => vm.Post.Title).TwoWay();
         set.Bind(txtBody).To(vm => vm.Post.Body).TwoWay();
         set.Bind(btnCancel).To(vm => vm.CancelCommand);
         set.Bind(btnOk).To(vm => vm.EditPostCommand);
         set.Apply();
      }
   }
}