using System.Threading.Tasks;
using Android.Content;
using TelephonyApp.Droid.Properties;
using Xamarin.Forms;
using DroidApp = Android.App.Application;
using DroidUri = Android.Net.Uri;

[assembly: Dependency(typeof(DroidCallUpImpl))]

namespace TelephonyApp.Droid.Properties
{
   public class DroidCallUpImpl : ICallUp
   {
      public Task CallAsync(string aPhoneNumber)
      {
         var packageManager = DroidApp.Context.PackageManager;
         var telephoneUri = DroidUri.Parse($"tel:{aPhoneNumber}");
         var callIntent = new Intent(Intent.ActionCall, telephoneUri);
         callIntent.AddFlags(ActivityFlags.NewTask);
         var callAccessibility = callIntent.ResolveActivity(packageManager) != null;
         if (!string.IsNullOrWhiteSpace(aPhoneNumber) && callAccessibility)
         {
            DroidApp.Context.StartActivity(callIntent);
         }

         return Task.FromResult(true);
      }
   }
}