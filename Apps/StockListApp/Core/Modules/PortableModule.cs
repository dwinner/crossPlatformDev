using Autofac;
using StockList.Core.Ioc;
using StockList.Core.ViewModels;
using StockList.Core.WebServices;

namespace StockList.Core.Modules
{
   public class PortableModule : IModule
   {
      public void Register(ContainerBuilder builder)
      {
         builder.RegisterType<MainPageViewModel>().SingleInstance();
         builder.RegisterType<StockListPageViewModel>().SingleInstance();
         builder.RegisterType<StockItemDetailsPageViewModel>().InstancePerDependency();
         builder.RegisterType<StockItemViewModel>().InstancePerDependency();
         builder.RegisterType<StocklistWebServiceControllerImpl>()
            .As<IStocklistWebServiceController>()
            .SingleInstance();
      }
   }
}