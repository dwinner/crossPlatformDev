using System.Collections.Generic;
using Newtonsoft.Json;

namespace IslandMenu.App.Models
{
   /// <summary>
   ///    Restaurant
   /// </summary>
   [JsonObject]
   public class Restaurant
   {
      /// <summary>
      ///    Placeholder for a record ID (not used here)
      /// </summary>
      [JsonProperty("ID")]
      public int Id { get; set; }

      /// <summary>
      ///    Name of the restaurant
      /// </summary>
      [JsonProperty("Name")]
      public string Name { get; set; }

      /// <summary>
      ///    Street address
      /// </summary>
      [JsonProperty("Address1")]
      public string Address1 { get; set; }

      /// <summary>
      ///    Additional address information (not used here)
      /// </summary>
      [JsonProperty("Address2")]
      public string Address2 { get; set; }

      /// <summary>
      ///    Name of the town
      /// </summary>
      [JsonProperty("Town")]
      public string Town { get; set; }

      /// <summary>
      ///    Phone number with full country code
      /// </summary>
      [JsonProperty("PhoneNumber")]
      public string PhoneNumber { get; set; }

      /// <summary>
      ///    Fax number (not used here)
      /// </summary>
      [JsonProperty("Fax")]
      public string Fax { get; set; }

      /// <summary>
      ///    File name or URL of the photo
      /// </summary>
      [JsonProperty("Photo")]
      public string Photo { get; set; }

      /// <summary>
      ///    Collection of menu items for the restaurant
      /// </summary>
      [JsonProperty("Menu")]
      public List<RestaurantMenuItem> Menu { get; set; }
   }
}