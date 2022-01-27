using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HelloTV.Enums;
using HelloTV.Models;
using Newtonsoft.Json;

namespace HelloTV.Helpers
{
   public static class WeatherServiceHelper
   {
      private const string AppId = "31fbd9047de24999e383c8d397c533b5";
      private static readonly HttpClient _HttpClient;

      static WeatherServiceHelper() =>
         _HttpClient = new HttpClient
         {
            BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/weather")
         };

      public static async Task<WeatherInfo> GetWeatherInfoAsync(string cityName, TemperatureUnit unit)
      {
         var getRequestUri = GetRequestUri(cityName, unit);
         var response = await _HttpClient.GetAsync(getRequestUri).ConfigureAwait(true);
         CheckStatusCode(response.StatusCode);
         var jsonString = await response.Content.ReadAsStringAsync().ConfigureAwait(true);

         return JsonConvert.DeserializeObject<WeatherInfo>(jsonString);
      }

      public static string GetTemperatureUnitString(TemperatureUnit unit)
      {
         string unitString;
         const char degSymbol = (char) 176;

         switch (unit)
         {
            case TemperatureUnit.Imperial:
               unitString = $"{degSymbol}F";
               break;

            case TemperatureUnit.Metric:
               unitString = $"{degSymbol}C";
               break;

            //case TemperatureUnit.Default:
            default:
               unitString = "K";
               break;
         }

         return unitString;
      }

      private static string GetRequestUri(string cityName, TemperatureUnit unit) =>
         $"?appId={AppId}"
         + $"&q={cityName}"
         + $"&{TemperatureUnitToQueryString(unit)}";

      private static string TemperatureUnitToQueryString(TemperatureUnit unit)
      {
         var queryString = "units=";

         switch (unit)
         {
            case TemperatureUnit.Imperial:
               queryString += "imperial";
               break;

            case TemperatureUnit.Metric:
               queryString += "metric";
               break;
         }

         return queryString;
      }

      private static void CheckStatusCode(HttpStatusCode statusCode)
      {
         if (statusCode != HttpStatusCode.OK)
         {
            throw new Exception($"Unexpected status code: {statusCode}");
         }
      }
   }
}