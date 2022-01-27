using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace ScheduledBackgroundTask
{
   [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
   public class MainActivity : AppCompatActivity
   {
      private AlarmManager _alarm;
      private const int Interval = 5000;
      private PendingIntent _pendingIntent;
      private readonly bool _scheduleRunning = false;

      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);
         SetContentView(Resource.Layout.activity_main);

         var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
         SetSupportActionBar(toolbar);

         var fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
         fab.Click += FabOnClick;

         var intent = new Intent(this, typeof(ScheduleReceiver));
         _pendingIntent = PendingIntent.GetBroadcast(this, 0, intent, 0);
      }

      public override bool OnCreateOptionsMenu(IMenu menu)
      {
         MenuInflater.Inflate(Resource.Menu.menu_main, menu);
         return true;
      }

      public override bool OnOptionsItemSelected(IMenuItem item)
      {
         var id = item.ItemId;
         if (id == Resource.Id.action_settings)
         {
            return true;
         }

         return base.OnOptionsItemSelected(item);
      }

      private void FabOnClick(object sender, EventArgs eventArgs)
      {
         if (!_scheduleRunning)
         {
            //Starting the scheduled task
            _alarm = (AlarmManager) GetSystemService(AlarmService);
            _alarm.SetRepeating(AlarmType.RtcWakeup, DateTime.Now.TimeOfDay.Milliseconds, Interval, _pendingIntent);
            Toast.MakeText(this, "Schedule started", ToastLength.Short).Show();
         }
         else
         {
            if (_alarm != null)
            {
               _alarm.Cancel(_pendingIntent);
               Toast.MakeText(this, "Schedule stopped", ToastLength.Short).Show();
            }
         }
      }
   }
}