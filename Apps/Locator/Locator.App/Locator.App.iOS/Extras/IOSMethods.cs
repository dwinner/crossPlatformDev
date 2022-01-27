using Locator.Common.Extras;
using ObjCRuntime;
using UIKit;

namespace Locator.App.iOS.Extras
{
   /// <summary>
   ///    The methods interface
   /// </summary>
   public class IosMethods : IMethods
   {
      /// <summary>
      ///    Exit this instance.
      /// </summary>
      public void Exit() =>
         UIApplication.SharedApplication.PerformSelector(new Selector("terminateWithSuccess"), null, 0f);
   }
}