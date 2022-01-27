using System.Windows.Input;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Helpers;
using SQLiteExample.ViewModel;

namespace SQLiteExample.Droid.Activities
{
   public partial class HobbiesPage : Fragment
   {
      private static AddHobbiesViewModel ViewModel => App.Locator.AddHobbiesVm;

      private ICommand RecordAndBack => new RelayCommand(() =>
      {
         ViewModel.BtnCommit.Execute(null);
         MoveBackFragment();
      });

      public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) =>
         inflater.Inflate(Resource.Layout.Hobby, container, false);

      public override void OnViewCreated(View view, Bundle savedInstanceState)
      {
         base.OnViewCreated(view, savedInstanceState);
         BindEditText();
         BindSpinner();
         BindButtons();
      }

      private void BindEditText()
      {
         this.SetBinding(
            () => ViewModel.HobbyName,
            () => EditName.Text,
            BindingMode.TwoWay);

         this.SetBinding(
            () => ViewModel.HobbyDesc,
            () => EditDesc.Text,
            BindingMode.TwoWay);

         this.SetBinding(
            () => ViewModel.HobbyCost,
            () => EditCost.Text,
            BindingMode.TwoWay);
      }

      private void BindSpinner()
      {
         var freqAdapter = new ArrayAdapter<string>(EditCost.Context, Android.Resource.Layout.SimpleSpinnerItem,
            ViewModel.GetFrequencies);
         freqAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
         SpinFreq.Adapter = freqAdapter;
         SpinFreq.ItemSelected += (sender, e) =>
         {
            e.SetBinding(
               () => ViewModel.Frequency,
               () => ViewModel.GetFrequencies[e.Position],
               BindingMode.TwoWay);
         };
      }

      private void BindButtons()
      {
         BtnAddHobby.SetCommand(Events.Events.Click, RecordAndBack);
         BtnCancel.Click += delegate { MoveBackFragment(); };
      }

      private void MoveBackFragment()
      {
         MainViewModel.Parent = -1;
         var transaction = FragmentManager.BeginTransaction();
         transaction.Replace(Resource.Layout.GeneralInfo, new GeneralInfoPage());
         transaction.AddToBackStack(null);
         transaction.Commit();
      }
   }
}