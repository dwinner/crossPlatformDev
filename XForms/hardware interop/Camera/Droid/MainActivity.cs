// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainActivity.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2016 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Android.App;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;
using Camera.Controls;
using Camera.Droid.Modules;
using Camera.Modules;
using Camera.Portable.Ioc;
using Camera.Portable.Modules;
using Xamarin.Forms;
using Debug = System.Diagnostics.Debug;

namespace Camera.Droid
{
   /// <summary>
   ///    Main activity.
   /// </summary>
   [Activity(Label = "Camera.Droid", Icon = "@drawable/icon", MainLauncher = true,
      ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
   public class MainActivity : Xamarin.Forms.Platform.Android.FormsApplicationActivity
   {
      #region Protected Methods

      /// <summary>
      ///    Called when the activity is created.
      /// </summary>
      /// <returns>The create.</returns>
      /// <param name="bundle">Bundle.</param>
      protected override void OnCreate(Bundle bundle)
      {
         base.OnCreate(bundle);
         Forms.Init(this, bundle);

         InitIoC();

         LoadApplication(new App());
      }

      #endregion

      /// <summary>
      ///    Ons the configuration changed.
      /// </summary>
      /// <param name="newConfig">New config.</param>
      public override void OnConfigurationChanged(Configuration newConfig)
      {
         base.OnConfigurationChanged(newConfig);

         Debug.WriteLine("MainActivity: Orientation changed. " + newConfig.Orientation);

         switch (newConfig.Orientation)
         {
            case Orientation.Portrait:
               OrientationPage.NotifyOrientationChange(Portable.Enums.Orientation.Portrait);
               break;
            case Orientation.Landscape:
               OrientationPage.NotifyOrientationChange(Portable.Enums.Orientation.LandscapeLeft);
               break;
         }
      }

      #region Private Methods

      /// <summary>
      ///    Inits the IoC container and modules.
      /// </summary>
      /// <returns>The io c.</returns>
      private void InitIoC()
      {
         IoC.CreateContainer();
         IoC.RegisterModule(new DroidModule());
         IoC.RegisterModule(new XamFormsModule());
         IoC.RegisterModule(new PortableModule());
         IoC.StartContainer();
      }

      #endregion
   }
}