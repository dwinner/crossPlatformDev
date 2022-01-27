using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace PaperBoy.Data
{
   public class Favorite
   {
      [JsonProperty(PropertyName = "id")]
      public string Id { get; set; }

      [JsonProperty(PropertyName = "title")]
      public string Title { get; set; }

      [JsonProperty(PropertyName = "description")]
      public string Description { get; set; }

      [JsonProperty(PropertyName = "imageUrl")]
      public string ImageUrl { get; set; }

      [JsonProperty(PropertyName = "articleDate")]
      public DateTime ArticleDate { get; set; }

      [Version]
      public string Version { get; set; }
   }
}