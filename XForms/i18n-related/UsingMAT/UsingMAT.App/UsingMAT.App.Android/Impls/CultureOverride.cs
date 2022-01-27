using System.Globalization;
using System.Threading;
using UsingMAT.App.Droid.Impls;
using UsingMAT.App.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(CultureOverride))]

namespace UsingMAT.App.Droid.Impls
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