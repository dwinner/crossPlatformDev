namespace MvvxSandboxApp.Core.ViewModelResults
{
   public class ConfirmationResult<TEntity>
   {
      public TEntity Entity { get; set; }

      public bool IsConfirmed { get; set; }
   }
}