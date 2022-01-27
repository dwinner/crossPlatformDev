using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.RecyclerView.Widget;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using StarWarsSample.Core.Resources;
using StarWarsSample.Core.ViewModels;
using StarWarsSample.Droid.Extensions;

namespace StarWarsSample.Droid.Views
{
   [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, true)]
   [Register("starWarsSample.droid.views.PeopleView")]
   public class PeopleView : BaseFragment<PeopleViewModel>
   {
      protected override int FragmentId => Resource.Layout.PeopleView;

      public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
      {
         var view = base.OnCreateView(inflater, container, savedInstanceState);

         ParentActivity.SupportActionBar.Title = Strings.TargetPeople;

         var recyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.people_recycler_view);
         if (recyclerView != null)
         {
            recyclerView.HasFixedSize = true;
            var layoutManager = new LinearLayoutManager(Activity);
            recyclerView.SetLayoutManager(layoutManager);

            recyclerView.AddOnScrollFetchItemsListener(layoutManager, () => ViewModel.FetchPeopleTask,
               () => ViewModel.FetchPeopleCommand);
         }

         return view;
      }
   }
}