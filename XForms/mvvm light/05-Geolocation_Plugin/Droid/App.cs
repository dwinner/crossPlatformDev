using System;
using Android.App;
using Android.Runtime;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using geolocation_plugin.Droid.Activities;
using geolocation_plugin.Droid.Services;
using geolocation_plugin.Interfaces;

namespace geolocation_plugin.Droid
{
   [Application(Icon = "@mipmap/icon")]
   public class App : Application
   {
      private static ViewModelLocator _locator;

      public App(IntPtr h, JniHandleOwnership jho)
         : base(h, jho)
      {
      }

      public static ViewModelLocator Locator
      {
         get
         {
            if (_locator == null)
            {
               var nav = new NavigationService();

               // configure the navigation service
               nav.Configure(ViewModelLocator.MainKey, typeof(MainActivity));

               // register with the Navigation Service
               SimpleIoc.Default.Register<INavigationService>(() => nav);
               SimpleIoc.Default.Register<IDialogService, DialogService>();
               SimpleIoc.Default.Register<IGeolocation, Geolocation>();

               _locator = new ViewModelLocator();
            }

            return _locator;
         }
      }
   }
}