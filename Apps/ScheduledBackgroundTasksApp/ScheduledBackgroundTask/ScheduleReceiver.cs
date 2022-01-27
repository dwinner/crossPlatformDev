using Android.Content;
using Android.Widget;

namespace ScheduledBackgroundTask
{
   [BroadcastReceiver]
   public class ScheduleReceiver : BroadcastReceiver
   {
      public override void OnReceive(Context context, Intent intent)
      {
         Toast.MakeText(context, "Received intent! You can run your background task here.", ToastLength.Short).Show();
      }
   }
}