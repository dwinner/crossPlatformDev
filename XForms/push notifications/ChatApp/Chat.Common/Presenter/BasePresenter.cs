using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Chat.Common.Events;
using Chat.Common.Model;
using Chat.ServiceAccess.SignalR;
using Chat.ServiceAccess.Web;
using Newtonsoft.Json;

namespace Chat.Common.Presenter
{
   /// <summary>
   ///    Base presenter.
   /// </summary>
   public abstract class BasePresenter
   {
      /// <summary>
      ///    The signal RE vents.
      /// </summary>
      private readonly IDictionary<string, Action<string>> _signalREvents;

      /// <summary>
      ///    Initializes a new instance of the <see cref="BasePresenter" /> class.
      /// </summary>
      protected BasePresenter()
      {
         _webApiAccess = new WebApiAccess();

         _signalREvents = new Dictionary<string, Action<string>>
         {
            {
               "clients", data =>
               {
                  var list = JsonConvert.DeserializeObject<IEnumerable<string>>(data);
                  ConnectedClientsUpdated?.Invoke(this, new ConnectedClientsUpdatedEventArgs(list.Select(x => new Client
                  {
                     Username = x
                  })));
               }
            },
            {
               "chat", data =>
               {
                  ChatReceived?.Invoke(this, new ChatEventArgs(data));
               }
            }
         };
      }

      /// <summary>
      ///    Handles the signal RD ata received.
      /// </summary>
      /// <returns>The signal RD ata received.</returns>
      /// <param name="sender">Sender.</param>
      /// <param name="e">E.</param>
      protected void HandleSignalRDataReceived(object sender, Tuple<string, string> e)
      {
         var (item1, item2) = e;
         _signalREvents[item1](item2);
      }

      /// <summary>
      ///    The navigation service.
      /// </summary>
      protected INavigationService _navigationService;

      /// <summary>
      ///    The state.
      /// </summary>
      protected ApplicationState _state;

      /// <summary>
      ///    The signal RC lient.
      /// </summary>
      protected SignalRClient _signalRClient;

      /// <summary>
      ///    The web API access.
      /// </summary>
      protected WebApiAccess _webApiAccess;

      /// <summary>
      ///    The access token.
      /// </summary>
      protected string _accessToken;

      /// <summary>
      ///    Occurs when connected clients updated.
      /// </summary>
      public event EventHandler<ConnectedClientsUpdatedEventArgs> ConnectedClientsUpdated;

      /// <summary>
      ///    Occurs when chat received.
      /// </summary>
      public event EventHandler<ChatEventArgs> ChatReceived;

      /// <summary>
      ///    Inits the signal r.
      /// </summary>
      /// <returns>The signal r.</returns>
      /// <param name="accessToken">Access token.</param>
      protected Task InitSignalRAsync(string accessToken)
      {
         _signalRClient = new SignalRClient();
         return _signalRClient.ConnectAsync(accessToken);
      }

      /// <summary>
      ///    Gets all connected clients.
      /// </summary>
      /// <returns>The all connected clients.</returns>
      protected async Task GetAllConnectedClientsAsync()
      {
         var clients = await _webApiAccess.GetAllConnectedUsersAsync(CancellationToken.None).ConfigureAwait(true);
         if (clients != null)
         {
            ConnectedClientsUpdated?.Invoke(this,
               new ConnectedClientsUpdatedEventArgs(clients
                  .Where(x => !x.ToLower().Contains(_state.Username))
                  .Select(x => new Client
                  {
                     Username = x
                  })));
         }
      }
   }
}