using NSubstitute;
using NUnit.Framework;
using OnlineUnitTesting.Interfaces;

namespace UnitTests
{
   [TestFixture]
   public class TestConnection
   {
      [Test]
      public void TestCase()
      {
         var connection = Substitute.For<IConnection>();
         connection.IsConnected().Returns(true);
      }
   }
}