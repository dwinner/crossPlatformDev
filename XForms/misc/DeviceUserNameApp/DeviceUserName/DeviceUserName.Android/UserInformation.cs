using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Accounts;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DeviceUserName.Droid;
using Plugin.CurrentActivity;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.Dependency(typeof(UserInformation))]
namespace DeviceUserName.Droid
{
    public class UserInformation : IUserInformation
    {
        public async Task<string> GetUserName()
        {
            FormsAppCompatActivity activity = CrossCurrentActivity.Current.Activity as FormsAppCompatActivity;

            AccountManager manager = AccountManager.Get(activity);
            Account[] accounts = manager.GetAccountsByType("com.google");
            List<String> emails = new List<string>();

            foreach (Account account in accounts)
            {
                var name = account.Name;
                emails.Add(account.Name);
            }

            if (emails.Any() && emails[0] != null)
            {
                String email = emails[0];
                String[] parts = email.Split('@');

                if (parts.Length > 1)
                    return parts[0];
            }

            return "";
        }
    }
}