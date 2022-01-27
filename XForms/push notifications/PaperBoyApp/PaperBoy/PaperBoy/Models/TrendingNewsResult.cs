namespace PaperBoy.Models.Trending
{
   public class TrendingNewsResult
   {
      public string Type { get; set; }
      public Value[] Value { get; set; }
   }

   public class Value
   {
      public string Name { get; set; }
      public Image Image { get; set; }
      public string WebSearchUrl { get; set; }
      public bool IsBreakingNews { get; set; }
      public Query Query { get; set; }
   }

   public class Image
   {
      public string Url { get; set; }
      public Provider[] Provider { get; set; }
   }

   public class Provider
   {
      public string Type { get; set; }
      public string Name { get; set; }
   }

   public class Query
   {
      public string Text { get; set; }
   }
}