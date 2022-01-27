using DeviceUserName.UWP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

[assembly: Xamarin.Forms.Dependency(typeof(UserInformation))]
namespace DeviceUserName.UWP
{
    public class UserInformation : IUserInformation
    {
        public async Task<string> GetUserName()
        {
            IReadOnlyList<User> users = await User.FindAllAsync();

            var current = users.Where(p => p.AuthenticationStatus == UserAuthenticationStatus.LocallyAuthenticated && p.Type == UserType.LocalUser).FirstOrDefault();
            
            var data = await current.GetPropertyAsync(KnownUserProperties.AccountName);
            string username = data.ToString();

            if (String.IsNullOrEmpty(username))
            {
                username = $"{(await current.GetPropertyAsync(KnownUserProperties.FirstName)).ToString()} {(await current.GetPropertyAsync(KnownUserProperties.LastName)).ToString()}";
            }

            return username;
        }
    }
}
