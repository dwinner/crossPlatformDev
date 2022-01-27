using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using MvvmCrossDemo.Core.Models;
using MvvmCrossDemo.Core.Services;

namespace MvvmCrossDemo.Core.ViewModels
{
   public class PostEditViewModel : MvxViewModel<PostViewModel, Post>
   {
      private readonly IMvxNavigationService _navigationService;
      private readonly IPostService _postService;
      private IMvxAsyncCommand _cancelCommand;
      private IMvxAsyncCommand _editPostCommand;
      private MvxNotifyTask _editPostTaskNotifier;
      private PostViewModel _post;
      private int _postId;

      public PostEditViewModel(IMvxNavigationService navigationService, IPostService postService)
      {
         _navigationService = navigationService;
         _postService = postService;
      }

      public PostViewModel Post
      {
         get => _post;
         set => SetProperty(ref _post, value);
      }

      public IMvxAsyncCommand CancelCommand =>
         _cancelCommand ?? (_cancelCommand = new MvxAsyncCommand(() => _navigationService.Close(this)));

      /// <summary>
      ///    NOTE: Use the IsNotCompleted/IsCompleted properties of the MvxNotifyTask to show an indicator.
      ///    Using the MvxNotifyTask is a recommended way to use an async command.
      /// </summary>
      public MvxNotifyTask EditPostTaskNotifier
      {
         get => _editPostTaskNotifier;
         set => SetProperty(ref _editPostTaskNotifier, value);
      }

      public IMvxAsyncCommand EditPostCommand =>
         _editPostCommand ?? (_editPostCommand = new MvxAsyncCommand(OnEditPostAsync));

      public override void Prepare(PostViewModel parameter) => _postId = parameter.Id;

      public override async Task Initialize()
      {
         await base.Initialize().ConfigureAwait(true);
         var response = await _postService.GetPostAsync(_postId).ConfigureAwait(true);
         if (response.IsSuccess)
         {
            Post = response.Result;
         }
      }

      private Task OnEditPostAsync()
      {
         EditPostTaskNotifier = MvxNotifyTask.Create(async () =>
         {
            var dtoPost = (Post) Post;
            var response = await _postService.UpdatePostAsync(Post.Id, dtoPost).ConfigureAwait(true);
            if (response.IsSuccess)
            {
               await _navigationService.Close(this, response.Result).ConfigureAwait(true);
            }
         }, OnEditPostError);

         return Task.FromResult(true);
      }

      private static void OnEditPostError(Exception error) => Debug.WriteLine(error.Message);
   }
}