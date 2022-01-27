using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Chat.ServiceAccess.Web.Contracts;
using Newtonsoft.Json;

namespace Chat.ServiceAccess.Web
{
   /// <summary>
   ///    Web API access.
   /// </summary>
   public class WebApiAccess
   {
      /// <summary>
      ///    The base address.
      /// </summary>
      private readonly string _baseAddress = "http://10.0.0.128:52786/";

      /// <summary>
      ///    Logins the async.
      /// </summary>
      /// <returns>The async.</returns>
      /// <param name="name">Name.</param>
      /// <param name="password">Password.</param>
      /// <param name="cancellationToken">Cancellation token.</param>
      public async Task<bool> LoginAsync(string name, string password, CancellationToken? cancellationToken = null)
      {
         var httpMessage = new HttpRequestMessage(HttpMethod.Post, new Uri($"{_baseAddress}api/Account/Login"))
         {
            Content = new StringContent($"Username={name}&Password={password}", Encoding.UTF8,
               "application/x-www-form-urlencoded")
         };

         var client = new HttpClient();

         var response = await client.SendAsync(httpMessage, cancellationToken ?? new CancellationToken(false)).ConfigureAwait(true);

         switch (response.StatusCode)
         {
            case HttpStatusCode.NotFound:
               throw new Exception(string.Empty);
         }

         var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(true);

         bool.TryParse(responseContent, out var loginSuccess);
         return loginSuccess;
      }

      /// <summary>
      ///    Registers the async.
      /// </summary>
      /// <returns>The async.</returns>
      /// <param name="name">Name.</param>
      /// <param name="password">Password.</param>
      /// <param name="cancellationToken">Cancellation token.</param>
      public async Task<bool> RegisterAsync(string name, string password, CancellationToken? cancellationToken = null)
      {
         var httpMessage = new HttpRequestMessage(HttpMethod.Post, new Uri($"{_baseAddress}api/Account/Register"))
         {
            Content = new StringContent($"Username={name}&Password={password}", Encoding.UTF8,
               "application/x-www-form-urlencoded")
         };

         var client = new HttpClient();

         var response = await client.SendAsync(httpMessage, cancellationToken ?? new CancellationToken(false)).ConfigureAwait(true);

         return response.StatusCode == HttpStatusCode.OK;
      }

      /// <summary>
      ///    Gets the token async.
      /// </summary>
      /// <returns>The token async.</returns>
      /// <param name="name">Name.</param>
      /// <param name="password">Password.</param>
      /// <param name="cancellationToken">Cancellation token.</param>
      public async Task<TokenContract> GetTokenAsync(string name, string password,
         CancellationToken? cancellationToken = null)
      {
         var httpMessage = new HttpRequestMessage(HttpMethod.Post, new Uri(_baseAddress + "token"))
         {
            Content = new StringContent($"Username={name}&Password={password}&grant_type=password",
               Encoding.UTF8, "application/x-www-form-urlencoded")
         };

         var client = new HttpClient();

         var response = await client.SendAsync(httpMessage, cancellationToken ?? new CancellationToken(false)).ConfigureAwait(true);

         switch (response.StatusCode)
         {
            case HttpStatusCode.NotFound:
               throw new Exception(string.Empty);
         }

         var tokenJson = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
         return JsonConvert.DeserializeObject<TokenContract>(tokenJson);
      }

      /// <summary>
      ///    Gets all connected users async.
      /// </summary>
      /// <returns>The all connected users async.</returns>
      /// <param name="cancellationToken">Cancellation token.</param>
      public async Task<IEnumerable<string>> GetAllConnectedUsersAsync(CancellationToken? cancellationToken = null)
      {
         var httpMessage =
            new HttpRequestMessage(HttpMethod.Get, new Uri($"{_baseAddress}api/Account/GetAllConnectedUsers"));

         var client = new HttpClient();

         var response = await client.SendAsync(httpMessage, cancellationToken ?? new CancellationToken(false)).ConfigureAwait(true);

         switch (response.StatusCode)
         {
            case HttpStatusCode.NotFound:
               throw new Exception(string.Empty);
         }

         var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(true);

         return JsonConvert.DeserializeObject<IEnumerable<string>>(responseContent);
      }
   }
}