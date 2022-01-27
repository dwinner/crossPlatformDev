using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MvvmCrossDemo.Core.Infrastructure.Extensions
{
   public static class JsonExtentions
   {
      public static async Task<T> ReadAsJsonAsync<T>(this HttpResponseMessage response)
      {
         var jsonString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
         return jsonString.ToObject<T>();
      }

      public static T ToObject<T>(this string jsonString) => JsonConvert.DeserializeObject<T>(jsonString);

      public static StringContent ToStringContent<T>(this T obj, string contentType = "application/json")
      {
         var json = JsonConvert.SerializeObject(obj);
         return new StringContent(json, Encoding.UTF8, contentType);
      }
   }
}