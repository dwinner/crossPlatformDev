using System;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Xaml.Controls;
using SpeechTalk.App.Services;

// ReSharper disable AvoidAsyncVoid
// ReSharper disable AsyncConverter.ConfigureAwaitHighlighting

namespace SpeechTalk.App.UWP.Services
{
   /// <summary>
   ///    Text to speech win phone for UWP
   /// </summary>
   public class WinPhoneTextToSpeechImpl : ITextToSpeech
   {
      /// <inheritdoc />
      public async void Speak(string aMessage)
      {
         var mediaElement = new MediaElement();
         var synthesizer = new SpeechSynthesizer();
         var stream = await synthesizer.SynthesizeTextToStreamAsync(aMessage);
         mediaElement.SetSource(stream, stream.ContentType);
         mediaElement.Play();
      }
   }
}