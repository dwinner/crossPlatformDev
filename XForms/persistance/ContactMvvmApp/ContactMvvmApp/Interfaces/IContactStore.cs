using System.Collections.Generic;
using System.Threading.Tasks;
using ContactMvvmApp.Models;

namespace ContactMvvmApp.Interfaces
{
   public interface IContactStore
   {
      Task<IEnumerable<Contact>> GetContactsAsync();

      Task<Contact> GetContactAsync(int id);

      Task AddContactAsync(Contact contact);

      Task UpdateContactAsync(Contact contact);

      Task DeleteContactAsync(Contact contact);
   }
}