using MvvmCrossDemo.Core.ViewModels;
using Newtonsoft.Json;

namespace MvvmCrossDemo.Core.Models
{
   public class Post
   {
      [JsonProperty("userId")]
      public int UserId { get; set; }

      [JsonProperty("id")]
      public int Id { get; set; }

      [JsonProperty("title")]
      public string Title { get; set; }

      [JsonProperty("body")]
      public string Body { get; set; }

      public static implicit operator PostViewModel(Post post) =>
         new PostViewModel
         {
            Body = post.Body,
            Id = post.Id,
            Title = post.Title,
            UserId = post.UserId
         };

      public static explicit operator Post(PostViewModel postVm) =>
         new Post
         {
            Body = postVm.Body,
            Id = postVm.Id,
            UserId = postVm.UserId,
            Title = postVm.Title
         };
   }
}