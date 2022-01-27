using Android.Widget;

namespace SQLiteExample.Droid.Activities
{
   public partial class HobbiesPage
   {
      private Button _btnAddHobby, _btnCancel;
      private EditText _editName, _editDesc, _editCost;
      private Spinner _spinFrequency;

      public EditText EditName => _editName ?? (_editName = View.FindViewById<EditText>(Resource.Id.editName));
      public EditText EditDesc => _editDesc ?? (_editDesc = View.FindViewById<EditText>(Resource.Id.editDescription));
      public EditText EditCost => _editCost ?? (_editCost = View.FindViewById<EditText>(Resource.Id.editCost));

      public Spinner SpinFreq =>
         _spinFrequency ?? (_spinFrequency = View.FindViewById<Spinner>(Resource.Id.spinFrequency));

      public Button BtnAddHobby => _btnAddHobby ?? (_btnAddHobby = View.FindViewById<Button>(Resource.Id.btnAddHobby));
      public Button BtnCancel => _btnCancel ?? (_btnCancel = View.FindViewById<Button>(Resource.Id.btnCancel));
   }
}