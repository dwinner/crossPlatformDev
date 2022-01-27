using System.Threading.Tasks;

namespace TelephonyApp
{
   public interface ICallUp
   {
      Task CallAsync(string aPhoneNumber);
   }
}