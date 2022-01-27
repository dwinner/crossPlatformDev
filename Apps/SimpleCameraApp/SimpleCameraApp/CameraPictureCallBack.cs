using System.IO;
using Android.Content;
using Android.Hardware;
using Android.Net;
using Android.Util;
using Java.Lang;
using Exception = System.Exception;

namespace SimpleCameraApp
{
   public class CameraPictureCallBack : Object, Camera.IPictureCallback
   {
      private const string APP_NAME = "SimpleCameraApp";
      private readonly Context _context;

      public CameraPictureCallBack(Context cont) => _context = cont;

      /// <summary>
      ///    Callback when the picture is taken by the Camera
      /// </summary>
      /// <param name="data"></param>
      /// <param name="camera"></param>
      public void OnPictureTaken(byte[] data, Camera camera)
      {
         try
         {
            var fileName = Uri.Parse("test.jpg").LastPathSegment;
            var os = _context.OpenFileOutput(fileName, FileCreationMode.Private);
            var binaryWriter = new BinaryWriter(os);
            binaryWriter.Write(data);
            binaryWriter.Close();

            //We start the camera preview back since after taking a picture it freezes
            camera.StartPreview();
         }
         catch (Exception e)
         {
            Log.Debug(APP_NAME, "File not found: " + e.Message);
         }
      }
   }
}