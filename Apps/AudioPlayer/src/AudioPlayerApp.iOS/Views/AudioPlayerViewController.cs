using System;
using System.Linq;
using AudioPlayerApp.Core.ViewModels;
using AudioPlayerApp.iOS.Extras;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

namespace AudioPlayerApp.iOS.Views
{
   public class AudioPlayerViewController : MvxViewController<AudioPlayerPageViewModel>
   {
      private AudioPlayerPageViewModel _model; // The model.
      private UIButton _playButton; // The play button
      private bool _playing; // The playing.
      private UISlider _progressSlider; // The progress slider.

      public override void ViewDidLoad()
      {
         base.ViewDidLoad();

         var mainView = new UIView
         {
            TranslatesAutoresizingMaskIntoConstraints = false,
            BackgroundColor = UIColor.White
         };

         var buttonView = new UIView
         {
            TranslatesAutoresizingMaskIntoConstraints = false,
            BackgroundColor = UIColor.Clear
         };

         var imageView = new UIImageView
         {
            TranslatesAutoresizingMaskIntoConstraints = false,
            ContentMode = UIViewContentMode.ScaleAspectFit,
            Image = new UIImage("moby.png")
         };

         var descriptionLabel = new UILabel
         {
            TranslatesAutoresizingMaskIntoConstraints = false,
            TextAlignment = UITextAlignment.Center
         };

         var currentLabel = new UILabel
         {
            TranslatesAutoresizingMaskIntoConstraints = false,
            TextAlignment = UITextAlignment.Left
         };

         var endLabel = new UILabel
         {
            TranslatesAutoresizingMaskIntoConstraints = false,
            TextAlignment = UITextAlignment.Right
         };

         _progressSlider = new UISlider
         {
            TranslatesAutoresizingMaskIntoConstraints = false,
            MinValue = 0
         };

         _progressSlider.ValueChanged += ProgressSliderValueChanged;

         _playButton = new UIButton(UIButtonType.Custom)
         {
            TranslatesAutoresizingMaskIntoConstraints = false
         };
         _playButton.TouchUpInside += HandlePlayButton;
         _playButton.SetImage(UIImage.FromFile("play.png"), UIControlState.Normal);

         var rewindButton = new UIButton(UIButtonType.Custom)
         {
            TranslatesAutoresizingMaskIntoConstraints = false
         };
         rewindButton.TouchUpInside += HandleRewindForwardButton;
         rewindButton.SetImage(UIImage.FromFile("rewind.png"), UIControlState.Normal);

         var fastForwardButton = new UIButton(UIButtonType.Custom)
         {
            TranslatesAutoresizingMaskIntoConstraints = false
         };
         fastForwardButton.TouchUpInside += HandleRewindForwardButton;
         fastForwardButton.SetImage(UIImage.FromFile("fast_forward.png"), UIControlState.Normal);

         const string mainViewMark = "mainView";
         const string buttonViewMark = "buttonView";
         const string imageViewMark = "imageView";
         const string descriptionLabelMark = "descriptionLabel";
         const string currentLabelMark = "currentLabel";
         const string endLabelMark = "endLabel";
         const string progressSliderMark = "progressSlider";
         const string playButtonMark = "playButton";
         const string rewindButtonMark = "rewindButton";
         const string fastForwardButtonMark = "fastForwardButton";
         var views = new DictionaryViews
         {
            {mainViewMark, mainView},
            {buttonViewMark, buttonView},
            {imageViewMark, imageView},
            {descriptionLabelMark, descriptionLabel},
            {currentLabelMark, currentLabel},
            {endLabelMark, endLabel},
            {progressSliderMark, _progressSlider},
            {playButtonMark, _playButton},
            {rewindButtonMark, rewindButton},
            {fastForwardButtonMark, fastForwardButton}
         };

         View.Add(mainView);

         mainView.Add(imageView);
         mainView.Add(descriptionLabel);
         mainView.Add(buttonView);
         mainView.Add(currentLabel);
         mainView.Add(endLabel);
         mainView.Add(_progressSlider);

         buttonView.Add(_playButton);
         buttonView.Add(rewindButton);
         buttonView.Add(fastForwardButton);

         View.AddConstraints(
            NSLayoutConstraint.FromVisualFormat($"V:|[{mainViewMark}]|", NSLayoutFormatOptions.DirectionLeftToRight,
                  null, views)
               .Concat(NSLayoutConstraint.FromVisualFormat($"H:|[{mainViewMark}]|", NSLayoutFormatOptions.AlignAllTop,
                  null, views))
               .ToArray());

         mainView.AddConstraints(
            NSLayoutConstraint.FromVisualFormat(
                  $"V:|-100-[{imageViewMark}(200)]-[{descriptionLabelMark}(30)]-[{buttonViewMark}(50)]-[{currentLabelMark}(30)]-[{progressSliderMark}]",
                  NSLayoutFormatOptions.DirectionLeftToRight, null, views)
               .Concat(NSLayoutConstraint.FromVisualFormat(
                  $"V:|-100-[{imageViewMark}(200)]-[{descriptionLabelMark}(30)]-[{buttonViewMark}(50)]-[{endLabelMark}(30)]-[{progressSliderMark}]",
                  NSLayoutFormatOptions.DirectionLeftToRight, null, views))
               .Concat(NSLayoutConstraint.FromVisualFormat($"H:|-20-[{progressSliderMark}]-20-|",
                  NSLayoutFormatOptions.AlignAllTop, null, views))
               .Concat(NSLayoutConstraint.FromVisualFormat($"H:|-25-[{currentLabelMark}(100)]",
                  NSLayoutFormatOptions.AlignAllTop, null, views))
               .Concat(NSLayoutConstraint.FromVisualFormat($"H:[{endLabelMark}(100)]-25-|",
                  NSLayoutFormatOptions.AlignAllTop, null, views))
               .Concat(NSLayoutConstraint.FromVisualFormat($"H:|-5-[{descriptionLabelMark}]-5-|",
                  NSLayoutFormatOptions.AlignAllTop, null, views))
               .Concat(NSLayoutConstraint.FromVisualFormat($"H:|-5-[{imageViewMark}]-5-|",
                  NSLayoutFormatOptions.AlignAllTop, null, views))
               .Concat(new[]
               {
                  NSLayoutConstraint.Create(buttonView, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, mainView,
                     NSLayoutAttribute.CenterX, 1, 0)
               })
               .ToArray());

         buttonView.AddConstraints(
            NSLayoutConstraint.FromVisualFormat($"V:|-5-[{rewindButtonMark}]-5-|", NSLayoutFormatOptions.AlignAllTop,
                  null, views)
               .Concat(NSLayoutConstraint.FromVisualFormat($"V:|-5-[{playButtonMark}]-5-|",
                  NSLayoutFormatOptions.AlignAllTop, null, views))
               .Concat(NSLayoutConstraint.FromVisualFormat($"V:|-5-[{fastForwardButtonMark}]-5-|",
                  NSLayoutFormatOptions.AlignAllTop, null, views))
               .Concat(NSLayoutConstraint.FromVisualFormat(
                  $"H:|-20-[{rewindButtonMark}]-[{playButtonMark}(100)]-[{fastForwardButtonMark}]-20-|",
                  NSLayoutFormatOptions.AlignAllTop, null, views))
               .ToArray());

         // create the binding set
         var bindingSet = this.CreateBindingSet<AudioPlayerViewController, AudioPlayerPageViewModel>();
         bindingSet.Bind(this).For(nameof(AudioPlayerPageViewModel.Title)).To(vm => vm.Title);
         bindingSet.Bind(descriptionLabel).To(vm => vm.DescriptionMessage);
         bindingSet.Bind(currentLabel).To(vm => vm.CurrentTimeStr);
         bindingSet.Bind(endLabel).To(vm => vm.EndTimeStr);
         bindingSet.Bind(_progressSlider).For(v => v.Value).To(vm => vm.CurrentTime).TwoWay().Apply();
         bindingSet.Bind(_progressSlider).For(v => v.MaxValue).To(vm => vm.EndTime);
         bindingSet.Bind(_playButton).To(vm => vm.PlayPauseCommand);
         bindingSet.Bind(rewindButton).To(vm => vm.RewindCommand);
         bindingSet.Bind(fastForwardButton).To(vm => vm.ForwardCommand);
         bindingSet.Apply();

         _model = (AudioPlayerPageViewModel) DataContext;
      }

