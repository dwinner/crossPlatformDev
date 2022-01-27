using System.IO;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace SatelliteMovingApp
{
   /// <summary>
   ///    Активность для экрана About
   /// </summary>
   [Activity(Label = "About")]
   public class AboutScreenActivity : Activity
   {
      public override bool OnCreateOptionsMenu(IMenu menu)
      {
         base.OnCreateOptionsMenu(menu);

         MenuInflater.Inflate(Resource.Menu.NavigateOptions, menu);
         menu.FindItem(Resource.Id.StartMenuItemId)
            .SetIntent(new Intent(this, typeof(StartScreenActivity)));
         menu.FindItem(Resource.Id.SettingsMenuItemId)
            .SetIntent(new Intent(this, typeof(SettingsScreenActivity)));
         menu.FindItem(Resource.Id.AboutMenuItemId)
            .SetIntent(new Intent(this, typeof(AboutScreenActivity)));

         return true;
      }

      public override bool OnOptionsItemSelected(IMenuItem item)
      {
         base.OnOptionsItemSelected(item);
         StartActivity(item.Intent);
         return true;
      }

      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);
         SetContentView(Resource.Layout.AboutScreen);

         try
         {
            var stream = Assets.Open("About.txt");
            using (var reader = new StreamReader(stream))
            {
               var content = reader.ReadToEnd();
               var helpTextView = FindViewById<TextView>(Resource.Id.HelpTextTextViewId);
               helpTextView.Text = content;
            }
         }
         catch (IOException ioEx)
         {
            Log.Error(typeof(AboutScreenActivity).Name, ioEx.Message, ioEx);
         }
      }
   }
}