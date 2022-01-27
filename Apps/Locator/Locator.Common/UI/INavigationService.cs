using System.Collections.Generic;
using System.Threading.Tasks;
using Locator.Common.Enums;

namespace Locator.Common.UI
{
   /// <summary>
   ///    Navigation service.
   /// </summary>
   public interface INavigationService
   {
      /// <summary>
      ///    Navigate the specified pageName and navigationParameters.
      /// </summary>
      /// <param name="pageName">Page name.</param>
      /// <param name="navigationParameters">Navigation parameters.</param>
      Task NavigateAsync(PageNames pageName, IDictionary<string, object> navigationParameters);
   }
}