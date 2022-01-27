using System;

namespace TimeOfDeath.Extensions
{
   public static class ShortDates
   {
      public static string ToShortDateString(this DateTime date) => date.Date.ToString("dd/MM/YYYY");

      public static string ToShortTimeString(this DateTime time) => $"{time.Hour}:{time.Minute}";
   }
}