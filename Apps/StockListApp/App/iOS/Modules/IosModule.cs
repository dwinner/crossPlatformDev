using Autofac;
using StockList.Core.Extras;
using StockList.Core.Ioc;
using StockListApp.iOS.Extras;

namespace StockListApp.iOS.Modules
{
   public class IosModule : IModule
   {
      public void Register(ContainerBuilder builder) =>
         builder.RegisterType<IOSMethods>().As<IMethods>().SingleInstance();
   }
}