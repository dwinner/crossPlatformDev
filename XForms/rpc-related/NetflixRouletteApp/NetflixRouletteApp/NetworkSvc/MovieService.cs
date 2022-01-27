using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NetflixRouletteApp.Models;
using Newtonsoft.Json;

namespace NetflixRouletteApp.NetworkSvc
{
   public class MovieService
   {
      private const string Url = "http://netflixroulette.net/api/api.php";
      internal const int MinSearchLength = 5;
      private readonly HttpClient _client = new HttpClient();

      public async Task<IEnumerable<Movie>> FindMoviesByActorAsync(string actor)
      {
         if (actor.Length < MinSearchLength)
         {
            return Enumerable.Empty<Movie>();
         }

         var response = await _client.GetAsync($"{Url}?actor={actor}")
            .ConfigureAwait(false);

         if (response.StatusCode == HttpStatusCode.NotFound)
         {
            return Enumerable.Empty<Movie>();
         }

         var content = await response.Content.ReadAsStringAsync()
            .ConfigureAwait(false);
         return JsonConvert.DeserializeObject<List<Movie>>(content);
      }

      public async Task<Movie> GetMovieAsync(string title)
      {
         var response = await _client.GetAsync($"{Url}?title={title}")
            .ConfigureAwait(false);

         if (response.StatusCode == HttpStatusCode.NotFound)
         {
            return null;
         }

         var content = await response.Content.ReadAsStringAsync()
            .ConfigureAwait(false);

         return JsonConvert.DeserializeObject<Movie>(content);
      }
   }
}