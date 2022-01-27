using System;
using System.Runtime.Serialization;

namespace SpeechTalk.App.UWP
{
   [Serializable]
   public class UwpNagigationException : Exception
   {
      public UwpNagigationException()
      {
      }

      public UwpNagigationException(string message) : base(message)
      {
      }

      public UwpNagigationException(string message, Exception inner) : base(message, inner)
      {
      }

      protected UwpNagigationException(
         SerializationInfo info,
         StreamingContext context) : base(info, context)
      {
      }
   }
}