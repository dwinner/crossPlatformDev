using System;
using System.Net;
using System.Net.Http;
using System.Reactive.Linq;
using System.Threading;
using Locator.Common.Resources;
using Locator.Common.WebServices.GeocodingWebServiceController.Contracts;
using Newtonsoft.Json;

// ReSharper disable UnthrowableException
// ReSharper disable ThrowingSystemException

namespace Locator.Common.WebServices.GeocodingWebServiceController
{
   /// <summary>
   ///    Geocoding web service controller.
   /// </summary>
   public sealed class GeocodingWebServiceController : IGeocodingWebServiceController
   {
      /// <summary>
      ///    The client handler.
      /// </summary>
      private readonly HttpClientHandler _clientHandler;

      public GeocodingWebServiceController(HttpClientHandler clientHandler) => _clientHandler = clientHandler;

      /// <summary>
      ///    Gets the geocode from address async.
      /// </summary>
      /// <returns>The geocode from address async.</returns>
      /// <param name="address">Address.</param>
      /// <param name="city">City.</param>
      /// <param name="state">State.</param>
      public IObservable<GeocodingContract> GetGeocodeFromAddressAsync(string address, string city, string state)
      {
         var authClient = new HttpClient(_clientHandler);

         var message = new HttpRequestMessage(HttpMethod.Get,
            new Uri(string.Format(ApiConfig.GoogleMapsUrl, address, city, state)));

         return Observable.FromAsync(() => authClient.SendAsync(message, new CancellationToken(false)))
            .SelectMany(response =>
            {
               if (response.StatusCode != HttpStatusCode.OK)
               {
                  throw new Exception("Respone error");
               }

               return response.Content.ReadAsStringAsync();
            })
            .Select(JsonConvert.DeserializeObject<GeocodingContract>);
      }
   }
}