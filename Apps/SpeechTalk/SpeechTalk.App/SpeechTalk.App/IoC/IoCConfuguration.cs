using Autofac;

namespace SpeechTalk.App.IoC
{
   public static class IoCConfuguration
   {
      private static ContainerBuilder _builder;

      private static IContainer Container { get; set; }

      public static void CreateContainer()
      {
         _builder = new ContainerBuilder();
      }

      public static void StartContainer()
      {
         Container = _builder.Build();
      }

      public static void RegisterModule(IModule module)
      {
         module.Register(_builder);
      }

      public static T Resolve<T>() => Container.Resolve<T>();
   }
}