using System;
using Locator.Common.WebServices.GeocodingWebServiceController.Contracts;

namespace Locator.Common.WebServices.GeocodingWebServiceController
{
   /// <summary>
   ///    The geocoding web service controller interface.
   /// </summary>
   public interface IGeocodingWebServiceController
   {
      /// <summary>
      ///    Gets the geocode from address async.
      /// </summary>
      /// <returns>The geocode from address async.</returns>
      /// <param name="address">Address.</param>
      /// <param name="city">City.</param>
      /// <param name="state">State.</param>
      IObservable<GeocodingContract> GetGeocodeFromAddressAsync(string address, string city, string state);
   }
}