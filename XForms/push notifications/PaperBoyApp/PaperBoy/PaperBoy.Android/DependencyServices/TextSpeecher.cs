using System;
using Android.OS;
using Android.Runtime;
using Android.Speech.Tts;
using PaperBoy.Droid.DependencyServices;
using PaperBoy.Interfaces;
using Xamarin.Forms;
using JavaObject = Java.Lang.Object;

#pragma warning disable 618

[assembly: Dependency(typeof(TextSpeecher))]

namespace PaperBoy.Droid.DependencyServices
{
   public class TextSpeecher : JavaObject, ITextSpeecher, TextToSpeech.IOnInitListener
   {
      private TextToSpeech _speaker;
      private string _toSpeak;

      public void OnInit([GeneratedEnum] OperationResult status)
      {
         if (status.Equals(OperationResult.Success))
         {
            (Build.VERSION.Release.StartsWith("4")
               ? (Action) (() => _speaker.Speak(_toSpeak, QueueMode.Flush, null))
               : () => _speaker.Speak(_toSpeak, QueueMode.Flush, null, null))();
         }
      }

      public void Speak(string text)
      {
         var ctx = Forms.Context;

         _toSpeak = text;
         if (_speaker == null)
         {
            _speaker = new TextToSpeech(ctx, this);
         }
         else
         {
            (Build.VERSION.Release.StartsWith("4")
               ? (Action)(() => _speaker.Speak(_toSpeak, QueueMode.Flush, null))
               : () => _speaker.Speak(_toSpeak, QueueMode.Flush, null, null))();
         }
      }
   }
}