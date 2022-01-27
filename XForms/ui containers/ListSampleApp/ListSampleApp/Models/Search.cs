using System;

namespace ListSampleApp.Models
{
   public class Search
   {
      public int Id { get; set; }

      public string Location { get; set; }

      public DateTime CheckIn { get; set; }

      public DateTime CheckOut { get; set; }

      public string Period => $"{CheckIn:MMM d, yyyy} - {CheckOut:MMM d, yyyy}";
   }
}