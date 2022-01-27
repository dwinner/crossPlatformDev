using Java.Text;
using Java.Util;

namespace WeatherViewer.App
{
   /// <summary>
   ///    Информация о погоде за один день
   /// </summary>
   public class Weather
   {
      private const string UnicodeDegreeSymbol = "\u00B0F";

      public Weather(long timeStamp, double minTemp, double maxTemp, double humidity, string description,
         string iconName)
      {
         var numberFormat = NumberFormat.Instance;
         numberFormat.MaximumFractionDigits = 0;

         DayOfWeek = ConvertTimeStampToDay(timeStamp);
         MinTemp = $"{numberFormat.Format(minTemp)}{UnicodeDegreeSymbol}";
         MaxTemp = $"{numberFormat.Format(maxTemp)}{UnicodeDegreeSymbol}";
         Humidity = NumberFormat.PercentInstance.Format(humidity / 100.0);
         Description = description;
         IconUrl = $"http://openweathermap.org/img/w/{iconName}.png";
      }

      public string DayOfWeek { get; }

      public string MinTemp { get; }

      public string MaxTemp { get; }

      public string Humidity { get; }

      public string Description { get; }

      public string IconUrl { get; }

      /// <summary>
      ///    Преобразование временной метки в название дня недели (Monday, ...)
      /// </summary>
      /// <param name="timeStamp">Время в секундах (с 1970)</param>
      /// <returns>Название дня недели</returns>
      private static string ConvertTimeStampToDay(long timeStamp)
      {
         var calendar = Calendar.Instance; // Объект Calendar
         calendar.TimeInMillis = timeStamp * 1000; // Получение времени
         var defaultTimeZone = TimeZone.Default; // Часовой пояс устройства
         calendar.Add(CalendarField.Millisecond,
            defaultTimeZone.GetOffset(calendar.TimeInMillis)); // Поправка на часовой пояс устройства
         var dateFormatter = new SimpleDateFormat("EEEE"); // Объект SimpleDateFormat, возвращающий название дня недели

         return dateFormatter.Format(calendar.Time);
      }
   }
}