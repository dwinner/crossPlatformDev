using Android.Widget;

namespace Messages.Droid.Activities
{
   public partial class MainActivity
   {
      private Button _btnClickMe;

      private Button BtnClickMe => _btnClickMe ?? (_btnClickMe = FindViewById<Button>(Resource.Id.myButton));
   }
}