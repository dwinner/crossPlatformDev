using Newtonsoft.Json;

namespace Locator.Common.WebServices.GeocodingWebServiceController.Contracts
{
   /// <summary>
   ///    Northeast contract.
   /// </summary>
   [JsonObject(MemberSerialization.OptIn)]
   public sealed class NortheastContract
   {
      /// <summary>
      ///    Gets or sets the lat.
      /// </summary>
      /// <value>The lat.</value>
      [JsonProperty("lat")]
      public double Lat { get; set; }

      /// <summary>
      ///    Gets or sets the lng.
      /// </summary>
      /// <value>The lng.</value>
      [JsonProperty("lng")]
      public double Lng { get; set; }
   }
}