using System;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Xaml.Controls;
using PaperBoy.Interfaces;
using PaperBoy.UWP.DependencyServices;
using Xamarin.Forms;

[assembly: Dependency(typeof(TextSpeecher))]

namespace PaperBoy.UWP.DependencyServices
{
   public class TextSpeecher : ITextSpeecher
   {
      public async void Speak(string text)
      {
         var mediaElement = new MediaElement();
         var synth = new SpeechSynthesizer();
         var stream = await synth.SynthesizeTextToStreamAsync(text);

         mediaElement.SetSource(stream, stream.ContentType);
         mediaElement.Play();
      }
   }
}