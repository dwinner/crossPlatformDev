using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InfiniteScrollDemo.Models;

namespace InfiniteScrollDemo.Services
{
   public class MockDataStore : IDataStore<Item>
   {
      private const int NumberOfItemsPerPage = 15;
      private readonly List<Item> _items;

      public MockDataStore() =>
         _items = new List<Item>
         {
            new Item
            {
               Id = Guid.NewGuid().ToString(),
               Text = "First item",
               Description = "This is an item description."
            },
            new Item
            {
               Id = Guid.NewGuid().ToString(),
               Text = "Second item",
               Description = "This is an item description."
            },
            new Item
            {
               Id = Guid.NewGuid().ToString(),
               Text = "Third item",
               Description = "This is an item description."
            },
            new Item
            {
               Id = Guid.NewGuid().ToString(),
               Text = "Fourth item",
               Description = "This is an item description."
            },
            new Item
            {
               Id = Guid.NewGuid().ToString(),
               Text = "Fifth item",
               Description = "This is an item description."
            },
            new Item
            {
               Id = Guid.NewGuid().ToString(),
               Text = "Sixth item",
               Description = "This is an item description."
            },
            new Item
            {
               Id = Guid.NewGuid().ToString(),
               Text = "Seventh item",
               Description = "This is an item description."
            },
            new Item
            {
               Id = Guid.NewGuid().ToString(),
               Text = "Eigth item",
               Description = "This is an item description."
            },
            new Item
            {
               Id = Guid.NewGuid().ToString(),
               Text = "Nineth item", Description = "This is an item description."
            },
            new Item
            {
               Id = Guid.NewGuid().ToString(),
               Text = "Tenth item",
               Description = "This is an item description."
            },
            new Item
            {
               Id = Guid.NewGuid().ToString(),
               Text = "Eleventh item",
               Description = "This is an item description."
            },
            new Item
            {
               Id = Guid.NewGuid().ToString(),
               Text = "Twelveth item",
               Description = "This is an item description."
            },
            new Item
            {
               Id = Guid.NewGuid().ToString(),
               Text = "Thirteenth item",
               Description = "This is an item description."
            },
            new Item
            {
               Id = Guid.NewGuid().ToString(),
               Text = "Fourteenth item",
               Description = "This is an item description."
            },
            new Item
            {
               Id = Guid.NewGuid().ToString(),
               Text = "Fifteenth item",
               Description = "This is an item description."
            },
            new Item
            {
               Id = Guid.NewGuid().ToString(),
               Text = "Sixteenth item",
               Description = "This is an item description."
            },
            new Item
            {
               Id = Guid.NewGuid().ToString(),
               Text = "Seventeenth item",
               Description = "This is an item description."
            },
            new Item
            {
               Id = Guid.NewGuid().ToString(),
               Text = "Eighteenth item",
               Description = "This is an item description."
            },
            new Item
            {
               Id = Guid.NewGuid().ToString(),
               Text = "Nineteenth item",
               Description = "This is an item description."
            },
            new Item
            {
               Id = Guid.NewGuid().ToString(),
               Text = "Twentieth item",
               Description = "This is an item description."
            },
            new Item
            {
               Id = Guid.NewGuid().ToString(),
               Text = "Twentyfirst item",
               Description = "This is an item description."
            },
            new Item
            {
               Id = Guid.NewGuid().ToString(),
               Text = "Twentysecond item",
               Description = "This is an item description."
            },
            new Item
            {
               Id = Guid.NewGuid().ToString(),
               Text = "Twentythird item",
               Description = "This is an item description."
            },
            new Item
            {
               Id = Guid.NewGuid().ToString(),
               Text = "Twentyfourth item",
               Description = "This is an item description."
            },
            new Item
            {
               Id = Guid.NewGuid().ToString(),
               Text = "Twentyfifth item",
               Description = "This is an item description."
            },
            new Item
            {
               Id = Guid.NewGuid().ToString(),
               Text = "Twentysixth item",
               Description = "This is an item description."
            },
            new Item
            {
               Id = Guid.NewGuid().ToString(),
               Text = "Twentyseventh item",
               Description = "This is an item description."
            },
            new Item
            {
               Id = Guid.NewGuid().ToString(),
               Text = "Twentyeight item",
               Description = "This is an item description."
            },
            new Item
            {
               Id = Guid.NewGuid().ToString(),
               Text = "Twentynineth item",
               Description = "This is an item description."
            },
            new Item
            {
               Id = Guid.NewGuid().ToString(),
               Text = "Thirtieth item",
               Description = "This is an item description."
            }
         };

      public async Task<bool> AddItemAsync(Item item)
      {
         _items.Add(item);
         return await Task.FromResult(true).ConfigureAwait(true);
      }

      public async Task<bool> UpdateItemAsync(Item item)
      {
         var oldItem = _items.FirstOrDefault(arg => arg.Id == item.Id);
         _items.Remove(oldItem);
         _items.Add(item);

         return await Task.FromResult(true).ConfigureAwait(true);
      }

      public async Task<bool> DeleteItemAsync(string id)
      {
         var oldItem = _items.FirstOrDefault(arg => arg.Id == id);
         _items.Remove(oldItem);

         return await Task.FromResult(true).ConfigureAwait(true);
      }

      public async Task<Item> GetItemAsync(string id)
      {
         return await Task.FromResult(_items.FirstOrDefault(s => s.Id == id)).ConfigureAwait(true);
      }

      public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false, int lastIndex = 0)
      {
         await Task.Delay(TimeSpan.FromSeconds(3)).ConfigureAwait(true);
         return await Task.FromResult(_items.Skip(lastIndex).Take(NumberOfItemsPerPage)).ConfigureAwait(true);
      }
   }
}