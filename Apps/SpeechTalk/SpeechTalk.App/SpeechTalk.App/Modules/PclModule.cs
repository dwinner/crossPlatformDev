using Autofac;
using SpeechTalk.App.IoC;
using SpeechTalk.App.Pages;
using SpeechTalk.App.ViewModels;

namespace SpeechTalk.App.Modules
{
   public class PclModule : IModule
   {
      public void Register(ContainerBuilder builder)
      {
         builder.RegisterType<MainPageViewModel>().SingleInstance();
         builder.RegisterType<MainPage>().SingleInstance();
      }
   }
}