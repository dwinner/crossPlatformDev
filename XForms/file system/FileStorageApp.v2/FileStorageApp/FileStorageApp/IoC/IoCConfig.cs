using System.Collections.Generic;
using Autofac;

namespace FileStorageApp.IoC
{
   /// <summary>
   ///    The IoC
   /// </summary>
   public static class IoCConfig
   {
      private static ContainerBuilder _Builder;

      private static IContainer Container { get; set; }

      public static void CreateContainer() => _Builder = new ContainerBuilder();

      public static void StartContainer() => Container = _Builder.Build();

      public static void RegisterModule(IModule module) => module.Register(_Builder);

      public static void RegisterModules(IEnumerable<IModule> modules)
      {
         foreach (var module in modules)
         {
            module.Register(_Builder);
         }
      }

      public static T Resolve<T>() => Container.Resolve<T>();
   }
}