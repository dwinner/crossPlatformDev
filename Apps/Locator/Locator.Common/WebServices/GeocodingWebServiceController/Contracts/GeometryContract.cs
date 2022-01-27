using Newtonsoft.Json;

namespace Locator.Common.WebServices.GeocodingWebServiceController.Contracts
{
   /// <summary>
   ///    Geometry contract.
   /// </summary>
   [JsonObject(MemberSerialization.OptIn)]
   public sealed class GeometryContract
   {
      /// <summary>
      ///    Gets or sets the location.
      /// </summary>
      /// <value>The location.</value>
      [JsonProperty("location")]
      public LocationContract Location { get; set; }

      /// <summary>
      ///    Gets or sets the type of the location.
      /// </summary>
      /// <value>The type of the location.</value>
      [JsonProperty("location_type")]
      public string LocationType { get; set; }

      /// <summary>
      ///    Gets or sets the viewport.
      /// </summary>
      /// <value>The viewport.</value>
      [JsonProperty("viewport")]
      public ViewportContract Viewport { get; set; }
   }
}