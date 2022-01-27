using System;
using Android.App;
using Android.Runtime;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;

namespace connectivity.Droid
{
   [Application]
   public class App : Application
   {
      private static ViewModelLocator _locator;

      public App(IntPtr h, JniHandleOwnership jho) : base(h, jho)
      {
      }

      public static ViewModelLocator Locator
      {
         get
         {
            if (_locator == null)
            {
               // First time initialisation
               var navigation = new NavigationService();

               // configure the navigation service
               navigation.Configure(ViewModelLocator.MainKey, typeof(MainActivity));

               // register with the Navigation Service
               SimpleIoc.Default.Register<INavigationService>(() => navigation);

               _locator = new ViewModelLocator();
            }

            return _locator;
         }
      }
   }
}