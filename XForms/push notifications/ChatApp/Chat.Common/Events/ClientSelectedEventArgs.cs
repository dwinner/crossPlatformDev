using System;
using Chat.Common.Model;

namespace Chat.Common.Events
{
   /// <summary>
   ///    Client selected event arguments.
   /// </summary>
   public class ClientSelectedEventArgs : EventArgs
   {
      /// <summary>
      ///    Initializes a new instance of the <see cref="ClientSelectedEventArgs" /> class.
      /// </summary>
      /// <param name="client">Client.</param>
      public ClientSelectedEventArgs(Client client) => Client = client;

      /// <summary>
      ///    Gets the client.
      /// </summary>
      /// <value>The client.</value>
      public Client Client { get; }
   }
}