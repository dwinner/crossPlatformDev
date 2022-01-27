using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Users.MobileClient.Models;

namespace Users.MobileClient.Helpers
{
   public static class UsersServiceHelper
   {
      private static readonly HttpClient _HttpClient;

      static UsersServiceHelper() =>
         _HttpClient = new HttpClient
         {
            BaseAddress = new Uri("http://jsonplaceholder.typicode.com/users/")
         };

      public static async Task<IEnumerable<User>> GetAsync()
      {
         var response = await _HttpClient.GetAsync(string.Empty).ConfigureAwait(true);
         CheckStatusCode(response.StatusCode);
         var jsonString = await response.Content.ReadAsStringAsync().ConfigureAwait(true);

         return JsonConvert.DeserializeObject<IEnumerable<User>>(jsonString);
      }

      public static async Task DeleteAsync(int userId)
      {
         var response = await _HttpClient.DeleteAsync($"{userId}").ConfigureAwait(true);
         CheckStatusCode(response.StatusCode);
      }

      public static async Task UpdateAsync(User user)
      {
         var userJson = JsonConvert.SerializeObject(user);
         var response = await _HttpClient.PutAsync($"{user.Id}", new StringContent(userJson)).ConfigureAwait(true);
         CheckStatusCode(response.StatusCode);
      }

      public static async Task<User> GetAsync(int userId)
      {
         var response = await _HttpClient.GetAsync($"{userId}").ConfigureAwait(true);
         CheckStatusCode(response.StatusCode);
         var jsonString = await response.Content.ReadAsStringAsync().ConfigureAwait(true);

         return JsonConvert.DeserializeObject<User>(jsonString);
      }

      private static void CheckStatusCode(HttpStatusCode statusCode)
      {
         if (statusCode != HttpStatusCode.OK)
         {
            throw new Exception($"Unexpected status code: {statusCode}");
         }
      }
   }
}