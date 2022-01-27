using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MvvmCrossDemo.Core.Models;

namespace MvvmCrossDemo.Core.Services
{
   public interface IPostService
   {
      Task<ResponseMessage<List<Post>>> GetPostsAsync(CancellationToken token = default);

      Task<ResponseMessage<Post>> GetPostAsync(int postId, CancellationToken token = default);

      Task<ResponseMessage<List<Comment>>> GetCommentsAsync(int postId, CancellationToken token = default);

      Task<ResponseMessage<Post>> UpdatePostAsync(int postId, Post post, CancellationToken token = default);
   }
}