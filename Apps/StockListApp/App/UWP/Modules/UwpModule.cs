using Autofac;
using StockList.Core.Extras;
using StockList.Core.Ioc;
using StockListApp.UWP.Extras;

namespace StockListApp.UWP.Modules
{
   public class UwpModule : IModule
   {
      public void Register(ContainerBuilder builder) =>
         builder.RegisterType<UwpMethods>().As<IMethods>().SingleInstance();
   }
}