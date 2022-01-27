using Autofac;
using FileStorageApp.DataAccess.Storage;
using FileStorageApp.Droid.DataAccess;
using FileStorageApp.Droid.Extras;
using FileStorageApp.Droid.Logging;
using FileStorageApp.IoC;
using FileStorageApp.Logging;
using FileStorageApp.Methods;
using SQLite.Net.Interop;
using SQLite.Net.Platform.XamarinAndroid;

namespace FileStorageApp.Droid.Modules
{
   public class DroidModule : IModule
   {
      public void Register(ContainerBuilder builder)
      {
         builder.RegisterType<DroidMethods>().As<IMethods>().SingleInstance();
         builder.RegisterType<LoggerDroid>().As<ILogger>().SingleInstance();

         builder.RegisterType<SqLiteSetup>().As<ISqLiteSetup>().SingleInstance();
         builder.RegisterType<SQLitePlatformAndroid>().As<ISQLitePlatform>().SingleInstance();
      }
   }
}