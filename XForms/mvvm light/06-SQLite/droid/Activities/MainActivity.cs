using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Java.Lang;
using JimBobBennett.MvvmLight.AppCompat;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentManager = Android.Support.V4.App.FragmentManager;

namespace SQLiteExample.Droid.Activities
{
   [Activity(Label = "SQLiteExample", MainLauncher = true, Theme = "@style/Theme.AppCompat")]
   public partial class MainActivity : AppCompatActivityBase
   {
      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);

         // Set our view from the "main" layout resource
         SetContentView(Resource.Layout.TabLayout);

         var fragments = new Fragment[]
         {
            new GeneralInfoPage(),
            new DataPage()
         };

         var titles = CharSequence.ArrayFromStringArray(new[]
         {
            "General",
            "Data"
         });

         VPager.Adapter = new TabsFragmentPagerAdapter(SupportFragmentManager, fragments, titles);

         SlidingTabs.SetupWithViewPager(VPager);
      }

      private sealed class TabsFragmentPagerAdapter : FragmentPagerAdapter
      {
         private readonly Fragment[] _fragments;
         private readonly ICharSequence[] _titles;

         public TabsFragmentPagerAdapter(FragmentManager fm, Fragment[] frags, ICharSequence[] title) : base(fm)
         {
            _fragments = frags;
            _titles = title;
         }

         public override int Count => _fragments.Length;

         public override Fragment GetItem(int position) => _fragments[position];

         public override ICharSequence GetPageTitleFormatted(int position) => _titles[position];
      }
   }
}