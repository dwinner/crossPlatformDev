using System.Globalization;
using System.Threading;
using IslandMenu.App.Interfaces;
using IslandMenu.App.iOS.Impl;
using Xamarin.Forms;

[assembly: Dependency(typeof(PlatformCultureInfo))]

namespace IslandMenu.App.iOS.Impl
{
   public class PlatformCultureInfo : ICultureInfo
   {
      public CultureInfo CurrentCulture
      {
         get => Thread.CurrentThread.CurrentCulture;
         set => Thread.CurrentThread.CurrentCulture = value;
      }

      public CultureInfo CurrentUiCulture
      {
         get => Thread.CurrentThread.CurrentUICulture;
         set => Thread.CurrentThread.CurrentUICulture = value;
      }
   }
}