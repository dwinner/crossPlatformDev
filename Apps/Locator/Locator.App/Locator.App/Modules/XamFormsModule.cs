using System.Windows.Input;
using Autofac;
using Locator.App.Pages;
using Locator.App.UI;
using Locator.Common.IoC;
using Locator.Common.UI;
using Xamarin.Forms;

namespace Locator.App.Modules
{
   /// <summary>
   ///    Xamarin forms module.
   /// </summary>
   public class XamFormsModule : IModule
   {
      /// <summary>
      ///    Register the specified builder.
      /// </summary>
      /// <param name="builder">builder.</param>
      public void Register(ContainerBuilder builder)
      {
         builder.RegisterType<MainPage>().SingleInstance();
         builder.RegisterType<MapPage>().SingleInstance();
         builder.RegisterType<Command>().As<ICommand>().InstancePerDependency();
         builder.Register(x => new NavigationPage(x.Resolve<MainPage>())).AsSelf().SingleInstance();
         builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
      }
   }
}