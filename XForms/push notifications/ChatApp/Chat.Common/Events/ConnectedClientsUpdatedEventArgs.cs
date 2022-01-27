using System;
using System.Collections.Generic;
using Chat.Common.Model;

namespace Chat.Common.Events
{
   /// <summary>
   ///    Connected clients updated event arguments.
   /// </summary>
   public class ConnectedClientsUpdatedEventArgs : EventArgs
   {
      /// <summary>
      ///    Initializes a new instance of the <see cref="ConnectedClientsUpdatedEventArgs" /> class.
      /// </summary>
      /// <param name="connectedClients">Connected clients.</param>
      public ConnectedClientsUpdatedEventArgs(IEnumerable<Client> connectedClients)
      {
         ConnectedClients = new List<Client>();

         foreach (var client in connectedClients)
         {
            ConnectedClients.Add(client);
         }
      }

      /// <summary>
      ///    Gets the connected clients.
      /// </summary>
      /// <value>The connected clients.</value>
      public IList<Client> ConnectedClients { get; }
   }
}