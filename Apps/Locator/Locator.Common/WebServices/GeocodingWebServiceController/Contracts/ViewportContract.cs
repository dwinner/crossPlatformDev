using Newtonsoft.Json;

namespace Locator.Common.WebServices.GeocodingWebServiceController.Contracts
{
   /// <summary>
   ///    Viewport contract.
   /// </summary>
   [JsonObject(MemberSerialization.OptIn)]
   public sealed class ViewportContract
   {
      /// <summary>
      ///    Gets or sets the northeast.
      /// </summary>
      /// <value>The northeast.</value>
      [JsonProperty("northeast")]
      public NortheastContract Northeast { get; set; }

      /// <summary>
      ///    Gets or sets the southwest.
      /// </summary>
      /// <value>The southwest.</value>
      [JsonProperty("southwest")]
      public SouthwestContract Southwest { get; set; }
   }
}