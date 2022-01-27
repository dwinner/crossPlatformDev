using AudioPlayerApp.Core.Sound;
using AVFoundation;
using Foundation;

namespace AudioPlayerApp.iOS.Sound
{
   public class IosSoundHandlerImpl : ISoundHandler
   {
      private AVAudioPlayer _audioPlayer; // The audio player

      /// <summary>
      ///    Gets or sets the is playing.
      /// </summary>
      /// <value>The is playing.</value>
      public bool IsPlaying { get; set; }

      /// <summary>
      ///    Load the audio.
      /// </summary>
      public void Load() => _audioPlayer = AVAudioPlayer.FromUrl(NSUrl.FromFilename("Moby - The Only Thing.mp3"));

      /// <summary>
      ///    Play/Pauses the audio.
      /// </summary>
      /// <returns>The pause.</returns>
      public void PlayPause()
      {
         if (_audioPlayer != null)
         {
            if (IsPlaying)
            {
               _audioPlayer.Stop();
            }
            else
            {
               _audioPlayer.Play();
            }

            IsPlaying = !IsPlaying;
         }
      }

      /// <summary>
      ///    Stop this audio.
      /// </summary>
      public void Stop() => _audioPlayer?.Stop();

      /// <summary>
      ///    Returns the audio duration.
      /// </summary>
      public double Duration() => _audioPlayer?.Duration ?? 0;

      /// <summary>
      ///    Sets the audio position.
      /// </summary>
      /// <returns>The position.</returns>
      /// <param name="value">Value.</param>
      public void SetPosition(double value)
      {
         if (_audioPlayer != null)
         {
            _audioPlayer.CurrentTime = value;
         }
      }

      /// <summary>
      ///    Returns current position of audio.
      /// </summary>
      /// <returns>The position.</returns>
      public double CurrentPosition() => _audioPlayer?.CurrentTime ?? 0;

      /// <summary>
      ///    Fast forwards audio position.
      /// </summary>
      public void Forward()
      {
         if (_audioPlayer != null)
         {
            IsPlaying = false;

            _audioPlayer.Stop();
            _audioPlayer.CurrentTime = _audioPlayer.Duration;
         }
      }

      /// <summary>
      ///    Rewind the audio position.
      /// </summary>
      public void Rewind()
      {
         if (_audioPlayer != null)
         {
            IsPlaying = false;

            _audioPlayer.Stop();
            _audioPlayer.CurrentTime = 0;
         }
      }
   }
}