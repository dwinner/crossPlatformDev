using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using Chat.Common.Presenter;
using Chat.Droid.Services;

namespace Chat.Droid.Views
{
   /// <summary>
   ///    Login activity.
   /// </summary>
   [Activity(MainLauncher = true, Label = "Chat", ScreenOrientation = ScreenOrientation.Portrait)]
   public class LoginActivity : Activity, LoginPresenter.ILoginView
   {
      /// <summary>
      ///    The dialog shown.
      /// </summary>
      private bool _dialogShown;

      /// <summary>
      ///    The is in progress.
      /// </summary>
      private bool _isInProgress;

      /// <summary>
      ///    The login field.
      /// </summary>
      private EditText _loginField;

      /// <summary>
      ///    The password field.
      /// </summary>
      private EditText _passwordField;

      /// <summary>
      ///    The presenter.
      /// </summary>
      private LoginPresenter _presenter;

      /// <summary>
      ///    The progress dialog.
      /// </summary>
#pragma warning disable 618
      private ProgressDialog progressDialog;
#pragma warning restore 618

      /// <summary>
      ///    Occurs when login.
      /// </summary>
      public event EventHandler<Tuple<string, string>> Login;

      /// <summary>
      ///    Occurs when register.
      /// </summary>
      public event EventHandler<Tuple<string, string>> Register;

      /// <summary>
      ///    Sets the error message.
      /// </summary>
      /// <returns>The error message.</returns>
      /// <param name="message">Message.</param>
      public void SetErrorMessage(string message)
      {
         if (!_dialogShown)
         {
            _dialogShown = true;

            var builder = new AlertDialog.Builder(this);
            builder
               .SetTitle("Chat")
               .SetMessage(message)
               .SetNeutralButton("Ok", (sender, e) => { _dialogShown = false; })
               .Show();
         }
      }

      /// <summary>
      ///    Gets or sets the is in progress.
      /// </summary>
      /// <value>The is in progress.</value>
      public bool IsInProgress
      {
         get => _isInProgress;

         set
         {
            if (value == _isInProgress)
            {
               return;
            }

            // we control the activity view when we set 'IsInProgress'
            if (value)
            {
               progressDialog.Show();
            }
            else
            {
               progressDialog.Dismiss();
            }

            _isInProgress = value;
         }
      }

      /// <summary>
      ///    Ons the create.
      /// </summary>
      /// <returns>The create.</returns>
      /// <param name="bundle">Bundle.</param>
      protected override void OnCreate(Bundle bundle)
      {
         base.OnCreate(bundle);

         SetContentView(Resource.Layout.LoginView);

#pragma warning disable 618
         progressDialog = new ProgressDialog(this);
#pragma warning restore 618
         progressDialog.SetMessage("Loading...");
         progressDialog.SetCancelable(false);

         _loginField = FindViewById<EditText>(Resource.Id.usernameField);
         _passwordField = FindViewById<EditText>(Resource.Id.passwordField);

         var registerButton = FindViewById<Button>(Resource.Id.registerButton);
         registerButton.Touch += (sender, e) =>
            Register(this, new Tuple<string, string>(_loginField.Text, _passwordField.Text));

         var loginButton = FindViewById<Button>(Resource.Id.loginButton);
         loginButton.Touch += (sender, e) =>
            Login(this, new Tuple<string, string>(_loginField.Text, _passwordField.Text));

         var app = ChatApplication.GetApplication(this);

         var state = new ApplicationState();
         _presenter = new LoginPresenter(state, new NavigationService(app));
         _presenter.SetView(this);

         app.CurrentActivity = this;
      }

      /// <summary>
      ///    Ons the resume.
      /// </summary>
      /// <returns>The resume.</returns>
      protected override void OnResume()
      {
         base.OnResume();

         var app = ChatApplication.GetApplication(this);
         app.CurrentActivity = this;
         _presenter?.SetView(this);
      }
   }
}