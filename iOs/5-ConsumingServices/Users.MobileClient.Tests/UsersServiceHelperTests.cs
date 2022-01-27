using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Users.MobileClient.Helpers;

namespace Users.MobileClient.Tests
{
   [TestFixture]
   public class UsersServiceHelperTests
   {
      private const int UserId = 5;

      private static async Task<int> GetUserCountAsync() =>
         (await UsersServiceHelper.GetAsync().ConfigureAwait(false)).Count();

      [Test]
      public async Task VerifyDelete()
      {
         // Arrange
         var currentDataCount = await GetUserCountAsync().ConfigureAwait(true);
         var expectedDataCount = currentDataCount - 1;

         // Act
         await UsersServiceHelper.DeleteAsync(UserId).ConfigureAwait(true);
         var actualDataCount = await GetUserCountAsync().ConfigureAwait(true);

         // Assert
         Assert.AreEqual(expectedDataCount, actualDataCount);
      }

      [Test]
      public async Task VerifyGet()
      {
         // Arrange
         const int expectedDataCount = 10;

         // Act and Assert
         Assert.AreEqual(expectedDataCount, await GetUserCountAsync().ConfigureAwait(true));
      }

      [Test]
      public async Task VerifyGetById()
      {
         // Arrange
         const string expectedName = "Chelsey Dietrich";
         const string expectedEmail = "Lucio_Hettinger@annie.ca";

         // Act
         var user = await UsersServiceHelper.GetAsync(UserId).ConfigureAwait(true);

         // Assert
         Assert.AreEqual(expectedName, user.Name);
         Assert.AreEqual(expectedEmail, user.Email);
      }

      [Test]
      public async Task VerifyUpdate()
      {
         // Arrange
         const string expectedName = "New name";
         var user = await UsersServiceHelper.GetAsync(UserId).ConfigureAwait(true);

         // Act
         // Update name of the user and then get the user again from the API
         user.Name = expectedName;
         await UsersServiceHelper.UpdateAsync(user).ConfigureAwait(true);
         user = await UsersServiceHelper.GetAsync(UserId).ConfigureAwait(true);

         // Assert
         // Check if the name of user was indeed updated
         Assert.AreEqual(expectedName, user.Name);
      }
   }
}