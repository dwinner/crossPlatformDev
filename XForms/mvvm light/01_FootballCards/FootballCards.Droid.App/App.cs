using System;
using Android.App;
using Android.Runtime;
using FootballCards.SharedLib;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;

namespace FootballCards.Droid.App
{
   [Application(Icon = "@mipmap/ic_launcher")]
   public class App : Application
   {
      private static ViewModelLocator _locator;

      public App(IntPtr handle, JniHandleOwnership ownership)
         : base(handle, ownership)
      {
      }

      public static ViewModelLocator Locator
      {
         get
         {
            if (_locator == null)
            {
               // First time init
               var navigationService = new NavigationService();

               // configure the navigation service
               navigationService.Configure(ViewModelLocator.MainPageKey, typeof(MainActivity));
               navigationService.Configure(ViewModelLocator.MapPageKey, typeof(MappingActivity));

               // register with the navigation service
               SimpleIoc.Default.Register<INavigationService>(() => navigationService);

               _locator = new ViewModelLocator();
            }

            return _locator;
         }
      }
   }
}