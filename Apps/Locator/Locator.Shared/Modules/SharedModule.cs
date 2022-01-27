using System.Net;
using System.Net.Http;
using Autofac;
using Locator.Common.IoC;
using ModernHttpClient;

namespace Locator.App.Droid.Modules
{
   /// <summary>
   ///    Shared module.
   /// </summary>
   public sealed class SharedModule : IModule
   {
      /// <summary>
      ///    The is windows.
      /// </summary>
      private readonly bool _isWindows;

      /// <summary>
      ///    Initializes a new instance of the <see cref="T:Locator.App.Droid.Modules.SharedModule" /> class.
      /// </summary>
      /// <param name="isWindows">Is windows.</param>
      public SharedModule(bool isWindows) => _isWindows = isWindows;

      /// <summary>
      ///    Register the specified builder.
      /// </summary>
      /// <param name="builder">Builder.</param>
      public void Register(ContainerBuilder builder)
      {
         var clientHandler = _isWindows ? new HttpClientHandler() : new NativeMessageHandler();
         clientHandler.UseCookies = false;
         clientHandler.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
         builder.Register(cb => clientHandler).As<HttpClientHandler>().SingleInstance();
      }
   }
}