using System.Globalization;
using IslandMenu.App.Interfaces;
using IslandMenu.App.UWP.Impl;
using Xamarin.Forms;

[assembly: Dependency(typeof(PlatformCultureInfo))]

namespace IslandMenu.App.UWP.Impl
{
   public class PlatformCultureInfo : ICultureInfo
   {
      public CultureInfo CurrentCulture
      {
         get => CultureInfo.CurrentCulture;
         set => CultureInfo.CurrentCulture = value;
      }

      public CultureInfo CurrentUiCulture
      {
         get => CultureInfo.CurrentUICulture;
         set => CultureInfo.CurrentUICulture = value;
      }
   }
}