using Autofac;

namespace StockList.Core.Ioc
{
   /// <summary>
   ///    The IoC container
   /// </summary>
   public static class IoC
   {
      private static ContainerBuilder _builder;

      public static IContainer Container { get; private set; }

      public static void CreateContainer() => _builder = new ContainerBuilder();

      public static void StartContainer() => Container = _builder.Build();

      public static void RegisterModule(IModule module) => module.Register(_builder);

      public static T Resolve<T>() => Container.Resolve<T>();
   }
}