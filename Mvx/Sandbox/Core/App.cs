using Acr.UserDialogs;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using MvvxSandboxApp.Core.Services.Implementations;
using MvvxSandboxApp.Core.Services.Interfaces;

namespace MvvxSandboxApp.Core
{
   public class App : MvxApplication
   {
      public override void Initialize()
      {
         CreatableTypes()
            .EndingWith("Service")
            .AsInterfaces()
            .RegisterAsLazySingleton();

         Mvx.IoCProvider.RegisterSingleton(() => UserDialogs.Instance);
         Mvx.IoCProvider.ConstructAndRegisterSingleton<IUsaStateStorage, UsaStateSqLiteStorageImpl>();

         // register the app start object
         RegisterCustomAppStart<AppStart>();
      }
   }
}