using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite;

namespace Todo
{
   public class TodoItemDatabase
   {
      private static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
      {
         return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
      });

      private static bool initialized;

      public TodoItemDatabase()
      {
         InitializeAsync().SafeFireAndForget(false);
      }

      private static SQLiteAsyncConnection Database => lazyInitializer.Value;

      private async Task InitializeAsync()
      {
         if (!initialized)
         {
            if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(TodoItem).Name))
            {
               await Database.CreateTablesAsync(CreateFlags.None, typeof(TodoItem)).ConfigureAwait(false);
            }

            initialized = true;
         }
      }

      public Task<List<TodoItem>> GetItemsAsync() => Database.Table<TodoItem>().ToListAsync();

      public Task<List<TodoItem>> GetItemsNotDoneAsync() =>
         Database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");

      public Task<TodoItem> GetItemAsync(int id)
      {
         return Database.Table<TodoItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
      }

      public Task<int> SaveItemAsync(TodoItem item)
      {
         if (item.ID != 0)
         {
            return Database.UpdateAsync(item);
         }

         return Database.InsertAsync(item);
      }

      public Task<int> DeleteItemAsync(TodoItem item) => Database.DeleteAsync(item);
   }
}