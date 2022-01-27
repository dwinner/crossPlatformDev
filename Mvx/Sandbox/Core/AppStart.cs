using System.Threading.Tasks;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using MvvxSandboxApp.Core.ViewModels.State;

namespace MvvxSandboxApp.Core
{
   public class AppStart : MvxAppStart
   {
      public AppStart(IMvxApplication application, IMvxNavigationService navigationService)
         : base(application, navigationService)
      {
      }

      protected override Task NavigateToFirstViewModel(object hint = null) =>
         NavigationService.Navigate<UsaStateListViewModel>();
   }
}