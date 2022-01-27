using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MobileClient
{
   public class FriendsService
   {
      private const string JsonMediaType = "application/json";
      private readonly string _endPointUrl;

      public FriendsService(string endPointUrl) => _endPointUrl = endPointUrl;

      private static HttpClient Client
      {
         get
         {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", JsonMediaType);
            return client;
         }
      }

      public async Task<IEnumerable<Friend>> GetAsync()
      {
         var client = Client;
         var result = await client.GetStringAsync(_endPointUrl).ConfigureAwait(false);
         return JsonConvert.DeserializeObject<IEnumerable<Friend>>(result);
      }

      public async Task<Friend> AddAsync(Friend friend)
      {
         var client = Client;
         var response = await client.PostAsync(_endPointUrl,
            new StringContent(JsonConvert.SerializeObject(friend), Encoding.UTF8, JsonMediaType)).ConfigureAwait(false);
         return response.StatusCode != HttpStatusCode.OK
            ? null
            : JsonConvert.DeserializeObject<Friend>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
      }

      public async Task<Friend> UpdateAsync(Friend friend)
      {
         var client = Client;
         var response = await client.PutAsync($"{_endPointUrl}/{friend.Id}",
            new StringContent(JsonConvert.SerializeObject(friend), Encoding.UTF8, JsonMediaType)).ConfigureAwait(false);
         return response.StatusCode != HttpStatusCode.OK
            ? null
            : JsonConvert.DeserializeObject<Friend>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
      }

      public async Task<Friend> DeleteAsync(int friendId)
      {
         var client = Client;
         var response = await client.DeleteAsync($"{_endPointUrl}/{friendId}").ConfigureAwait(false);
         return response.StatusCode != HttpStatusCode.OK
            ? null
            : JsonConvert.DeserializeObject<Friend>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
      }
   }
}