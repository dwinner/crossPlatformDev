using Autofac;
using SpeechTalk.App.Droid.Services;
using SpeechTalk.App.IoC;
using SpeechTalk.App.Services;

namespace SpeechTalk.App.Droid.Modules
{
   public class DroidModule : IModule
   {
      public void Register(ContainerBuilder builder)
      {
         builder.RegisterType<DroidTextToSpeechImpl>().As<ITextToSpeech>().SingleInstance();
      }
   }
}