using System.Threading.Tasks;
using Xamarin.Forms;

namespace ContactMvvmApp.Interfaces
{
   public interface IPageService
   {
      Task PushAsync(Page page);

      Task<Page> PopAsync();

      Task<bool> DisplayAlertAsync(string title, string message, string ok, string cancel);

      Task DisplayAlertAsync(string title, string message, string ok);
   }
}