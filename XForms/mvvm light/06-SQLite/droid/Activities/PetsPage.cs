using System.Windows.Input;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Helpers;
using SQLiteExample.ViewModel;

namespace SQLiteExample.Droid.Activities
{
   public partial class PetsPage : Fragment
   {
      private AddPetViewModel ViewModel => App.Locator.AddPetsVm;

      private ICommand RecordAndBack => new RelayCommand(() =>
      {
         ViewModel.BtnCommit.Execute(null);
         MoveBackFragment();
      });

      public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) =>
         inflater.Inflate(Resource.Layout.Pet, container, false);

      public override void OnViewCreated(View view, Bundle savedInstanceState)
      {
         base.OnViewCreated(view, savedInstanceState);
         BindEditText();
         BindSwitch();
         BindButtons();
      }

      private void BindEditText()
      {
         this.SetBinding(
            () => ViewModel.Name,
            () => EditName.Text,
            BindingMode.TwoWay);

         this.SetBinding(
            () => ViewModel.Breed,
            () => EditBreed.Text,
            BindingMode.TwoWay);

         this.SetBinding(
            () => ViewModel.Age,
            () => EditAge.Text,
            BindingMode.TwoWay);
      }

      private void BindSwitch() => SwchDog.CheckedChange += (sender, e) =>
      {
         e.SetBinding(
            () => ViewModel.IsDog,
            () => e.IsChecked,
            BindingMode.TwoWay);
      };

      private void BindButtons()
      {
         BtnCancel.Click += delegate { MoveBackFragment(); };
         BtnAddPet.SetCommand(Events.Events.Click, RecordAndBack);
      }

      private void MoveBackFragment()
      {
         MainViewModel.Parent = -1;
         var trans = FragmentManager.BeginTransaction();
         trans.Replace(Resource.Layout.GeneralInfo, new GeneralInfoPage());
         trans.AddToBackStack(null);
         trans.Commit();
      }
   }
}