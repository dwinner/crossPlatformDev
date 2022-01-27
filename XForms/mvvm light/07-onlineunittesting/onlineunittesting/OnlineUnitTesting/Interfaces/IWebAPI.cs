using System.Threading.Tasks;

namespace OnlineUnitTesting.Interfaces
{
   public interface IWebApi<T>
   {
      Task<T> GetRequestAsync(string apiUrl);

      Task<T> PostRequestAsync(string apiUrl, T postObject);

      Task PutRequestAsync(string apiUrl, T putObject);

      Task DeleteRequestAsync(string apiUrl);

      Task<TV> PostRequestAsync<TV>(string apiUrl, T postObject) where TV : class;
   }
}