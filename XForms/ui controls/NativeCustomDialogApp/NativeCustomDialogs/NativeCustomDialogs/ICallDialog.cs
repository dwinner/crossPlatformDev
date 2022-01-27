using System.Threading.Tasks;

namespace NativeCustomDialogs
{
   public interface ICallDialog
   {
      Task CallDialog(object viewModel);
   }
}