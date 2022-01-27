using MvvmCross.IoC;
using MvvmCross.ViewModels;

namespace AudioPlayerApp.Core
{
   public class App : MvxApplication
   {
      public override void Initialize()
      {
         CreatableTypes()
            .EndingWith("Service")
            .AsInterfaces()
            .RegisterAsLazySingleton();

         RegisterCustomAppStart<AppStart>();
      }
   }
}