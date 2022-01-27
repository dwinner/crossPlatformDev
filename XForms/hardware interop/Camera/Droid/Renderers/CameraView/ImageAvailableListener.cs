// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageAvailableListener.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2016 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using Android.Media;
using Object = Java.Lang.Object;

namespace Camera.Droid.Renderers.CameraView
{
   /// <summary>
   ///    This CameraCaptureSession.StateListener uses Action delegates to allow
   ///    the methods to be defined inline, as they are defined more than once.
   /// </summary>
   public class ImageAvailableListener : Object, ImageReader.IOnImageAvailableListener
   {
      /// <summary>
      ///    Ons the image available.
      /// </summary>
      /// <param name="reader">Reader.</param>
      public void OnImageAvailable(ImageReader reader)
      {
         Image image = null;

         try
         {
            image = reader.AcquireLatestImage();
            var buffer = image.GetPlanes()[0].Buffer;
            var imageData = new byte[buffer.Capacity()];
            buffer.Get(imageData);

            Photo?.Invoke(this, imageData);
         }
         catch (Exception ex)
         {
         }
         finally
         {
            if (image != null)
            {
               image.Close();
            }
         }
      }

      /// <summary>
      ///    Occurs when photo.
      /// </summary>
      public event EventHandler<byte[]> Photo;
   }
}