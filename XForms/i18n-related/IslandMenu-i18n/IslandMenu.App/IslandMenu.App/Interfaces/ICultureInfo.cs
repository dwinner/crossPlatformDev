using System.Globalization;

namespace IslandMenu.App.Interfaces
{
   public interface ICultureInfo
   {
      CultureInfo CurrentCulture { get; set; }

      CultureInfo CurrentUiCulture { get; set; }
   }
}