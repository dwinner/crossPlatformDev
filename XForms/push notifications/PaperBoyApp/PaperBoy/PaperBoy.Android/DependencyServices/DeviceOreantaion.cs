using Android.Content;
using Android.Runtime;
using Android.Views;
using PaperBoy.Droid.DependencyServices;
using PaperBoy.Interfaces;
using Xamarin.Forms;
using Application = Android.App.Application;

[assembly: Dependency(typeof(DeviceOreantaion))]

namespace PaperBoy.Droid.DependencyServices
{
   public class DeviceOreantaion : IDeviceOrentaion
   {
      public DeviceOrientations GetOrientation()
      {
         var windowManager = Application.Context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();

         var rotation = windowManager.DefaultDisplay.Rotation;
         var isLandScape = rotation == SurfaceOrientation.Rotation90 || rotation == SurfaceOrientation.Rotation270;
         return isLandScape ? DeviceOrientations.Landscape : DeviceOrientations.Portrait;
      }
   }
}