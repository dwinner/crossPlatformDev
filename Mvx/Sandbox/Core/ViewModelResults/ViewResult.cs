namespace MvvxSandboxApp.Core.ViewModelResults
{
   public class ViewResult<TEntity>
   {
      public TEntity Entity { get; set; }

      public ViewAction Action { get; set; }
   }
}