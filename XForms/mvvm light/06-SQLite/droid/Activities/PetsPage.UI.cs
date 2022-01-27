using Android.Widget;

namespace SQLiteExample.Droid.Activities
{
   public partial class PetsPage
   {
      private Button _btnAddPet, _btnCancel;
      private EditText _editName, _editBreed, _editAge;
      private Switch _swchDog;

      public EditText EditName => _editName ?? (_editName = View.FindViewById<EditText>(Resource.Id.editName));
      public EditText EditBreed => _editBreed ?? (_editBreed = View.FindViewById<EditText>(Resource.Id.editBreed));
      public EditText EditAge => _editAge ?? (_editAge = View.FindViewById<EditText>(Resource.Id.editAge));

      public Switch SwchDog => _swchDog ?? (_swchDog = View.FindViewById<Switch>(Resource.Id.swchIsDog));

      public Button BtnAddPet => _btnAddPet ?? (_btnAddPet = View.FindViewById<Button>(Resource.Id.btnCommit));
      public Button BtnCancel => _btnCancel ?? (_btnCancel = View.FindViewById<Button>(Resource.Id.btnCancel));
   }
}