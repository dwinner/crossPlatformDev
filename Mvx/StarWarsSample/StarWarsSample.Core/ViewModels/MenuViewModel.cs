using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace StarWarsSample.Core.ViewModels
{
   public class MenuViewModel : BaseViewModel
   {
      public MenuViewModel(IMvxNavigationService navigation)
      {
         ShowPlanetsCommand =
            new MvxAsyncCommand(async () => await navigation.Navigate<PlanetsViewModel>().ConfigureAwait(true));
         ShowPeopleCommand =
            new MvxAsyncCommand(async () => await navigation.Navigate<PeopleViewModel>().ConfigureAwait(true));
         ShowStatusCommand =
            new MvxAsyncCommand(async () => await navigation.Navigate<StatusViewModel>().ConfigureAwait(true));
      }

      // MvvmCross Lifecycle

      // MVVM Properties

      // MVVM Commands
      public IMvxCommand ShowStatusCommand { get; }
      public IMvxCommand ShowPlanetsCommand { get; }
      public IMvxCommand ShowPeopleCommand { get; }

      // Private methods
   }
}