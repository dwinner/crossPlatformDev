namespace StockList.WebApi.Models
{
   public class StockItem
   {
      public int Id { get; set; }

      public string Name { get; set; }

      public string Category { get; set; }

      public decimal Price { get; set; }
   }
}