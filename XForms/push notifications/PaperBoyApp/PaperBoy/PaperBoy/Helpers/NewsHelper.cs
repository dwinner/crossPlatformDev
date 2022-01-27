using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PaperBoy.Common;
using PaperBoy.Models;
using PaperBoy.Models.News;
using PaperBoy.Models.Trending;

namespace PaperBoy.Helpers
{
   public static class NewsHelper
   {
      public static async Task<List<NewsInformation>> GetTrendingAsync()
      {
         var results = new List<NewsInformation>();

         var searchUrl = "https://api.cognitive.microsoft.com/bing/v7.0/news/trendingtopics?cc=US";

         var client = new HttpClient();
         client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", CoreConstants.NewsSearchApiKey);

         var uri = new Uri(searchUrl);
         var result = await client.GetStringAsync(uri);
         var newsResult = JsonConvert.DeserializeObject<TrendingNewsResult>(result);

         results = (from item in newsResult.Value
            select new NewsInformation
            {
               Title = item.Name,
               Description = item.Query.Text,
               CreatedDate = DateTime.Now,
               ImageUrl = item.Image.Url
            }).ToList();

         return results.Where(w => !string.IsNullOrWhiteSpace(w.ImageUrl)).Take(10).ToList();
      }

      public static async Task<List<NewsInformation>> GetByCategoryAsync(NewsCategoryType newsCategory)
      {
         var results = new List<NewsInformation>();

         var searchUrl = $"https://api.cognitive.microsoft.com/bing/v7.0/news/?mkt=en-US&Category={newsCategory}";

         var client = new HttpClient();
         client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", CoreConstants.NewsSearchApiKey);

         var uri = new Uri(searchUrl);
         var result = await client.GetStringAsync(uri);
         var newsResult = JsonConvert.DeserializeObject<NewsResult>(result);

         results = (from item in newsResult.Value
            select new NewsInformation
            {
               Title = item.Name,
               Description = item.Description,
               ImageUrl = item.Image?.Thumbnail?.ContentUrl,
               CreatedDate = item.DatePublished
            }).ToList();
         return results.Where(w => !string.IsNullOrWhiteSpace(w.ImageUrl)).Take(10).ToList();
      }

      public static async Task<List<NewsInformation>> GetSearchAsync(string searchQuery)
      {
         var results = new List<NewsInformation>();
         var searchUrl = $"https://api.cognitive.microsoft.com/bing/v7.0/news/search?q={searchQuery}&mkt=en-US";

         var client = new HttpClient();
         client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", CoreConstants.NewsSearchApiKey);

         var url = new Uri(searchUrl);
         var result = await client.GetStringAsync(url);

         var searchResult = JsonConvert.DeserializeObject<NewsResult>(result);
         results = (from item in searchResult.Value
            select new NewsInformation
            {
               Title = item.Name,
               Description = item.Description,
               ImageUrl = item.Image?.Thumbnail?.ContentUrl,
               CreatedDate = item.DatePublished
            }).ToList();
         return results.Where(w => !string.IsNullOrWhiteSpace(w.ImageUrl)).Take(10).ToList();
      }
   }
}