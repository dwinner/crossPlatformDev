using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UIKit;

namespace ColorsTable.Colors
{
   public static class ColorsHelper
   {
      public static List<Color> GetColors()
      {
         var uiColorType = typeof(UIColor);
         var availableColors = uiColorType.GetProperties(BindingFlags.Public | BindingFlags.Static);

         return availableColors
            .Select(color => new Color
            {
               Name = color.Name,
               Value = color.GetValue(uiColorType) as UIColor
            }).ToList();
      }
   }
}