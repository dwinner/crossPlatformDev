using Android.App;
using Android.OS;
using Android.Support.V7.App;

namespace AppDevUnited.WelcomeApp
{
   [Activity(Label = "WelcomeApp", MainLauncher = true)]
   public class MainActivity : AppCompatActivity
   {
      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);         
         SetContentView(Resource.Layout.Main);
      }
   }
}