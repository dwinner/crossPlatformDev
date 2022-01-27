using System.Threading.Tasks;

namespace FileStorageApp.Methods
{
   /// <summary>
   ///    The methods interface
   /// </summary>
   public interface IMethods
   {
      /// <summary>
      ///    Exit this instance.
      /// </summary>
      void Exit();

      /// <summary>
      ///    Displaies the entry alert.
      /// </summary>
      /// <returns>The entry alert.</returns>
      void DisplayEntryAlert(TaskCompletionSource<string> puppetTask, string message);
   }
}