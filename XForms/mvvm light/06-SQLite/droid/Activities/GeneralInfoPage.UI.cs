using Android.Widget;

namespace SQLiteExample.Droid.Activities
{
   public partial class GeneralInfoPage
   {
      private Button _btnAddPet, _btnAddHobby, _btnCommit;
      private EditText _editName, _editAddress1, _editAddress2, _editAddress3, _editPostcode, _editEmail, _editMobile;
      private TextView _txtId, _txtTotal;

      public TextView TxtId => _txtId ?? (_txtId = View.FindViewById<TextView>(Resource.Id.txtID));
      public TextView TxtTotal => _txtTotal ?? (_txtTotal = View.FindViewById<TextView>(Resource.Id.txtTotal));

      public EditText EditName => _editName ?? (_editName = View.FindViewById<EditText>(Resource.Id.editName));

      public EditText EditAddress1 =>
         _editAddress1 ?? (_editAddress1 = View.FindViewById<EditText>(Resource.Id.editAddress1));

      public EditText EditAddress2 =>
         _editAddress2 ?? (_editAddress2 = View.FindViewById<EditText>(Resource.Id.editAddress2));

      public EditText EditAddress3 =>
         _editAddress3 ?? (_editAddress3 = View.FindViewById<EditText>(Resource.Id.editAddress3));

      public EditText EditPostcode =>
         _editPostcode ?? (_editPostcode = View.FindViewById<EditText>(Resource.Id.editPostcode));

      public EditText EditEmail => _editEmail ?? (_editEmail = View.FindViewById<EditText>(Resource.Id.editEmail));
      public EditText EditMobile => _editMobile ?? (_editMobile = View.FindViewById<EditText>(Resource.Id.editMobile));

      public Button BtnAddPet => _btnAddPet ?? (_btnAddPet = View.FindViewById<Button>(Resource.Id.btnAddPet));
      public Button BtnAddHobby => _btnAddHobby ?? (_btnAddHobby = View.FindViewById<Button>(Resource.Id.btnAddHobby));
      public Button BtnCommit => _btnCommit ?? (_btnCommit = View.FindViewById<Button>(Resource.Id.btnCommit));
   }
}