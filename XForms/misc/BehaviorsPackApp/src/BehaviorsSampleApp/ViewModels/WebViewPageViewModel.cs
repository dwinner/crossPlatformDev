using Xamarin.Forms;

namespace BehaviorsSampleApp.ViewModels
{
   public class WebViewPageViewModel
   {
      public Command<string> NavigatedCommand => new Command<string>(x => { });
   }
}