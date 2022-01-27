using Autofac;
using SpeechTalk.App.IoC;
using SpeechTalk.App.Services;
using SpeechTalk.App.UWP.Services;

namespace SpeechTalk.App.UWP.Modules
{
   public class WinPhoneModule :IModule
   {
      public void Register(ContainerBuilder builder)
      {
         builder.RegisterType<WinPhoneTextToSpeechImpl>().As<ITextToSpeech>().SingleInstance();
      }
   }
}