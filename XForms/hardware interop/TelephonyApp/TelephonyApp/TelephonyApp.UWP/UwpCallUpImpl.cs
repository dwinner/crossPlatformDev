using System.Threading.Tasks;
using InTheHand.ApplicationModel.Calls;
using TelephonyApp.UWP;
using Xamarin.Forms;

[assembly: Dependency(typeof(UwpCallUpImpl))]

namespace TelephonyApp.UWP
{
   public class UwpCallUpImpl : ICallUp
   {
      public Task CallAsync(string aPhoneNumber)
      {
         PhoneCallManager.ShowPhoneCallUI(aPhoneNumber, "Test call", true);
         return Task.FromResult(true);
      }
   }
}