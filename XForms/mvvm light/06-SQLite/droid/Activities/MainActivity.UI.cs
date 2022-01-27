using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V7.Widget;

namespace SQLiteExample.Droid.Activities
{
   public partial class MainActivity
   {
      private TabLayout _slidingTabs;
      private Toolbar _toolBar;
      private ViewPager _vPager;

      public Toolbar Toolbar => _toolBar ?? (_toolBar = FindViewById<Toolbar>(Resource.Id.toolbar));
      public ViewPager VPager => _vPager ?? (_vPager = FindViewById<ViewPager>(Resource.Id.viewpager));

      public TabLayout SlidingTabs =>
         _slidingTabs ?? (_slidingTabs = FindViewById<TabLayout>(Resource.Id.sliding_tabs));
   }
}