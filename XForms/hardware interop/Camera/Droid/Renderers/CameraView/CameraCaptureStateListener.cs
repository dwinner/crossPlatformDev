// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CameraCaptureStateListener.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2016 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using Android.Hardware.Camera2;

namespace Camera.Droid.Renderers.CameraView
{
   /// <summary>
   ///    This CameraCaptureSession.StateListener uses Action delegates to allow
   ///    the methods to be defined inline, as they are defined more than once.
   /// </summary>
   public class CameraCaptureStateListener : CameraCaptureSession.StateCallback
   {
      /// <summary>
      ///    The on configured action.
      /// </summary>
      public Action<CameraCaptureSession> OnConfiguredAction;

      /// <summary>
      ///    The on configure failed action.
      /// </summary>
      public Action<CameraCaptureSession> OnConfigureFailedAction;

      /// <summary>
      ///    Ons the configure failed.
      /// </summary>
      /// <param name="session">Session.</param>
      public override void OnConfigureFailed(CameraCaptureSession session)
      {
         if (OnConfigureFailedAction != null)
         {
            OnConfigureFailedAction(session);
         }
      }

      /// <summary>
      ///    Ons the configured.
      /// </summary>
      /// <param name="session">Session.</param>
      public override void OnConfigured(CameraCaptureSession session)
      {
         if (OnConfiguredAction != null)
         {
            OnConfiguredAction(session);
         }
      }
   }
}