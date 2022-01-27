using System.Collections.Generic;
using Newtonsoft.Json;

namespace Locator.Common.WebServices.GeocodingWebServiceController.Contracts
{
   /// <summary>
   ///    AccountContract
   /// </summary>
   [JsonObject(MemberSerialization.OptIn)]
   public sealed class GeocodingContract
   {
      /// <summary>
      ///    Gets or sets the results.
      /// </summary>
      /// <value>The results.</value>
      [JsonProperty("results")]
      public List<GeocodingResultContract> Results { get; set; }

      /// <summary>
      ///    Gets or sets the status.
      /// </summary>
      /// <value>The status.</value>
      [JsonProperty("status")]
      public string Status { get; set; }
   }
}