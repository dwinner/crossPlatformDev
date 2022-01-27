using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using IslandMenu.App.Models;
using Newtonsoft.Json;

namespace IslandMenu.App.Services
{
   public class DataServices
   {
      private static readonly string[] _SupportedLocales = {"es", "de", "zh"};
      private static readonly string _Namespace;

      static DataServices() => _Namespace = typeof(DataServices).Namespace;

      public async Task<IEnumerable<Restaurant>> GetRestaurantsAsync(string locale)
      {
         var language = GetClosestLanguage(locale);
         var assembly = typeof(DataServices).GetTypeInfo().Assembly;
         var stream = assembly.GetManifestResourceStream($"{_Namespace}.data{language}.json") ??
                      assembly.GetManifestResourceStream($"{_Namespace}.data.json");

         string json;
         using (var reader = new StreamReader(stream))
         {
            json = await reader.ReadToEndAsync().ConfigureAwait(false);
         }

         var restaurants = JsonConvert.DeserializeObject<List<Restaurant>>(json);

         return restaurants;
      }

      private static string GetClosestLanguage(string locale)
      {
         var langs = _SupportedLocales;
         var result = string.Empty;

         foreach (var lang in langs)
         {
            if (locale.StartsWith(lang))
            {
               result = $"-{lang}";
               break;
            }
         }

         return result;
      }
   }
}