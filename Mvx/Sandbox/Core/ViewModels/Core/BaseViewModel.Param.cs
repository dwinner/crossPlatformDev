using MvvmCross.ViewModels;

namespace MvvxSandboxApp.Core.ViewModels.Core
{
   public abstract class BaseViewModel<TParameter> : BaseViewModel, IMvxViewModel<TParameter>
      where TParameter : notnull
   {
      public abstract void Prepare(TParameter parameter);
   }
}