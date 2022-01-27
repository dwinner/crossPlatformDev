using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReactiveUIDemo.Services
{
   internal class LoginService : ILogin
   {
      private readonly Dictionary<string, string> _userCredentials;

      public LoginService()
      {
         _userCredentials = new Dictionary<string, string>
         {
            {"us@sad.com", "aaaaaaaa"},
            {"user2@sad.com", "Userabc123"},
            {"user3@sad.com", "!A@3534"}
         };
      }

      public async Task<bool> LoginAsync(string username, string password)
      {
         //await Task.Delay(TimeSpan.FromMilliseconds(300)).ConfigureAwait(false);
         if (_userCredentials.ContainsKey(username))
         {
            return _userCredentials[username] == password;
         }

         return false;
      }
   }
}