using Chat.Common;
using Chat.Common.Presenter;
using Chat.iOS.Views;
using UIKit;

namespace Chat.iOS.Services
{
   /// <summary>
   ///    Navigation service.
   /// </summary>
   public class NavigationService : INavigationService
   {
      /// <summary>
      ///    The navigation controller.
      /// </summary>
      private readonly UINavigationController _navigationController;

      /// <summary>
      ///    Initializes a new instance of the <see cref="NavigationService" /> class.
      /// </summary>
      /// <param name="navigationController">Navigation controller.</param>
      public NavigationService(UINavigationController navigationController) =>
         _navigationController = navigationController;

      /// <summary>
      ///    Pushs the presenter.
      /// </summary>
      /// <returns>The presenter.</returns>
      /// <param name="presenter">Presenter.</param>
      public void PushPresenter(BasePresenter presenter)
      {
         switch (presenter)
         {
            case LoginPresenter loginPresenter:
            {
               var viewController = new LoginViewController(loginPresenter);
               _navigationController.PushViewController(viewController, true);
               break;
            }

            case ClientsListPresenter listPresenter:
            {
               var viewController = new ClientsListViewController(listPresenter);
               _navigationController.PushViewController(viewController, true);
               break;
            }

            case ChatPresenter chatPresenter:
            {
               var viewController = new ChatViewController(chatPresenter);
               _navigationController.PushViewController(viewController, true);
               break;
            }
         }
      }

      /// <summary>
      ///    Pops the presenter.
      /// </summary>
      /// <returns>The presenter.</returns>
      /// <param name="animated">Animated.</param>
      public void PopPresenter(bool animated)
      {
         _navigationController.PopViewController(animated);
      }
   }
}