/**
 * See https://doumer.me/smart-app-with-luis-and-xamarin-android/
 * for details
 */

using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Speech;
using Android.Widget;
using Java.Util;
using Microsoft.Cognitive.LUIS;
using Intent = Android.Content.Intent;

namespace SmartApp
{
   [Activity(Label = "SmartApp", MainLauncher = true)]
   public class MainActivity : Activity
   {
      private const string AppId = "";
      private const string SubcriptionKey = "";
      private const int Voice = 10;
      private string _userCommand;
      private bool _isRecording;
      private Button _recButton;
      private TextView _textView;

      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);

         // Set our view from the "main" layout resource
         SetContentView(Resource.Layout.Main);

         _recButton = FindViewById<Button>(Resource.Id.button1);
         _textView = FindViewById<TextView>(Resource.Id.textView1);

         _recButton.Click += RecButton_Click;
      }

      public void WriteInterpretation(string msg)
      {
         _textView.Text = msg;
      }

      private void RecButton_Click(object sender, EventArgs e)
      { // change the text on the button
         _recButton.Text = "End Recording";
         _isRecording = !_isRecording;
         if (_isRecording)
         {
            // create intent and start the activity
            var voiceIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
            voiceIntent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);

            // put a message on the modal dialog
            voiceIntent.PutExtra(RecognizerIntent.ExtraPrompt, "Speak now");

            // end speech if 1.5 secs have passed
            voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, 1500);
            voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, 15000);
            voiceIntent.PutExtra(RecognizerIntent.ExtraMaxResults, 1);
            voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputPossiblyCompleteSilenceLengthMillis, 1500);

            voiceIntent.PutExtra(RecognizerIntent.ExtraLanguage, Locale.Default);
            StartActivityForResult(voiceIntent, Voice);
         }
      }

      protected override async void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
      {
         if (requestCode == Voice)
         {
            if (resultCode == Result.Ok)
            {
               var matches = data.GetStringArrayListExtra(RecognizerIntent.ExtraResults);
               if (matches.Count != 0)
               {
                  _userCommand = matches[0];

                  var intentHandler = new IntentHandler();

                  using var router = IntentRouter.Setup(AppId, SubcriptionKey, intentHandler);
                  var handled = await router.Route(_userCommand, this).ConfigureAwait(true);
                  if (!handled)
                  {
                     WriteInterpretation("Could not process text.");
                  }
               }
            }

            base.OnActivityResult(requestCode, resultCode, data);
         }
      }
   }
}