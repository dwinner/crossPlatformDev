namespace SpeechTalk.App.Services
{
   /// <summary>
   ///    Text to speech.
   /// </summary>
   public interface ITextToSpeech
   {
      /// <summary>
      ///    Speak the specified message.
      /// </summary>
      /// <param name="aMessage">Message.</param>
      void Speak([NotNull] string aMessage);
   }
}