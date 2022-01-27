using System;
using System.Collections.Generic;

namespace FlagQuiz.App
{
   public static class ListExtensions
   {
      private static readonly Random _Rng = new Random();

      public static void Shuffle<T>(this IList<T> list)
      {
         var n = list.Count;
         while (n > 1)
         {
            n--;
            var k = _Rng.Next(n + 1);
            var value = list[k];
            list[k] = list[n];
            list[n] = value;
         }
      }
   }
}