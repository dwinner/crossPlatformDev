using System;
using System.Collections.Generic;

namespace StockList.Core.WebServices
{
   /// <summary>
   ///    The stock item web service controller interface
   /// </summary>
   public interface IStocklistWebServiceController
   {
      /// <summary>
      ///    Gets the stock items
      /// </summary>
      /// <returns>The stock items</returns>
      IObservable<List<StockItemContract>> GetAllStockItems();

      /// <summary>
      ///    Gets the stock item
      /// </summary>
      /// <param name="id">Identifier</param>
      /// <returns>The stock item</returns>
      IObservable<StockItemContract> GetStockItem(int id);
   }
}