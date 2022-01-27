using Windows.UI.Xaml;
using Locator.Common.Extras;

namespace Locator.App.UWP.Extras
{
   /// <summary>
   ///    The methods interface
   /// </summary>
   public class WinPhoneMethods : IMethods
   {
      /// <summary>
      ///    Exits the application.
      /// </summary>
      public void Exit() => Application.Current.Exit();
   }
}