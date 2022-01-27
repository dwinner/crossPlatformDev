using StockList.Core.Extras;
using DroidProc = Android.OS.Process;

namespace StockListApp.Droid.Extras
{
   public class DroidMethodsImpl : IMethods
   {
      public void Exit() => DroidProc.KillProcess(DroidProc.MyPid());
   }
}