using System.Collections.ObjectModel;
using Android.App;
using Android.OS;
using GalaSoft.MvvmLight.Views;

namespace TimeOfDeath.Droid
{
   [Activity(MainLauncher = true, Icon = "@drawable/icon")]
   public class MainActivity : ActivityBase
   {
      private readonly ObservableCollection<Fragment> fragments = new ObservableCollection<Fragment>();
      private ActionBar.Tab tab;

      protected override void OnCreate(Bundle bundle)
      {
         base.OnCreate(bundle);
         SetContentView(Resource.Layout.Main);
         ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

         fragments.Add(new TODFragment());
         fragments.Add(new ConditionsFragment());

         AddTabToActionBar("Time", Resource.Drawable.crucifix_colour);
         AddTabToActionBar("Conditions", Resource.Drawable.weather_colour);
      }

      private void AddTabToActionBar(string text, int iconResourceId)
      {
         tab = ActionBar.NewTab().SetTag(text).SetText(text).SetIcon(iconResourceId);

         /* uncomment and comment out one of the two below to see the difference in operation */

         tab.TabSelected += TabOnTabSelected;
         //tab.SetCommand<ActionBar.TabEventArgs>("TabSelected", TabClicked);
         ActionBar.AddTab(tab);
      }

      private void TabOnTabSelected(object sender, ActionBar.TabEventArgs tabEventArgs)
      {
         var tabNo = sender as ActionBar.Tab;
         var frag = fragments[tabNo.Position];
         tabEventArgs.FragmentTransaction.Replace(Resource.Id.frameLayout1, frag);
      }
   }
}