using System.Globalization;
using System.Threading;
using UsingMAT.App.Interfaces;
using UsingMAT.App.iOS.Impl;
using Xamarin.Forms;

[assembly: Dependency(typeof(CultureOverride))]

namespace UsingMAT.App.iOS.Impl
{
   public class CultureOverride : ICultureOverride
   {
      public void SetCultureOverride(string aCulture)
      {
         Thread.CurrentThread.CurrentCulture = new CultureInfo(aCulture);
         Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
      }
   }
}