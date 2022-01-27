using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Reactive.Linq;
using System.Threading;
using Newtonsoft.Json;
using StockList.Core.Resources;

namespace StockList.Core.WebServices
{
   public class StocklistWebServiceControllerImpl : IStocklistWebServiceController
   {
      private const string ErrorMessage = "Response error";
      private static readonly CancellationToken _CancellationToken = CancellationToken.None;
      private readonly HttpClientHandler _clientHandler;

      public StocklistWebServiceControllerImpl(HttpClientHandler clientHandler) => _clientHandler = clientHandler;

      public IObservable<List<StockItemContract>> GetAllStockItems()
      {
         var authClient = new HttpClient(_clientHandler);
         var message = new HttpRequestMessage(HttpMethod.Get, new Uri(Config.ApiAllItems));

         return Observable.FromAsync(() => authClient.SendAsync(message, _CancellationToken))
            .SelectMany(responseMessage =>
            {
               if (responseMessage.StatusCode != HttpStatusCode.OK)
               {
                  throw new Exception(ErrorMessage);
               }

               return responseMessage.Content.ReadAsStringAsync();
            })
            .Select(JsonConvert.DeserializeObject<List<StockItemContract>>);
      }

      public IObservable<StockItemContract> GetStockItem(int id)
      {
         var authClient = new HttpClient(_clientHandler);
         var message = new HttpRequestMessage(HttpMethod.Get, new Uri(string.Format(Config.GetStockItem, id)));

         return Observable.FromAsync(() => authClient.SendAsync(message, _CancellationToken))
            .SelectMany(responseMessage =>
            {
               if (responseMessage.StatusCode != HttpStatusCode.OK)
               {
                  throw new Exception(ErrorMessage);
               }

               return responseMessage.Content.ReadAsStringAsync();
            })
            .Select(JsonConvert.DeserializeObject<StockItemContract>);
      }
   }
}