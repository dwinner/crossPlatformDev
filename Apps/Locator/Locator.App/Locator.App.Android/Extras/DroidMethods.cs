using Android.OS;
using Locator.Common.Extras;

namespace Locator.App.Droid.Extras
{
   /// <summary>
   ///    The android methods interface
   /// </summary>
   public class DroidMethods : IMethods
   {
      /// <summary>
      ///    Exit this instance.
      /// </summary>
      public void Exit() => Process.KillProcess(Process.MyPid());
   }
}