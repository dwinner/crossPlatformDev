using Newtonsoft.Json;

namespace IslandMenu.App.Models
{
   /// <summary>
   ///    Restaurant menu item
   /// </summary>
   [JsonObject]
   public class RestaurantMenuItem
   {
      /// <summary>
      ///    Name of the menu item
      /// </summary>
      [JsonProperty("Name")]
      public string Name { get; set; }

      /// <summary>
      ///    Dish category
      /// </summary>
      [JsonProperty("CategoryId")]
      public int CategoryId { get; set; }

      /// <summary>
      ///    Locale code for this translation
      /// </summary>
      [JsonProperty("Language")]
      public string Language { get; set; }

      /// <summary>
      ///    Description of what the menu item is
      /// </summary>
      [JsonProperty("Description")]
      public string Description { get; set; }

      /// <summary>
      ///    Local price of the item in euros (€)
      /// </summary>
      [JsonProperty("PriceInEuros")]
      public decimal PriceInEuros { get; set; }
   }
}