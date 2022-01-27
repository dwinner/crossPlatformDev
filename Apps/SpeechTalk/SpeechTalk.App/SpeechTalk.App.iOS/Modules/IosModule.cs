using Autofac;
using SpeechTalk.App.iOS.Services;
using SpeechTalk.App.IoC;
using SpeechTalk.App.Services;

namespace SpeechTalk.App.iOS.Modules
{
   public class IosModule : IModule
   {
      public void Register(ContainerBuilder builder)
      {
         builder.RegisterType<IosTextToSpeechImpl>().As<ITextToSpeech>().SingleInstance();
      }
   }
}