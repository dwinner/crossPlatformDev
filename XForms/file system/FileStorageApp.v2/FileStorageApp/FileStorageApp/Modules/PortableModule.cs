using Autofac;
using FileStorageApp.DataAccess.Storage;
using FileStorageApp.IoC;
using FileStorageApp.ViewModels;

namespace FileStorageApp.Modules
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
         builder.RegisterType<SqLiteStorage>().As<ISqLiteStorage>().SingleInstance();
         builder.RegisterType<MainPageViewModel>().SingleInstance();
         builder.RegisterType<FilesPageViewModel>().SingleInstance();
         builder.RegisterType<EditFilePageViewModel>().SingleInstance();
         builder.RegisterType<FileItemViewModel>().InstancePerDependency();
      }
   }
}