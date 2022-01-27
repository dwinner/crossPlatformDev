using System;
using Android.App;
using Android.Runtime;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;

namespace Alert.Droid
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
               SimpleIoc.Default.Register<IDialogService, DialogService>();
               _locator = new ViewModelLocator();
            }

            return _locator;
         }
      }
   }
}