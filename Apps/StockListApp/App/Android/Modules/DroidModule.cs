using Autofac;
using StockList.Core.Extras;
using StockList.Core.Ioc;
using StockListApp.Droid.Extras;

namespace StockListApp.Droid.Modules
{
   public class DroidModule : IModule
   {
      public void Register(ContainerBuilder builder) =>
         builder.RegisterType<DroidMethodsImpl>().As<IMethods>().SingleInstance();
   }
}