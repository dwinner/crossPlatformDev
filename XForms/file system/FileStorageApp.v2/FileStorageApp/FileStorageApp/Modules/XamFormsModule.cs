using System;
using System.Windows.Input;
using Autofac;
using FileStorageApp.IoC;
using FileStorageApp.Pages;
using FileStorageApp.UI;
using Xamarin.Forms;

namespace FileStorageApp.Modules
{
   public class XamFormsModule : IModule
   {
      public void Register(ContainerBuilder builder)
      {
         builder.RegisterType<MainPage>().SingleInstance();
         builder.RegisterType<FilesPage>().SingleInstance();
         builder.RegisterType<EditFilePage>().SingleInstance();
         builder.RegisterType<Command>().As<ICommand>().InstancePerDependency();
         builder.Register(x => new NavigationPage(x.Resolve<MainPage>())).AsSelf().SingleInstance();
         //builder.RegisterInstance<Func<Page>>(() => ((NavigationPage)Application.Current.MainPage).CurrentPage);
         builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
      }
   }
}