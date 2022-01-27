using Android.Widget;

namespace TimeOfDeath.Droid
{
   public partial class TODFragment
   {
      private EditText editDate, editTime, tempBody, tempSurround, weightBody;
      private CheckBox weightBodyKG;

      public EditText EditDate => editDate ?? (editDate = view.FindViewById<EditText>(Resource.Id.editDate));
      public EditText EditTime => editTime ?? (editTime = view.FindViewById<EditText>(Resource.Id.editTime));
      public EditText EditTempBody => tempBody ?? (tempBody = view.FindViewById<EditText>(Resource.Id.tempBody));

      public EditText EditTempSurround =>
         tempSurround ?? (tempSurround = view.FindViewById<EditText>(Resource.Id.tempSurround));

      public EditText EditWeightBody =>
         weightBody ?? (weightBody = view.FindViewById<EditText>(Resource.Id.weightBody));

      public CheckBox ChkBoxWeightKg =>
         weightBodyKG ?? (weightBodyKG = view.FindViewById<CheckBox>(Resource.Id.weightBodyKG));

      private void KillViews()
      {
         editDate = editTime = tempBody = tempSurround = weightBody = null;
         weightBodyKG = null;
      }
   }
}