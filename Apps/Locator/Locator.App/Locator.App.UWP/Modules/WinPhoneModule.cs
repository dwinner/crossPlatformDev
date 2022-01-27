using Autofac;
using Locator.App.UWP.Extras;
using Locator.App.UWP.Location;
using Locator.Common.Extras;
using Locator.Common.IoC;
using Locator.Common.Location;

namespace Locator.App.UWP.Modules
{
   /// <summary>
   ///    Windows phone module.
   /// </summary>
   public class WinPhoneModule : IModule
   {
      /// <summary>
      ///    Register the specified builder.
      /// </summary>
      /// <param name="builder">builder.</param>
      public void Register(ContainerBuilder builder)
      {
         builder.RegisterType<GeolocatorWinPhone>().As<IGeolocator>().SingleInstance();
         builder.RegisterType<WinPhoneMethods>().As<IMethods>().SingleInstance();
      }
   }
}