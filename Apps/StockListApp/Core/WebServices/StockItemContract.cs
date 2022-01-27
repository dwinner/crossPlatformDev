namespace StockList.Core.WebServices
{
   public sealed class StockItemContract
   {
      public int Id { get; set; }

      public string Name { get; set; }

      public string Category { get; set; }

      public decimal Price { get; set; }
   }
}