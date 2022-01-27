using System;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace MvvmCrossDemo.Core.ViewModels
{
   public class WrapperPostViewModel : MvxViewModel
   {
      public WrapperPostViewModel(PostViewModel post,
         Action<PostViewModel> showPostDetailAction,
         Action<PostViewModel> editPostAction)
      {
         Post = post;
         ShowPostDetailCommand = new MvxCommand(() => showPostDetailAction(Post));
         EditPostCommand = new MvxCommand(() => editPostAction(Post));
      }

      public PostViewModel Post { get; set; }

      public IMvxCommand ShowPostDetailCommand { get; }

      public IMvxCommand EditPostCommand { get; }
   }
}