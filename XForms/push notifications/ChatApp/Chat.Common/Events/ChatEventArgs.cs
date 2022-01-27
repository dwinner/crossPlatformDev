using System;

namespace Chat.Common.Events
{
   /// <summary>
   ///    Chat event arguments.
   /// </summary>
   public class ChatEventArgs : EventArgs
   {
      /// <summary>
      ///    Initializes a new instance of the <see cref="ChatEventArgs" /> class.
      /// </summary>
      /// <param name="message">Message.</param>
      public ChatEventArgs(string message) => Message = message;

      /// <summary>
      ///    Gets the message.
      /// </summary>
      /// <value>The message.</value>
      public string Message { get; }
   }
}