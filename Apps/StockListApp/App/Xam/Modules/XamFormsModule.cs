using System.Windows.Input;
using Autofac;
using StockList.Core.Ioc;
using StockList.Core.UI;
using StockListApp.Xam.Pages;
using StockListApp.Xam.UI;
using Xamarin.Forms;

namespace StockListApp.Xam.Modules
{
   public class XamFormsModule : IModule
   {
      public void Register(ContainerBuilder builder)
      {
         builder.RegisterType<MainPage>().SingleInstance();
         builder.RegisterType<StocklistPage>().SingleInstance();
         builder.RegisterType<StockItemDetailsPage>().InstancePerDependency();
         builder.RegisterType<Command>().As<ICommand>().InstancePerDependency();
         builder.Register(ctx => new NavigationPage(ctx.Resolve<MainPage>())).AsSelf().SingleInstance();
         builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
      }
   }
}