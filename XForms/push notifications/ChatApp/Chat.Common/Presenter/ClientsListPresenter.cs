using System;
using System.Collections.Generic;
using System.Linq;
using Chat.Common.Events;
using Chat.Common.Model;

namespace Chat.Common.Presenter
{
   /// <summary>
   ///    Clients list presenter.
   /// </summary>
   public class ClientsListPresenter : BasePresenter
   {
      /// <summary>
      ///    The view.
      /// </summary>
      private IClientsListView _view;

      /// <summary>
      ///    Initializes a new instance of the <see cref="ClientsListPresenter" /> class.
      /// </summary>
      /// <param name="state">State.</param>
      /// <param name="navigationService">Navigation service.</param>
      /// <param name="accessToken">Access token.</param>
      public ClientsListPresenter(ApplicationState state, INavigationService navigationService,
         string accessToken)
      {
         _navigationService = navigationService;
         _state = state;
         _state.AccessToken = accessToken;
         InitSignalRAsync(accessToken).ConfigureAwait(false);
      }

      /// <summary>
      ///    Sets the view.
      /// </summary>
      /// <returns>The view.</returns>
      /// <param name="view">View.</param>
      public void SetView(IClientsListView view)
      {
         _view = view;

         _signalRClient.OnDataReceived -= HandleSignalRDataReceived;
         _signalRClient.OnDataReceived += HandleSignalRDataReceived;

         _view.ClientSelected -= HandleClientSelected;
         _view.ClientSelected += HandleClientSelected;

         ConnectedClientsUpdated -= HandleConnectedClientsUpdated;
         ConnectedClientsUpdated += HandleConnectedClientsUpdated;

         GetAllConnectedClientsAsync().ConfigureAwait(false);
      }

      /// <summary>
      ///    Releases the view.
      /// </summary>
      /// <returns>The view.</returns>
      public void ReleaseView()
      {
         _signalRClient.OnDataReceived -= HandleSignalRDataReceived;
      }

      /// <summary>
      ///    Signout this instance.
      /// </summary>
      public void Signout()
      {
         _signalRClient.Disconnect();
         _navigationService.PopPresenter(true);
      }

      /// <summary>
      ///    Handles the client selected.
      /// </summary>
      /// <returns>The client selected.</returns>
      /// <param name="sender">Sender.</param>
      /// <param name="e">E.</param>
      private void HandleClientSelected(object sender, ClientSelectedEventArgs e)
      {
         var presenter = new ChatPresenter(_state, _navigationService, e.Client, _signalRClient);
         _navigationService.PushPresenter(presenter);
      }

      /// <summary>
      ///    Handles the connected clients updated.
      /// </summary>
      /// <returns>The connected clients updated.</returns>
      /// <param name="sender">Sender.</param>
      /// <param name="e">E.</param>
      private void HandleConnectedClientsUpdated(object sender, ConnectedClientsUpdatedEventArgs e)
      {
         _view.NotifyConnectedClientsUpdated(e.ConnectedClients
            .Where(x => !x.Username.ToLower()
               .Contains(_state.Username.ToLower())));
      }

      /// <summary>
      ///    Clients list view.
      /// </summary>
      public interface IClientsListView : IView
      {
         event EventHandler<ClientSelectedEventArgs> ClientSelected;

         void NotifyConnectedClientsUpdated(IEnumerable<Client> clients);
      }
   }
}