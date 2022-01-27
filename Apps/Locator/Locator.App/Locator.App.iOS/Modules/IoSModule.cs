using Autofac;
using Locator.App.iOS.Extras;
using Locator.App.iOS.Location;
using Locator.Common.Extras;
using Locator.Common.IoC;
using Locator.Common.Location;

namespace Locator.App.iOS.Modules
{
   /// <summary>
   ///    IOS Module.
   /// </summary>
   public class IosModule : IModule
   {
      /// <summary>
      ///    Register the specified builder.
      /// </summary>
      /// <param name="builder">builder.</param>
      public void Register(ContainerBuilder builder)
      {
         builder.RegisterType<GeolocatorIos>().As<IGeolocator>().SingleInstance();
         builder.RegisterType<IosMethods>().As<IMethods>().SingleInstance();
      }
   }
}