using Windows.UI.Xaml;
using StockList.Core.Extras;

namespace StockListApp.UWP.Extras
{
   public class UwpMethods : IMethods
   {
      public void Exit() => Application.Current.Exit();
   }
}