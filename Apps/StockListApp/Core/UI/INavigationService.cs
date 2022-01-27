using System.Collections.Generic;
using System.Threading.Tasks;
using StockList.Core.Enums;

namespace StockList.Core.UI
{
   /// <summary>
   ///    Navigation service
   /// </summary>
   public interface INavigationService
   {
      /// <summary>
      ///    Navigate the specified page name and navigation parameters
      /// </summary>
      /// <param name="pageName">Page name</param>
      /// <param name="navigationParameters">Navigation parameters</param>
      /// <returns></returns>
      Task NavigateAsync(PageNames pageName, IDictionary<string, object> navigationParameters);
   }
}