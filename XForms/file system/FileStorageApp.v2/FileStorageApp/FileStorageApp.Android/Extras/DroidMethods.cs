using System.Threading.Tasks;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using FileStorageApp.Methods;
using Xamarin.Forms;

namespace FileStorageApp.Droid.Extras
{
   public class DroidMethods : IMethods
   {
      public void Exit() => Process.KillProcess(Process.MyPid());

      public void DisplayEntryAlert(TaskCompletionSource<string> puppetTask, string message)
      {
#pragma warning disable 618
         var context = Forms.Context;
#pragma warning restore 618

         var factory = LayoutInflater.From(context);
         var view = factory.Inflate(Resource.Layout.EntryAlertView, null);

         var editText = view.FindViewById<EditText>(Resource.Id.textEntry);

         new AlertDialog.Builder(context)
            .SetTitle("Chat")
            .SetMessage(message)
            .SetPositiveButton("Ok", (sender, e) => { puppetTask.SetResult(editText.Text); })
            .SetNegativeButton("Cancel", (sender, e) => { puppetTask.SetResult(null); })
            .SetView(view)
            .Show();
      }
   }
}