      /// <summary>
      ///    Views the did appear.
      /// </summary>
      /// <returns>The did appear.</returns>
      /// <param name="animated">Animated.</param>
      public override void ViewDidAppear(bool animated)
      {
         _model.Load();
         base.ViewDidAppear(animated);
      }

      /// <summary>
      ///    Views the did disappear.
      /// </summary>
      /// <returns>The did disappear.</returns>
      /// <param name="animated">Animated.</param>
      public override void ViewDidDisappear(bool animated)
      {
         _model.Dispose();
         base.ViewDidDisappear(animated);
      }

      /// <summary>
      ///    Progresses the slider value changed.
      /// </summary>
      /// <returns>The slider value changed.</returns>
      /// <param name="sender">Sender.</param>
      /// <param name="e">E.</param>
      private void ProgressSliderValueChanged(object sender, EventArgs e) =>
         _model.UpdateAudioPosition(_progressSlider.Value);

      /// <summary>
      ///    Handles the touch up inside.
      /// </summary>
      /// <returns>The touch up inside.</returns>
      /// <param name="sender">Sender.</param>
      /// <param name="e">E.</param>
      private void HandlePlayButton(object sender, EventArgs e)
      {
         _playing = !_playing;
         _playButton.SetImage(UIImage.FromFile(_playing ? "pause.png" : "play.png"), UIControlState.Normal);
      }

      /// <summary>
      ///    Handles the rewind forward button.
      /// </summary>
      /// <returns>The rewind forward button.</returns>
      /// <param name="sender">Sender.</param>
      /// <param name="e">E.</param>
      private void HandleRewindForwardButton(object sender, EventArgs e)
      {
         _playing = false;
         _playButton.SetImage(UIImage.FromFile("play.png"), UIControlState.Normal);
      }
   }
}