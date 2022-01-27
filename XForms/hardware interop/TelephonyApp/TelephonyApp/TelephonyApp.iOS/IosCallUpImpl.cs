using System;
using System.Threading.Tasks;
using Foundation;
using TelephonyApp.iOS;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(IosCallUpImpl))]

namespace TelephonyApp.iOS
{
   public class IosCallUpImpl : ICallUp
   {
      public Task CallAsync(string aPhoneNumber)
      {
         var phoneUri = new Uri($"tel:{aPhoneNumber}");
         var nsUrl = new NSUrl(phoneUri.AbsoluteUri);
         if (!string.IsNullOrWhiteSpace(aPhoneNumber) && UIApplication.SharedApplication.CanOpenUrl(nsUrl))
         {
            UIApplication.SharedApplication.OpenUrl(nsUrl);
         }

         return Task.FromResult(true);
      }
   }
}