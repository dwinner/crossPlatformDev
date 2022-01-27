using System.Threading.Tasks;
using MvvmCross.ViewModels;

namespace MvvxSandboxApp.Core.ViewModels.Core
{
   public abstract class BaseViewModelResult<TResult> : BaseViewModel, IMvxViewModelResult<TResult>
      where TResult : notnull
   {
      public TaskCompletionSource<object> CloseCompletionSource { get; set; }

      public override void ViewDestroy(bool viewFinishing = true)
      {
         if (viewFinishing && CloseCompletionSource?.Task.IsCompleted == false &&
             !CloseCompletionSource.Task.IsFaulted)
         {
            CloseCompletionSource?.TrySetCanceled();
         }

         base.ViewDestroy(viewFinishing);
      }
   }
}