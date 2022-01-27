using AVFoundation;
using SpeechTalk.App.Services;

namespace SpeechTalk.App.iOS.Services
{
   public class IosTextToSpeechImpl : ITextToSpeech
   {
      public void Speak(string aMessage)
      {
         var speechSynthesizer = new AVSpeechSynthesizer();
         var speechUtterance = new AVSpeechUtterance(aMessage)
         {
            Rate = AVSpeechUtterance.MaximumSpeechRate / 4,
            Voice = AVSpeechSynthesisVoice.FromLanguage("en-US"),
            Volume = 0.5f,
            PitchMultiplier = 1.0f
         };

         speechSynthesizer.SpeakUtterance(speechUtterance);
      }
   }
}