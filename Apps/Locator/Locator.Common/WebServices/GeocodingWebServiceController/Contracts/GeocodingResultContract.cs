using System.Collections.Generic;
using Newtonsoft.Json;

namespace Locator.Common.WebServices.GeocodingWebServiceController.Contracts
{
   /// <summary>
   ///    Geocoding result contract.
   /// </summary>
   [JsonObject(MemberSerialization.OptIn)]
   public sealed class GeocodingResultContract
   {
      /// <summary>
      ///    Gets or sets the address components.
      /// </summary>
      /// <value>The address components.</value>
      [JsonProperty("address_components")]
      public List<AddressComponentContract> AddressComponents { get; set; }

      /// <summary>
      ///    Gets or sets the formatted address.
      /// </summary>
      /// <value>The formatted address.</value>
      [JsonProperty("formatted_address")]
      public string FormattedAddress { get; set; }

      /// <summary>
      ///    Gets or sets the geometry.
      /// </summary>
      /// <value>The geometry.</value>
      [JsonProperty("geometry")]
      public GeometryContract Geometry { get; set; }

      /// <summary>
      ///    Gets or sets the place identifier.
      /// </summary>
      /// <value>The place identifier.</value>
      [JsonProperty("place_id")]
      public string PlaceId { get; set; }

      /// <summary>
      ///    Gets or sets the types.
      /// </summary>
      /// <value>The types.</value>
      [JsonProperty("types")]
      public List<string> Types { get; set; }
   }
}