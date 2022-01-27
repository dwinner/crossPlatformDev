using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Foundation;
using HelloTV.Models;
using UIKit;

namespace HelloTV.Helpers
{
   public static class IconHelper
   {
      private static readonly HttpClient _HttpClient;

      private const string BaseAddress = "http://openweathermap.org/img/w/";
      private const string IconExtension = ".png";

      static IconHelper() => _HttpClient = new HttpClient();

      public static async Task<UIImage> GetIconAsync(WeatherInfo weatherInfo)
      {
         var iconUrl = GetIconUrl(weatherInfo);
         var imageData = await _HttpClient.GetByteArrayAsync(iconUrl).ConfigureAwait(true);

         return UIImage.LoadFromData(NSData.FromArray(imageData));
      }

      private static string GetIconUrl(WeatherInfo weatherInfo)
      {
         var iconName = weatherInfo.Weather.FirstOrDefault().Icon;
         return $"{BaseAddress}{iconName}{IconExtension}";
      }
   }
}