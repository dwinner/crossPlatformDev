using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StockList.WebApi.Models;

namespace StockList.WebApi.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class StockListController : ControllerBase
   {
      private readonly List<StockItem> _stockItems = new List<StockItem>
      {
         new StockItem
         {
            Id = 1,
            Name = "Tomato Soup",
            Category = "Groceries",
            Price = 1
         },
         new StockItem
         {
            Id = 2,
            Name = "Yo-Yo",
            Category = "Toys",
            Price = 3.75M
         },
         new StockItem
         {
            Id = 3,
            Name = "Hammer",
            Category = "Hardware",
            Price = 16.99M
         }
      };

      // GET api/stocklist
      [HttpGet]
      public ActionResult<IEnumerable<StockItem>> GetStockItems() => _stockItems;

      // GET api/stocklist/1
      [HttpGet("{id}")]
      public ActionResult<StockItem> GetStockItem(int id) => _stockItems.FirstOrDefault(item => item.Id == id);
   }
}