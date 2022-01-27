using Android.Speech.Tts;
using Java.Util;
using SpeechTalk.App.Services;
using Xamarin.Forms;
using JavaObj = Java.Lang.Object;

namespace SpeechTalk.App.Droid.Services
{
   public class DroidTextToSpeechImpl : JavaObj, ITextToSpeech, TextToSpeech.IOnInitListener
   {
      private TextToSpeech _speaker;
      private string _toSpeak;

      public void OnInit(OperationResult status)
      {
         if (status == OperationResult.Success)
         {
            _speaker.Speak(_toSpeak, QueueMode.Flush, null, string.Empty);
         }
      }

      public void Speak(string aMessage)
      {
#pragma warning disable 618
         var context = Forms.Context;
#pragma warning restore 618
         _toSpeak = aMessage;

         if (_speaker == null)
         {
            _speaker = new TextToSpeech(context, this);
            _speaker.SetLanguage(Locale.Us);
         }
         else
         {
            _speaker.Speak(_toSpeak, QueueMode.Flush, null, string.Empty);
         }
      }
   }
}