using AVFoundation;
using PaperBoy.Interfaces;
using PaperBoy.iOS.DependencyServices;
using Xamarin.Forms;

[assembly: Dependency(typeof(TextSpeecher))]

namespace PaperBoy.iOS.DependencyServices
{
   public class TextSpeecher : ITextSpeecher
   {
      public void Speak(string text)
      {
         var speechSynthersizer = new AVSpeechSynthesizer();
         var speechUtterance = new AVSpeechUtterance(text)
         {
            Rate = AVSpeechUtterance.MaximumSpeechRate / 4,
            Voice = AVSpeechSynthesisVoice.FromLanguage("en-US"),
            Volume = 0.5f,
            PitchMultiplier = 1.0F
         };
         speechSynthersizer.SpeakUtterance(speechUtterance);
      }
   }
}