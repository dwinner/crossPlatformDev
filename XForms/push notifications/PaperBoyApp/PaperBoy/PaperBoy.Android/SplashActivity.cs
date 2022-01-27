using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;

namespace PaperBoy.Droid
{
   [Activity(Label = "Paperboy", Theme = "@style/Theme.Splash", MainLauncher = true, NoHistory = true)]
   public class SplashActivity : Activity
   {
      public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistableState)
      {
         base.OnCreate(savedInstanceState, persistableState);

         // Create your application here
      }

      protected override void OnResume()
      {
         base.OnResume();

         var startupWork = new Task(async () => await Task.Delay(1000));
         startupWork.ContinueWith(t => { StartActivity(new Intent(Application.Context, typeof(MainActivity))); },
            TaskScheduler.FromCurrentSynchronizationContext());

         startupWork.Start();
      }
   }
}