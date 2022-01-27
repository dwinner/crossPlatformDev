using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;

// ReSharper disable RedundantAwait
// ReSharper disable AsyncConverter.AsyncAwaitMayBeElidedHighlighting

namespace SqLiteSampleApp
{
   public class FriendRepository
   {
      private readonly SQLiteAsyncConnection _database;

      public FriendRepository(string fileName)
      {
         var databasePath = DependencyService.Get<ISqLite>().GetDatabasePath(fileName);
         _database = new SQLiteAsyncConnection(databasePath);
      }

      public async Task CreateTableAsync() =>
         await _database.CreateTableAsync<Friend>().ConfigureAwait(false);

      public async Task<IEnumerable<Friend>> GetItemsAsync() =>
         await _database.Table<Friend>().ToListAsync().ConfigureAwait(false);

      public async Task<Friend> GetItemAsync(int friendId) =>
         await _database.GetAsync<Friend>(friendId).ConfigureAwait(false);

      public async Task<int> DeleteItemAsync(int friendId) =>
         await _database.DeleteAsync<Friend>(friendId).ConfigureAwait(false);

      public async Task<int> SaveItemAsync(Friend aFriend)
      {
         if (aFriend.Id != 0)
         {
            await _database.UpdateAsync(aFriend).ConfigureAwait(false);
            return aFriend.Id;
         }

         return await _database.InsertAsync(aFriend).ConfigureAwait(false);
      }
   }
}