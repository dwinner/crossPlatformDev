using System.Collections.Generic;
using Newtonsoft.Json;

namespace Locator.Common.WebServices.GeocodingWebServiceController.Contracts
{
   /// <summary>
   ///    Address component contract.
   /// </summary>
   [JsonObject(MemberSerialization.OptIn)]
   public sealed class AddressComponentContract
   {
      /// <summary>
      ///    Gets or sets the long name.
      /// </summary>
      /// <value>The long name.</value>
      [JsonProperty("long_name")]
      public string LongName { get; set; }

      /// <summary>
      ///    Gets or sets the short name.
      /// </summary>
      /// <value>The short name.</value>
      [JsonProperty("short_name")]
      public string ShortName { get; set; }

      /// <summary>
      ///    Gets or sets the types.
      /// </summary>
      /// <value>The types.</value>
      [JsonProperty("types")]
      public List<string> Types { get; set; }
   }
}