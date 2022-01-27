using Autofac;
using Locator.App.Droid.Extras;
using Locator.App.Droid.Location;
using Locator.Common.Extras;
using Locator.Common.IoC;
using Locator.Common.Location;

namespace Locator.App.Droid.Modules
{
   /// <summary>
   ///    Droid module.
   /// </summary>
   public class DroidModule : IModule
   {
      /// <summary>
      ///    Register the specified builder.
      /// </summary>
      /// <param name="builder">builder.</param>
      public void Register(ContainerBuilder builder)
      {
         builder.RegisterType<GeolocatorDroid>().As<IGeolocator>().SingleInstance();
         builder.RegisterType<DroidMethods>().As<IMethods>().SingleInstance();
      }
   }
}