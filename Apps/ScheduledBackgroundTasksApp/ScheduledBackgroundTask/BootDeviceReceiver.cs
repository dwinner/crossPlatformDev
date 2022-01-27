using Android.App;
using Android.Content;
using Android.Widget;

namespace ScheduledBackgroundTask
{
   [BroadcastReceiver(Enabled = true, Exported = true)]
   [IntentFilter(new[] {Intent.ActionBootCompleted})]
   public class BootDeviceReceiver : BroadcastReceiver
   {
      public override void OnReceive(Context context, Intent intent)
      {
         if (intent.Action.Equals("android.intent.action.BOOT_COMPLETED"))
         {
            Toast.MakeText(context, "1 Received intent! You can run your background task here.", ToastLength.Short)
               .Show();
            Toast.MakeText(context, "2 Received intent! You can run your background task here.", ToastLength.Short)
               .Show();
            Toast.MakeText(context, "3 Received intent! You can run your background task here.", ToastLength.Short)
               .Show();
            Toast.MakeText(context, "4 Received intent! You can run your background task here.", ToastLength.Short)
               .Show();
         }
      }
   }
}