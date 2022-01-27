using UIKit;

namespace Chat.iOS
{
   /// <summary>
   ///    Application.
   /// </summary>
   public class Application
   {
      /// <summary>
      ///    The entry point of the program, where the program control starts and ends.
      /// </summary>
      /// <param name="args">The command-line arguments.</param>
      private static void Main(string[] args)
      {
         UIApplication.Main(args, null, nameof(AppDelegate));
      }
   }
}