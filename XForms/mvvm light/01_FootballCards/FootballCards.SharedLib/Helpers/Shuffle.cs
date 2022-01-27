using System;
using System.Collections.Generic;
using FootballCards.SharedLib.Model;

namespace FootballCards.SharedLib.Helpers
{
   public static class CardShuffle
   {
      private static readonly Random Rng = new Random();
      private static int _shuffles;

      public static List<Cards> Shuffle(this List<Cards> list, int shuffles)
      {
         if (_shuffles < shuffles)
         {
            var n = list.Count;
            while (n > 1)
            {
               n--;
               var k = Rng.Next(n + 1);
               var value = list[k];
               list[k] = list[n];
               list[n] = value;
            }

            _shuffles++;
            Shuffle(list, shuffles);
         }

         _shuffles = 0;
         return list;
      }
   }
}