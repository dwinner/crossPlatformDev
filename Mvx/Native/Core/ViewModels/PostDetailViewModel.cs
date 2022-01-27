using System.Threading.Tasks;
using MvvmCross.ViewModels;
using MvvmCrossDemo.Core.Models;
using MvvmCrossDemo.Core.Services;

namespace MvvmCrossDemo.Core.ViewModels
{
   public class PostDetailViewModel : MvxViewModel<PostViewModel>
   {
      private readonly IPostService _postService;
      private MvxObservableCollection<Comment> _commentList;
      private PostViewModel _post;
      private int _postId;

      public PostDetailViewModel(IPostService postService) => _postService = postService;

      public MvxObservableCollection<Comment> CommentList
      {
         get => _commentList;
         set => SetProperty(ref _commentList, value);
      }

      public PostViewModel Post
      {
         get => _post;
         set => SetProperty(ref _post, value);
      }

      public override void Prepare(PostViewModel postVm)
      {
         CommentList = new MvxObservableCollection<Comment>();
         _postId = postVm.Id;
      }

      public override async Task Initialize()
      {
         await base.Initialize().ConfigureAwait(true);
         await GetPostAsync(_postId).ConfigureAwait(true);
         await GetCommentsAsync(_postId).ConfigureAwait(true);
      }

      private async Task GetPostAsync(int postId)
      {
         var response = await _postService.GetPostAsync(postId).ConfigureAwait(true);
         if (response.IsSuccess)
         {
            Post = response.Result;
         }
      }

      private async Task GetCommentsAsync(int postId)
      {
         var response = await _postService.GetCommentsAsync(postId).ConfigureAwait(true);
         if (response.IsSuccess)
         {
            CommentList.AddRange(response.Result);
         }
      }
   }
}