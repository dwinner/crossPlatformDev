using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;

namespace Chat.ServiceAccess.SignalR
{
   /// <summary>
   ///    Signal RC lient.
   /// </summary>
   public class SignalRClient
   {
      /// <summary>
      ///    Initializes a new instance of the <see cref="SignalRClient" /> class.
      /// </summary>
      public SignalRClient()
      {
         _connection = new HubConnection("http://10.0.0.128:52786/");
         _proxy = _connection.CreateHubProxy("ChatHub");
      }

      /// <summary>
      ///    Occurs when on data received.
      /// </summary>
      public event EventHandler<Tuple<string, string>> OnDataReceived;

      /// <summary>
      ///    The connection.
      /// </summary>
      private readonly HubConnection _connection;

      /// <summary>
      ///    The proxy.
      /// </summary>
      private readonly IHubProxy _proxy;

      /// <summary>
      ///    Connect the specified accessToken.
      /// </summary>
      /// <param name="accessToken">Access token.</param>
      public async Task<bool> ConnectAsync(string accessToken)
      {
         try
         {
            _connection.Headers.Add("Authorization", $"Bearer {accessToken}");

            await _connection.Start().ConfigureAwait(true);

            _proxy.On<string, string>("displayMessage", (id, data) => OnDataReceived?.Invoke(this, new Tuple<string, string>(id, data)));

            return true;
         }
         catch (Exception e)
         {
            Console.WriteLine(e);
         }

         return false;
      }

      /// <summary>
      ///    Disconnect this instance.
      /// </summary>
      public void Disconnect()
      {
         _connection.Stop();
         _connection.Dispose();
      }

      /// <summary>
      ///    Sends the message to client.
      /// </summary>
      /// <returns>The message to client.</returns>
      /// <param name="user">User.</param>
      /// <param name="message">Message.</param>
      public Task SendMessageToClientAsync(string user, string message) => _proxy.Invoke("Send", message, user);
   }
}