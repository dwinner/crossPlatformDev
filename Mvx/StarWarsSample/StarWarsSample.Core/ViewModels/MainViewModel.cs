using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace StarWarsSample.Core.ViewModels
{
   public class MainViewModel : BaseViewModel
   {
      public MainViewModel(IMvxNavigationService navigation)
      {
         ShowPeopleViewModelCommand =
            new MvxAsyncCommand(async () => await navigation.Navigate<PeopleViewModel>().ConfigureAwait(true));
         ShowPlanetsViewModelCommand =
            new MvxAsyncCommand(async () => await navigation.Navigate<PlanetsViewModel>().ConfigureAwait(true));
         ShowMenuViewModelCommand = new MvxAsyncCommand(async () =>
            await navigation.Navigate<MenuViewModel>().ConfigureAwait(true));
      }

      // MvvmCross Lifecycle

      // MVVM Properties

      // MVVM Commands
      public IMvxAsyncCommand ShowPeopleViewModelCommand { get; }
      public IMvxAsyncCommand ShowPlanetsViewModelCommand { get; }
      public IMvxAsyncCommand ShowMenuViewModelCommand { get; }

      // Private methods
   }
}