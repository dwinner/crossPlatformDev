using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using static System.StringComparison;

namespace SatelliteMovingApp
{
   /// <summary>
   ///    Активность для экрана главного меню
   /// </summary>
   [Activity(Label = "Main menu")]
   public class MenuScreenActivity : ListActivity
   {
      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);

         ListAdapter = new ArrayAdapter<string>(this,
            Resource.Layout.MainMenuItem,
            new[]
            {
               Resources.GetString(Resource.String.MenuItemStart),
               Resources.GetString(Resource.String.MenuItemSettings),
               Resources.GetString(Resource.String.MenuItemAbout)
            });
         ListView.ItemClick += (sender, args) =>
         {
            if (!(args.View is TextView clickedTextView))
               return;

            var text = clickedTextView.Text;
            var intent = GetCurrentIntent(text);
            if (intent != null)
               StartActivity(intent);
         };
      }

      private Intent GetCurrentIntent(string text)
      {
         Intent intent = null;
         if (text.Equals(Resources.GetString(Resource.String.MenuItemStart), CurrentCultureIgnoreCase))
            intent = new Intent(this, typeof(StartScreenActivity));
         else if (text.Equals(Resources.GetString(Resource.String.MenuItemSettings), CurrentCultureIgnoreCase))
            intent = new Intent(this, typeof(SettingsScreenActivity));
         else if (text.Equals(Resources.GetString(Resource.String.MenuItemAbout), CurrentCultureIgnoreCase))
            intent = new Intent(this, typeof(AboutScreenActivity));

         return intent;
      }
   }
}