using System.Threading.Tasks;
using NativeCustomDialogs.Droid;
using NativeCustomDialogs.Droid.Dialogs;
using NativeCustomDialogs.ViewModels;
using Plugin.CurrentActivity;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Dependency(typeof(CallDialog))]

namespace NativeCustomDialogs.Droid
{
   public class CallDialog : ICallDialog
   {
      async Task ICallDialog.CallDialog(object viewModel)
      {
         var activity = CrossCurrentActivity.Current.Activity as FormsAppCompatActivity;

         new CreateTodoDialog(viewModel as CreateTodoViewModel)
            .Show(activity.SupportFragmentManager, "CreateTodoDialog");
      }
   }
}