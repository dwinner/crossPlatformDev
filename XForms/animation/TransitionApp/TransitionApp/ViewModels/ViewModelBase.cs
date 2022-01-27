using Prism.Mvvm;
using Prism.Navigation;

namespace TransitionApp.ViewModels
{
   public class ViewModelBase : BindableBase, INavigationAware, IDestructible
   {
      protected ViewModelBase(INavigationService navigationService) => NavigationService = navigationService;

      protected INavigationService NavigationService { get; }

      public virtual void Destroy()
      {
      }

      public virtual void OnNavigatedFrom(INavigationParameters parameters)
      {
      }

      public virtual void OnNavigatedTo(INavigationParameters parameters)
      {
      }

      public virtual void OnNavigatingTo(INavigationParameters parameters)
      {
      }
   }
}