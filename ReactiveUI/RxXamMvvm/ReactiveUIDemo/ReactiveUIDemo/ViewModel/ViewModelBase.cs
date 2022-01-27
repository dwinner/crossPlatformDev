using ReactiveUI;
using Splat;

namespace ReactiveUIDemo.ViewModel
{
   public class ViewModelBase : ReactiveObject, IRoutableViewModel, ISupportsActivation
   {
      protected ViewModelBase(IScreen hostScreen = null) =>
         HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();

      public string UrlPathSegment { get; protected set; }

      public IScreen HostScreen { get; }

      public ViewModelActivator Activator { get; } = new ViewModelActivator();
   }
}