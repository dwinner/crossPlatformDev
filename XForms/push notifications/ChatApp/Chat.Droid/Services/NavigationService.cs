using Android.Content;
using Chat.Common;
using Chat.Common.Presenter;
using Chat.Droid.Views;

namespace Chat.Droid.Services
{
   /// <summary>
   ///    Navigation service.
   /// </summary>
   public class NavigationService : INavigationService
   {
      /// <summary>
      ///    At application.
      /// </summary>
      private readonly ChatApplication _application;

      /// <summary>
      ///    Initializes a new instance of the <see cref="NavigationService" /> class.
      /// </summary>
      /// <param name="application">Application.</param>
      public NavigationService(ChatApplication application) => _application = application;

      /// <summary>
      ///    Pushs the presenter.
      /// </summary>
      /// <returns>The presenter.</returns>
      /// <param name="presenter">Presenter.</param>
      public void PushPresenter(BasePresenter presenter)
      {
         var oldPresenter = _application.Presenter as BasePresenter;

         if (presenter != oldPresenter)
         {
            _application.Presenter = presenter;
            Intent intent = null;

            switch (presenter)
            {
               case LoginPresenter _:
                  intent = new Intent(_application.CurrentActivity, typeof(LoginActivity));
                  break;

               case ClientsListPresenter _:
                  intent = new Intent(_application.CurrentActivity, typeof(ClientsListActivity));
                  break;

               case ChatPresenter _:
                  intent = new Intent(_application.CurrentActivity, typeof(ChatActivity));
                  break;
            }

            if (intent != null)
            {
               _application.CurrentActivity.StartActivity(intent);
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
         _application.CurrentActivity.Finish();
      }
   }
}