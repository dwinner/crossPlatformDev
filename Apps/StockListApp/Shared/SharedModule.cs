using System.Net;
using System.Net.Http;
using Autofac;
using ModernHttpClient;
using StockList.Core.Ioc;

namespace StockList.Shared
{
   public sealed class SharedModule : IModule
   {
      private readonly bool _isWindows;

      public SharedModule(bool isWindows) => _isWindows = isWindows;

      public void Register(ContainerBuilder builder)
      {
         var clientHandler = _isWindows ? new HttpClientHandler() : new NativeMessageHandler();
         clientHandler.UseCookies = false;
         clientHandler.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
         builder.Register(cb => clientHandler).As<HttpClientHandler>().SingleInstance();
      }
   }
}