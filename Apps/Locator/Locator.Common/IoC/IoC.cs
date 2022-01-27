using Autofac;

namespace Locator.Common.IoC
{
   /// <summary>
   ///    The IoC.
   /// </summary>
   public static class IoC
   {
      /// <summary>
      ///    The builder.
      /// </summary>
      private static ContainerBuilder _Builder;

      /// <summary>
      ///    Gets the container.
      /// </summary>
      /// <value>The container.</value>
      private static IContainer _Container;

      /// <summary>
      ///    Creates the container.
      /// </summary>
      /// <returns>The container.</returns>
      public static void CreateContainer() => _Builder = new ContainerBuilder();

      /// <summary>
      ///    Starts the container.
      /// </summary>
      /// <returns>The container.</returns>
      public static void StartContainer() => _Container = _Builder.Build();

      /// <summary>
      ///    Registers the module.
      /// </summary>
      /// <returns>The module.</returns>
      /// <param name="module">Module.</param>
      public static void RegisterModule(IModule module) => module.Register(_Builder);

      /// <summary>
      ///    Resolve this instance.
      /// </summary>
      /// <typeparam name="T">The 1st type parameter.</typeparam>
      public static T Resolve<T>() => _Container.Resolve<T>();
   }
}