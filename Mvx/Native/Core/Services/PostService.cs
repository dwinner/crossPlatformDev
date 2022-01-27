using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MvvmCrossDemo.Core.Infrastructure.Extensions;
using MvvmCrossDemo.Core.Models;

namespace MvvmCrossDemo.Core.Services
{
   public class PostService : IPostService
   {
      private const string ApiUrl = "https://jsonplaceholder.typicode.com/";
      private readonly HttpClient _httpClient;

      public PostService() => _httpClient = new HttpClient();

      public async Task<ResponseMessage<List<Post>>> GetPostsAsync(CancellationToken token = default)
      {
         try
         {
            var response = await _httpClient.GetAsync($"{ApiUrl}posts", token).ConfigureAwait(false);
            if (response.StatusCode == HttpStatusCode.OK)
            {
               var result = await response.ReadAsJsonAsync<List<Post>>().ConfigureAwait(false);
               return new ResponseMessage<List<Post>>
               {
                  IsSuccess = true,
                  Result = result
               };
            }

            return new ResponseMessage<List<Post>>
            {
               IsSuccess = false,
               Message = "Errors" // Show the detailed error message here according to the response.
            };
         }
         catch (Exception e)
         {
            // Debug.WriteLine(e.Message);
            return new ResponseMessage<List<Post>>
            {
               IsSuccess = false,
               Message = e.Message // Show the detailed error message here.
            };
         }
      }

      public async Task<ResponseMessage<Post>> GetPostAsync(int postId, CancellationToken token = default)
      {
         try
         {
            var response = await _httpClient.GetAsync($"{ApiUrl}posts/{postId}", token).ConfigureAwait(false);
            if (response.StatusCode == HttpStatusCode.OK)
            {
               var result = await response.ReadAsJsonAsync<Post>().ConfigureAwait(false);
               return new ResponseMessage<Post>
               {
                  IsSuccess = true,
                  Result = result
               };
            }

            return new ResponseMessage<Post>
            {
               IsSuccess = false,
               Message = "Errors" // Show the detailed error message here according to the response.
            };
         }
         catch (Exception e)
         {
            return new ResponseMessage<Post>
            {
               IsSuccess = false,
               Message = e.Message // Show the detailed error message here.
            };
         }
      }

      public async Task<ResponseMessage<List<Comment>>> GetCommentsAsync(int postId, CancellationToken token = default)
      {
         try
         {
            var response = await _httpClient.GetAsync($"{ApiUrl}posts/{postId}/comments", token).ConfigureAwait(false);
            if (response.StatusCode == HttpStatusCode.OK)
            {
               var result = await response.ReadAsJsonAsync<List<Comment>>().ConfigureAwait(false);
               return new ResponseMessage<List<Comment>>
               {
                  IsSuccess = true,
                  Result = result
               };
            }

            return new ResponseMessage<List<Comment>>
            {
               IsSuccess = true,
               Message = response.ReasonPhrase
            };
         }
         catch (Exception e)
         {
            return new ResponseMessage<List<Comment>>
            {
               IsSuccess = false,
               Message = e.Message
            };
         }
      }

      public async Task<ResponseMessage<Post>> UpdatePostAsync(int postId, Post post, CancellationToken token = default)
      {
         try
         {
            var content = post.ToStringContent();
            var response = await _httpClient.PutAsync($"{ApiUrl}posts/{postId}", content, token).ConfigureAwait(false);
            if (response.StatusCode == HttpStatusCode.OK)
            {
               var result = await response.ReadAsJsonAsync<Post>().ConfigureAwait(false);
               return new ResponseMessage<Post>
               {
                  IsSuccess = true,
                  Result = result
               };
            }

            return new ResponseMessage<Post>
            {
               IsSuccess = false,
               Message = response.ReasonPhrase
            };
         }
         catch (Exception e)
         {
            return new ResponseMessage<Post>
            {
               IsSuccess = true,
               Message = e.Message
            };
         }
      }
   }
}