using MvvmCross.ViewModels;

namespace MvvxSandboxApp.Core.ViewModels.Core
{
   public abstract class BaseViewModel<TParameter, TResult> : BaseViewModelResult<TResult>,
      IMvxViewModel<TParameter, TResult>
      where TParameter : notnull
      where TResult : notnull
   {
      public abstract void Prepare(TParameter parameter);
   }
}