using Android.Widget;

namespace Messages.Droid.Activities
{
   public partial class SecondActivity
   {
      private TextView _txtData1;
      private TextView _txtData2;
      private TextView _txtData3;
      private TextView _txtData4;

      private TextView TxtData1 => _txtData1 ?? (_txtData1 = FindViewById<TextView>(Resource.Id.txtData1));

      private TextView TxtData2 => _txtData2 ?? (_txtData2 = FindViewById<TextView>(Resource.Id.txtData2));

      private TextView TxtData3 => _txtData3 ?? (_txtData3 = FindViewById<TextView>(Resource.Id.txtData3));

      private TextView TxtData4 => _txtData4 ?? (_txtData4 = FindViewById<TextView>(Resource.Id.txtData4));
   }
}