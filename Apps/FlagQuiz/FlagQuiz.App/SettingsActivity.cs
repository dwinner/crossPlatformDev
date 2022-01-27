/**
 * Activity to display SettingsActivityFragment on a phone
 */

using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;

namespace FlagQuiz.App
{
   [Activity(
      //Name = "FlagQuiz.App.SettingsActivity",
      Label = "@string/title_activity_settings",
      ParentActivity = typeof(MainActivity),
      Theme = "@style/AppTheme.NoActionBar")]
   public class SettingsActivity : AppCompatActivity
   {
      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);

         SetContentView(Resource.Layout.activity_settings);
         var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
         SetSupportActionBar(toolbar);
         SupportActionBar.SetDisplayHomeAsUpEnabled(true);
      }
   }
}