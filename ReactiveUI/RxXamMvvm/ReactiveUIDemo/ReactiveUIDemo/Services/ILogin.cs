using System.Threading.Tasks;

namespace ReactiveUIDemo.Services
{
   public interface ILogin
   {
      Task<bool> LoginAsync(string username, string password);
   }
}