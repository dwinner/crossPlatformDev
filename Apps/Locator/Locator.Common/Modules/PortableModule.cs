using Autofac;
using Locator.Common.IoC;
using Locator.Common.Location;
using Locator.Common.ViewModels;
using Locator.Common.WebServices.GeocodingWebServiceController;

namespace Locator.Common.Modules
{
   /// <summary>
   ///    Portable module.
   /// </summary>
   public class PortableModule : IModule
   {
      /// <summary>
      ///    Register the specified builder.
      /// </summary>
      /// <param name="builder">builder.</param>
      public void Register(ContainerBuilder builder)
      {
         builder.RegisterType<MainPageViewModel>().SingleInstance();
         builder.RegisterType<MapPageViewModel>().SingleInstance();
         builder.RegisterType<Position>().As<IPosition>().SingleInstance();
         builder.RegisterType<GeocodingWebServiceController>().As<IGeocodingWebServiceController>().SingleInstance();
      }
   }
}