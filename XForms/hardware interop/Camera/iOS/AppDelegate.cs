// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppDelegate.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2016 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Camera.Controls;
using Camera.iOS.Modules;
using Camera.Modules;
using Camera.Portable.Enums;
using Camera.Portable.Ioc;
using Camera.Portable.Modules;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace Camera.iOS
{
   /// <summary>
   ///    App delegate.
   /// </summary>
   [Register("AppDelegate")]
   public class AppDelegate : FormsApplicationDelegate
   {
      #region Public Methods

      /// <summary>
      ///    Finisheds the launching.
      /// </summary>
      /// <returns>The launching.</returns>
      /// <param name="app">App.</param>
      /// <param name="options">Options.</param>
      public override bool FinishedLaunching(UIApplication app, NSDictionary options)
      {
         Forms.Init();

         InitIoC();

         LoadApplication(new App());

         return base.FinishedLaunching(app, options);
      }

      #endregion


      /// <summary>
      ///    Dids the change status bar orientation.
      /// </summary>
      /// <param name="application">Application.</param>
      /// <param name="oldStatusBarOrientation">Old status bar orientation.</param>
      public override void DidChangeStatusBarOrientation(UIApplication application,
         UIInterfaceOrientation oldStatusBarOrientation)
      {
         // change listview opacity based upon orientation
         switch (UIApplication.SharedApplication.StatusBarOrientation)
         {
            case UIInterfaceOrientation.Portrait:
            case UIInterfaceOrientation.PortraitUpsideDown:
               OrientationPage.NotifyOrientationChange(Orientation.Portrait);
               break;
            case UIInterfaceOrientation.LandscapeLeft:
               OrientationPage.NotifyOrientationChange(Orientation.LandscapeLeft);
               break;
            case UIInterfaceOrientation.LandscapeRight:
               OrientationPage.NotifyOrientationChange(Orientation.LandscapeRight);
               break;
         }
      }

      #region Private Methods

      /// <summary>
      ///    Inits the IoC container and modules
      /// </summary>
      /// <returns>The io c.</returns>
      private void InitIoC()
      {
         IoC.CreateContainer();
         IoC.RegisterModule(new IOSModule());
         //IoC.RegisterModule (new SharedModule(false));
         IoC.RegisterModule(new XamFormsModule());
         IoC.RegisterModule(new PortableModule());
         IoC.StartContainer();
      }

      #endregion
   }
}