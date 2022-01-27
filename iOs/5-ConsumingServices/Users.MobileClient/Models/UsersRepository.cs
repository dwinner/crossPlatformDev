using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.MobileClient.Helpers;

namespace Users.MobileClient.Models
{
   public class UsersRepository
   {
      private static UsersRepository _instance;

      private UsersRepository()
      {
      }

      public List<User> Users { get; private set; }

      public static async Task<UsersRepository> GetInstanceAsync()
      {
         // Create and initialize an instance only once
         if (_instance == null)
         {
            _instance = _instance ?? new UsersRepository();
            _instance.Users = (await UsersServiceHelper.GetAsync().ConfigureAwait(true)).ToList();
         }

         return _instance;
      }

      public User GetById(int userId) => Users.Find(u => u.Id == userId);

      private bool UserExists(int userId) => Users.Exists(u => u.Id == userId);

#pragma warning disable CS4014

      public void Delete(int userId)
      {
         if (!UserExists(userId))
         {
            return;
         }

         var userToDelete = GetById(userId);
         Users.Remove(userToDelete);
         UsersServiceHelper.DeleteAsync(userId);
      }

      public void Update(User user)
      {
         if (UserExists(user.Id))
         {
            UsersServiceHelper.UpdateAsync(user);
         }
      }

#pragma warning restore CS4014
   }
}