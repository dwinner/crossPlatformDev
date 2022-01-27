using Windows.ApplicationModel.Resources.Core;
using Windows.Globalization;
using UsingMAT.App.Interfaces;
using UsingMAT.App.UWP.Impl;
using Xamarin.Forms;

[assembly: Dependency(typeof(CultureOverride))]

namespace UsingMAT.App.UWP.Impl
{
   public class CultureOverride : ICultureOverride
   {
      public void SetCultureOverride(string aCulture)
      {
         ApplicationLanguages.PrimaryLanguageOverride = aCulture;
         ResourceContext.GetForCurrentView().Reset();
      }
   }
}