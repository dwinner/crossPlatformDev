using System;
using System.Collections.Generic;

namespace PaperBoy.Models.News
{
   public class NewsInformation
   {
      public string Title { get; set; }
      public string Description { get; set; }
      public string ImageUrl { get; set; }
      public DateTime CreatedDate { get; set; }
   }

   public class Sort
   {
      public string Name { get; set; }
      public string Id { get; set; }
      public bool IsSelected { get; set; }
      public string Url { get; set; }
   }

   public class Thumbnail
   {
      public string ContentUrl { get; set; }
      public int Width { get; set; }
      public int Height { get; set; }
   }

   public class Image
   {
      public Thumbnail Thumbnail { get; set; }
   }

   public class About
   {
      public string ReadLink { get; set; }
      public string Name { get; set; }
   }

   public class Provider
   {
      public string Type { get; set; }
      public string Name { get; set; }
   }

   public class Mention
   {
      public string Name { get; set; }
   }

   public class Value
   {
      public string Name { get; set; }
      public string Url { get; set; }
      public Image Image { get; set; }
      public string Description { get; set; }
      public IList<About> About { get; set; }
      public IList<Provider> Provider { get; set; }
      public DateTime DatePublished { get; set; }
      public string Category { get; set; }
      public IList<Mention> Mentions { get; set; }
   }

   public class NewsResult
   {
      public string Type { get; set; }
      public string ReadLink { get; set; }
      public int TotalEstimatedMatches { get; set; }
      public IList<Sort> Sort { get; set; }
      public IList<Value> Value { get; set; }
   }
}