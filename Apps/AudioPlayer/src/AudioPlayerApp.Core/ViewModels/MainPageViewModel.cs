using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace AudioPlayerApp.Core.ViewModels
{
   public class MainPageViewModel : MvxViewModel
   {
      private const string DefaultTitle = "Welcome"; // The title.
      private readonly IMvxNavigationService _navigationService;

      /// <summary>
      ///    The audio player command.
      /// </summary>
      private IMvxCommand _audioPlayerCommand;

      /// <summary>
      ///    The audio player title.
      /// </summary>
      private string _audioPlayerTitle = "Audio Player";

      /// <summary>
      ///    The description message.
      /// </summary>
      private string _descriptionMessage = "Welcome to the Music Room";

      /// <summary>
      ///    The exit command.
      /// </summary>
      private IMvxCommand _exitCommand;

      /// <summary>
      ///    The exit title.
      /// </summary>
      private string _exitTitle = "Exit";

      /// <summary>
      ///    Initializes a new instance of the <see cref="MainPageViewModel" /> class.
      /// </summary>
      public MainPageViewModel(IMvxNavigationService navigationService) => _navigationService = navigationService;

      /// <summary>
      ///    Gets or sets the title.
      /// </summary>
      /// <value>The title.</value>
      public string Title
      {
         get => DefaultTitle;
         set
         {
            if (!value.Equals(DefaultTitle))
            {
               _descriptionMessage = value;
               RaisePropertyChanged(() => Title);
            }
         }
      }

      /// <summary>
      ///    Gets or sets the description message.
      /// </summary>
      /// <value>The description message.</value>
      public string DescriptionMessage
      {
         get => _descriptionMessage;
         set
         {
            if (!value.Equals(_descriptionMessage))
            {
               _descriptionMessage = value;
               RaisePropertyChanged(() => DescriptionMessage);
            }
         }
      }

      /// <summary>
      ///    Gets or sets the audio player title.
      /// </summary>
      /// <value>The audio player title.</value>
      public string AudioPlayerTitle
      {
         get => _audioPlayerTitle;
         set
         {
            if (!value.Equals(_audioPlayerTitle))
            {
               _audioPlayerTitle = value;
               RaisePropertyChanged(() => AudioPlayerTitle);
            }
         }
      }

      /// <summary>
      ///    Gets or sets the exit title.
      /// </summary>
      /// <value>The exit title.</value>
      public string ExitTitle
      {
         get => _exitTitle;
         set
         {
            if (!value.Equals(_exitTitle))
            {
               _exitTitle = value;
               RaisePropertyChanged(() => ExitTitle);
            }
         }
      }

      /// <summary>
      ///    Gets or sets the audio player command.
      /// </summary>
      /// <value>The audio player command.</value>
      public IMvxCommand AudioPlayerCommand
      {
         get => _audioPlayerCommand
                ?? (_audioPlayerCommand =
                   new MvxCommand(() => _navigationService.Navigate<AudioPlayerPageViewModel>()));

         set
         {
            if (!value.Equals(_audioPlayerCommand))
            {
               _audioPlayerCommand = value;
               RaisePropertyChanged(() => AudioPlayerCommand);
            }
         }
      }

      /// <summary>
      ///    Gets or sets the exit command.
      /// </summary>
      /// <value>The exit command.</value>
      public IMvxCommand ExitCommand
      {
         get => _exitCommand
                ?? (_exitCommand = new MvxAsyncCommand(() => _navigationService.Close(this)));

         set
         {
            if (!value.Equals(_exitCommand))
            {
               _exitCommand = value;
               RaisePropertyChanged(() => ExitCommand);
            }
         }
      }
   }
}