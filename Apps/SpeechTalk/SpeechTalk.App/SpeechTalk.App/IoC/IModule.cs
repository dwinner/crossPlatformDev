using Autofac;

namespace SpeechTalk.App.IoC
{
   public interface IModule
   {
      void Register(ContainerBuilder builder);
   }
}