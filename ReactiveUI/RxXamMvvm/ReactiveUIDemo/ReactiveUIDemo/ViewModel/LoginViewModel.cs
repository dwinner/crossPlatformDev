using System;
using System.Text.RegularExpressions;
using ReactiveUI;
using ReactiveUIDemo.Services;

namespace ReactiveUIDemo.ViewModel
{
   public class LoginViewModel : ViewModelBase
   {
      private string _password;

      private string _userName;

      /// <summary>
      ///    This is an Oaph Observable propperty helper,
      ///    Which is used to determine whether a subsequent action
      ///    Could be performed or not depending on its value
      ///    This condition is calculated every time its value changes.
      /// </summary>
      private readonly ObservableAsPropertyHelper<bool> _validLogin;

      public LoginViewModel(ILogin login, IScreen hostScreen = null) : base(hostScreen)
      {
         this.WhenAnyValue(x => x.UserName, x => x.Password,
               (email, password) =>
                  !string.IsNullOrEmpty(password) && password.Length > 5 && !string.IsNullOrEmpty(email) &&
                  Regex.Matches(email, "^\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$").Count == 1)
            .ToProperty(this, v => v.ValidLogin, out _validLogin);

         LoginCommand = ReactiveCommand.CreateFromTask(async () =>
         {
            var lg = await login.LoginAsync(_userName, _password).ConfigureAwait(true);
            if (lg)
            {
               HostScreen.Router
                  .Navigate
                  .Execute(new ItemsViewModel())
                  .Subscribe();
            }
         }, this.WhenAnyValue(x => x.ValidLogin, x => x.ValidLogin, (validLogin, valid) => ValidLogin && valid));
      }

      public string UserName
      {
         get => _userName;
         //Notify when property user name changes
         set => this.RaiseAndSetIfChanged(ref _userName, value);
      }

      public string Password
      {
         get => _password;
         set => this.RaiseAndSetIfChanged(ref _password, value);
      }

      public bool ValidLogin => _validLogin?.Value ?? false;

      public ReactiveCommand LoginCommand { get; }
   }
}