using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using MvvmCrossDemo.Core.Models;
using MvvmCrossDemo.Core.Services;

namespace MvvmCrossDemo.Core.ViewModels
{
   public class PostListViewModel : MvxViewModel
   {
      private readonly IMvxNavigationService _navigationService;
      private readonly IPostService _postService;
      private MvxObservableCollection<WrapperPostViewModel> _postList;

      public PostListViewModel(IPostService postService, IMvxNavigationService navigationService)
      {
         _postService = postService;
         _navigationService = navigationService;
      }

      public MvxObservableCollection<WrapperPostViewModel> PostList
      {
         get => _postList;
         set => SetProperty(ref _postList, value);
      }

      public override void Prepare() => PostList = new MvxObservableCollection<WrapperPostViewModel>();

      public override async Task Initialize()
      {
         await base.Initialize().ConfigureAwait(true);
         await GetPostsAsync().ConfigureAwait(true);
      }

      private async Task GetPostsAsync()
      {
         var response = await _postService.GetPostsAsync().ConfigureAwait(true);
         if (response.IsSuccess)
         {
            PostList.AddRange(response.Result.Select(post =>
               new WrapperPostViewModel(post, ShowPostDetailAsync, EditPostAsync)));
         }
      }

      private void ShowPostDetailAsync(PostViewModel post) =>
         _navigationService.Navigate<PostDetailViewModel, PostViewModel>(post);

      private async void EditPostAsync(PostViewModel post)
      {
         var result = await _navigationService.Navigate<PostEditViewModel, PostViewModel, Post>(post)
            .ConfigureAwait(true);
         if (result != null)
         {
            var target = PostList.FirstOrDefault(x => x.Post.Id == result.Id);
            if (target != null)
            {
               target.Post.Title = result.Title;
               target.Post.Body = result.Body;
            }
         }
      }
   }
}