using System.Windows.Input;
using SpeechTalk.App.Services;
using Xamarin.Forms;

namespace SpeechTalk.App.ViewModels
{
   /// <summary>
   ///    Main page view model.
   /// </summary>
   public class MainPageViewModel : ViewModelBase
   {
      private const string DefaultDescription = "Enter text and press the 'Speak' button to start speaking";
      private const string DefaultSpeakPlaceholder = "Text to speak";
      private const string DefaultSpeakText = "I'm the speaker";
      private const string DefaultSpeakTitle = "Speak";

      private string _descriptionMessage = DefaultDescription; // The description message.
      private ICommand _speakCommand; // The speak command.
      private string _speakEntryPlaceholder = DefaultSpeakPlaceholder; // The speak entry placeholder.
      private string _speakText = DefaultSpeakText; // The speak text.
      private string _speakTitle = DefaultSpeakTitle; // The speak title.

      /// <summary>
      ///    Initializes a new instance of the <see cref="MainPageViewModel"/> class.
      /// </summary>
      /// <param name="textToSpeech">Text to speech.</param>
      public MainPageViewModel(ITextToSpeech textToSpeech)
      {
         _speakCommand = new Command(_ => textToSpeech.Speak(SpeakText));
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
            if (value.Equals(_descriptionMessage))
            {
               return;
            }

            _descriptionMessage = value;
            OnPropertyChanged();
         }
      }

      /// <summary>
      ///    Gets or sets the speak entry placeholder.
      /// </summary>
      /// <value>The speak entry placeholder.</value>
      public string SpeakEntryPlaceholder
      {
         get => _speakEntryPlaceholder;

         set
         {
            if (value.Equals(_speakEntryPlaceholder))
            {
               return;
            }

            _speakEntryPlaceholder = value;
            OnPropertyChanged();
         }
      }

      /// <summary>
      ///    Gets or sets the speak text.
      /// </summary>
      /// <value>The speak text.</value>
      public string SpeakText
      {
         get => _speakText;

         set
         {
            if (value.Equals(_speakText))
            {
               return;
            }

            _speakText = value;
            OnPropertyChanged();
         }
      }

      /// <summary>
      ///    Gets or sets the speak title.
      /// </summary>
      /// <value>The speak title.</value>
      public string SpeakTitle
      {
         get => _speakTitle;

         set
         {
            if (value.Equals(_speakTitle))
            {
               return;
            }

            _speakTitle = value;
            OnPropertyChanged();
         }
      }

      /// <summary>
      ///    Gets or sets the speak command.
      /// </summary>
      /// <value>The speak command.</value>
      public ICommand SpeakCommand
      {
         get => _speakCommand;

         set
         {
            if (value.Equals(_speakCommand))
            {
               return;
            }

            _speakCommand = value;
            OnPropertyChanged();
         }
      }
   }
}