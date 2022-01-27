using System;
using ReactiveUI;
using ReactiveUIDemo.Services;
using ReactiveUIDemo.ViewModel;
using ReactiveUIDemo.Views;
using Splat;
using Xamarin.Forms;
using RoutedViewHost = ReactiveUI.XamForms.RoutedViewHost;

namespace ReactiveUIDemo
{
   public class AppBootsrapper : ReactiveObject, IScreen
   {
      public AppBootsrapper()
      {
         Router = new RoutingState();

         // You much register This as IScreen to represent your app's main screen
         Locator.CurrentMutable.RegisterConstant(this, typeof(IScreen));

         // We register the service in the locator
         Locator.CurrentMutable.Register(() => new LoginService(), typeof(ILogin));

         // Register the views 
         Locator.CurrentMutable.Register(() => new LoginPage(), typeof(IViewFor<LoginViewModel>));
         Locator.CurrentMutable.Register(() => new ItemsPage(), typeof(IViewFor<ItemsViewModel>));

         Router
            .NavigateAndReset
            .Execute(new LoginViewModel(Locator.CurrentMutable.GetService<ILogin>()))
            .Subscribe();
      }

      public RoutingState Router { get; }

      public Page CreateMainPage() => new RoutedViewHost();
   }
}