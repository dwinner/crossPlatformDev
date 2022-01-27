using System.Collections.Generic;
using System.Threading.Tasks;
using ContactMvvmApp.Interfaces;
using ContactMvvmApp.Models;
using SQLite;

namespace ContactMvvmApp.Impl
{
   public sealed class ContactStoreImpl : IContactStore
   {
      private readonly SQLiteAsyncConnection _connection;

      public ContactStoreImpl(ISqLiteDb db)
      {
         _connection = db.GetConnection();
         _connection.CreateTableAsync<Contact>();
      }

      public async Task<IEnumerable<Contact>> GetContactsAsync() =>
         await _connection.Table<Contact>().ToListAsync().ConfigureAwait(false);

      public Task DeleteContactAsync(Contact contact) => _connection.DeleteAsync(contact);

      public Task AddContactAsync(Contact contact) => _connection.InsertAsync(contact);

      public Task UpdateContactAsync(Contact contact) => _connection.UpdateAsync(contact);

      public Task<Contact> GetContactAsync(int id) => _connection.FindAsync<Contact>(id);
   }
}