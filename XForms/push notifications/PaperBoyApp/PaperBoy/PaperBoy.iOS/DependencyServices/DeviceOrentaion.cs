using PaperBoy.Interfaces;
using PaperBoy.iOS.DependencyServices;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(DeviceOrentaion))]

namespace PaperBoy.iOS.DependencyServices
{
   public class DeviceOrentaion : IDeviceOrentaion
   {
      public DeviceOrientations GetOrientation()
      {
         var currentOrientation = UIApplication.SharedApplication.StatusBarOrientation;
         var isPortrait = currentOrientation == UIInterfaceOrientation.Portrait
                          || currentOrientation == UIInterfaceOrientation.PortraitUpsideDown;

         return isPortrait ? DeviceOrientations.Portrait : DeviceOrientations.Landscape;
      }
   }
}