using AudioPlayerApp.Core.ViewModels;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

namespace AudioPlayerApp.iOS.Views
{
   [MvxRootPresentation(WrapInNavigationController = true)]
   public class MainViewController : MvxViewController<MainPageViewModel>
   {
      public override void ViewDidLoad()
      {
         base.ViewDidLoad();

         var mainView = new UIView
         {
            TranslatesAutoresizingMaskIntoConstraints = false,
            BackgroundColor = UIColor.White
         };

         var imageView = new UIImageView
         {
            TranslatesAutoresizingMaskIntoConstraints = false,
            ContentMode = UIViewContentMode.ScaleAspectFit,
            Image = new UIImage("audio.png")
         };

         var descriptionLabel = new UILabel
         {
            TranslatesAutoresizingMaskIntoConstraints = false,
            TextAlignment = UITextAlignment.Center
         };

         var audioPlayerButton = new UIButton(UIButtonType.RoundedRect)
         {
            TranslatesAutoresizingMaskIntoConstraints = false
         };

         var exitButton = new UIButton(UIButtonType.RoundedRect)
         {
            TranslatesAutoresizingMaskIntoConstraints = false
         };

         View.Add(mainView);

         // add buttons to the main view
         mainView.Add(imageView);
         mainView.Add(descriptionLabel);
         mainView.Add(audioPlayerButton);
         mainView.Add(exitButton);

         const string mainViewMark = "mainView";
         const string imageViewMark = "imageView";
         const string descriptionLabelMark = "descriptionLabel";
         const string audioPlayerButtonMark = "audioPlayerButton";
         const string exitButtonMark = "exitButton";

         View.AddConstraints(NSLayoutConstraint.FromVisualFormat($"V:|[{mainViewMark}]|",
            NSLayoutFormatOptions.DirectionLeftToRight, null, new NSDictionary(mainViewMark, mainView)));
         View.AddConstraints(NSLayoutConstraint.FromVisualFormat($"H:|[{mainViewMark}]|",
            NSLayoutFormatOptions.AlignAllTop, null, new NSDictionary(mainViewMark, mainView)));

         mainView.AddConstraints(NSLayoutConstraint.FromVisualFormat(
            $"V:|-100-[{imageViewMark}(200)]-50-[{descriptionLabelMark}]-50-[{audioPlayerButtonMark}]-[{exitButtonMark}]",
            NSLayoutFormatOptions.DirectionLeftToRight, null,
            new NSDictionary(imageViewMark, imageView, descriptionLabelMark, descriptionLabel, audioPlayerButtonMark,
               audioPlayerButton, exitButtonMark, exitButton)));
         mainView.AddConstraints(NSLayoutConstraint.FromVisualFormat($"H:|-5-[{imageViewMark}]-5-|",
            NSLayoutFormatOptions.AlignAllTop, null, new NSDictionary(imageViewMark, imageView)));
         mainView.AddConstraints(NSLayoutConstraint.FromVisualFormat($"H:|-5-[{descriptionLabelMark}]-5-|",
            NSLayoutFormatOptions.AlignAllTop, null, new NSDictionary(descriptionLabelMark, descriptionLabel)));
         mainView.AddConstraints(NSLayoutConstraint.FromVisualFormat($"H:|-5-[{audioPlayerButtonMark}]-5-|",
            NSLayoutFormatOptions.AlignAllTop, null, new NSDictionary(audioPlayerButtonMark, audioPlayerButton)));
         mainView.AddConstraints(NSLayoutConstraint.FromVisualFormat($"H:|-5-[{exitButtonMark}]-5-|",
            NSLayoutFormatOptions.AlignAllTop, null, new NSDictionary(exitButtonMark, exitButton)));

         // create the binding set
         var bindingSet = this.CreateBindingSet<MainViewController, MainPageViewModel>();
         bindingSet.Bind(this).For(nameof(MainPageViewModel.Title)).To(vm => vm.Title);
         bindingSet.Bind(descriptionLabel).To(vm => vm.DescriptionMessage);
         bindingSet.Bind(audioPlayerButton).For(nameof(MainPageViewModel.Title)).To(vm => vm.AudioPlayerTitle);
         bindingSet.Bind(audioPlayerButton).To(vm => vm.AudioPlayerCommand);
         bindingSet.Bind(exitButton).For(nameof(MainPageViewModel.Title)).To(vm => vm.ExitTitle);
         bindingSet.Bind(exitButton).To(vm => vm.ExitCommand);
         bindingSet.Apply();
      }
   }
}