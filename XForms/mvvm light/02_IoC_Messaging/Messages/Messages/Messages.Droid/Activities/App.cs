using System;
using Android.App;
using Android.Runtime;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;

namespace Messages.Droid.Activities
{
   [Application(Icon = "@drawable/icon")]
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
               // First time initialisation
               var navigationService = new NavigationService();

               // configure the navigation service
               navigationService.Configure(ViewModelLocator.MainViewKey, typeof(MainActivity));
               navigationService.Configure(ViewModelLocator.SecondViewKey, typeof(SecondActivity));

               // register with the Navigation Service
               SimpleIoc.Default.Register<INavigationService>(() => navigationService);

               _locator = new ViewModelLocator();
            }

            return _locator;
         }
      }
   }
}