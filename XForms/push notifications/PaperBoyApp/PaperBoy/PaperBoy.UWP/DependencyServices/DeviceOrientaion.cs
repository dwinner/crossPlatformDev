using Windows.UI.ViewManagement;
using PaperBoy.Interfaces;
using PaperBoy.UWP.DependencyServices;
using Xamarin.Forms;

[assembly: Dependency(typeof(DeviceOrientaion))]

namespace PaperBoy.UWP.DependencyServices
{
   public class DeviceOrientaion : IDeviceOrentaion
   {
      public DeviceOrientations GetOrientation()
      {
         var orientation = ApplicationView.GetForCurrentView().Orientation;
         if (orientation == ApplicationViewOrientation.Landscape)
         {
            return DeviceOrientations.Landscape;
         }

         return DeviceOrientations.Portrait;
      }
   }
}