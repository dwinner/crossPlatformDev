using ObjCRuntime;
using StockList.Core.Extras;
using UIKit;

namespace StockListApp.iOS.Extras
{
   public class IOSMethods : IMethods
   {
      public void Exit() =>
         UIApplication.SharedApplication.PerformSelector(new Selector("terminateWithSuccess"), null, 0f);
   }
}