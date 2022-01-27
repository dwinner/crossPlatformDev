using System;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using AudioPlayerApp.Core.ViewModels;
using AudioPlayerApp.Droid.Controls;

// ReSharper disable BitwiseOperatorOnEnumWithoutFlags

namespace AudioPlayerApp.Droid.Views
{
   [Activity(
      Theme = "@style/AppTheme",
      WindowSoftInputMode = SoftInput.AdjustResize | SoftInput.StateHidden,
      Label = "Audio player")]
   public class AudioPlayerActivity : BaseActivity<AudioPlayerPageViewModel>
   {
      private AudioPlayerPageViewModel _model;
      private ImageButton _playButton;
      private bool _playing;
      private CustomSeekBar _seekBar;

      protected override int ActivityLayoutId => Resource.Layout.audio_payer_page;

      protected override void OnCreate(Bundle bundle)
      {
         base.OnCreate(bundle);

         _seekBar = FindViewById<CustomSeekBar>(Resource.Id.seekBar);
         _seekBar.ValueChanged += HandleValueChanged;

         _playButton = FindViewById<ImageButton>(Resource.Id.PlayButton);
         _playButton.SetColorFilter(Color.White);
         _playButton.Click += HandlePlayClick;

         var rewindButton = FindViewById<ImageButton>(Resource.Id.RewindButton);
         rewindButton.SetColorFilter(Color.White);
         rewindButton.Click += HandleRewindForwardClick;

         var forwardButton = FindViewById<ImageButton>(Resource.Id.ForwardButton);
         forwardButton.SetColorFilter(Color.White);
         forwardButton.Click += HandleRewindForwardClick;

         _model = ViewModel;
      }

      protected override void OnDestroy()
      {
         _model.Dispose();
         base.OnDestroy();
      }

      private void HandleRewindForwardClick(object sender, EventArgs e)
      {
         _playing = false;
         _playButton.SetImageResource(Resource.Drawable.play);
      }

      private void HandlePlayClick(object sender, EventArgs e)
      {
         _playing = !_playing;
         _playButton.SetImageResource(_playing ? Resource.Drawable.pause : Resource.Drawable.play);
      }

      private void HandleValueChanged(object sender, EventArgs e) => _model.UpdateAudioPosition(_seekBar.Progress);
   }
}