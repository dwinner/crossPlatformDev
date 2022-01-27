using System;
using System.Diagnostics;
using Android.App;
using Android.Media;
using AudioPlayerApp.Core.Sound;

namespace AudioPlayerApp.Droid.Sound
{
   public sealed class AndroidSoundHandlerImpl : ISoundHandler
   {
      private MediaPlayer _mediaPlayer;

      /// <inheritdoc />
      public bool IsPlaying { get; set; }

      /// <inheritdoc />
      public void Load()
      {
         try
         {
            _mediaPlayer = new MediaPlayer();
#pragma warning disable 618
            _mediaPlayer.SetAudioStreamType(Stream.Music);
#pragma warning restore 618

            var descriptor = Application.Context.Assets.OpenFd("Moby - The Only Thing.mp3");
            _mediaPlayer.SetDataSource(descriptor.FileDescriptor, descriptor.StartOffset, descriptor.Length);

            _mediaPlayer.Prepare();
            _mediaPlayer.SetVolume(1f, 1f);
         }
         catch (Exception e)
         {
            Debug.WriteLine(e);
         }
      }

      /// <inheritdoc />
      public void PlayPause()
      {
         if (_mediaPlayer != null)
         {
            if (IsPlaying)
            {
               _mediaPlayer.Pause();
            }
            else
            {
               _mediaPlayer.Start();
            }

            IsPlaying = !IsPlaying;
         }
      }

      /// <inheritdoc />
      public void Stop()
      {
         if (_mediaPlayer != null)
         {
            _mediaPlayer.Stop();
            _mediaPlayer.Reset();
         }
      }

      /// <inheritdoc />
      public double Duration() => (double?) _mediaPlayer?.Duration / 1000 ?? 0;

      /// <inheritdoc />
      public void SetPosition(double value) => _mediaPlayer?.SeekTo((int) value * 1000);

      /// <inheritdoc />
      public double CurrentPosition() => (double?) _mediaPlayer?.CurrentPosition / 1000 ?? 0;

      /// <inheritdoc />
      public void Forward()
      {
         if (_mediaPlayer != null)
         {
            IsPlaying = false;
            _mediaPlayer.Pause();
            _mediaPlayer.SeekTo(_mediaPlayer.Duration);
         }
      }

      /// <inheritdoc />
      public void Rewind()
      {
         if (_mediaPlayer != null)
         {
            IsPlaying = false;
            _mediaPlayer.Pause();
            _mediaPlayer.SeekTo(0);
         }
      }
   }
}