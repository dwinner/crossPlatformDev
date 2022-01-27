using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using JimBobBennett.MvvmLight.AppCompat;
using SQLiteExample.ViewModel;

namespace SQLiteExample.Droid.Activities
{
   public partial class DataPage : Fragment
   {
      private static ShowDataViewModel ViewModel => App.Locator.ShowDataVm;

      public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) =>
         inflater.Inflate(Resource.Layout.DataView, container, false);

      public override void OnViewCreated(View view, Bundle savedInstanceState)
      {
         base.OnViewCreated(view, savedInstanceState);
         LstView.Adapter =
            new ListViewAdapter.ListViewAdapter(AppCompatActivityBase.CurrentActivity, ViewModel.GetPersonInfo,
               ViewModel);
      }
   }
}