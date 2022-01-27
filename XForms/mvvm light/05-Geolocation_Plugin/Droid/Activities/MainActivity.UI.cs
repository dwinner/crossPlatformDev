using Android.Widget;

namespace geolocation_plugin.Droid.Activities
{
   public partial class MainActivity
   {
      private Button _btnListen;
      private TextView _txtLat, _txtLong, _txtAlt, _txtSpeed, _txtHeading;

      public TextView TxtLat => _txtLat ?? (_txtLat = FindViewById<TextView>(Resource.Id.txtLat));
      public TextView TxtLong => _txtLong ?? (_txtLong = FindViewById<TextView>(Resource.Id.txtLong));
      public TextView TxtAlt => _txtAlt ?? (_txtAlt = FindViewById<TextView>(Resource.Id.txtAlt));
      public TextView TxtSpeed => _txtSpeed ?? (_txtSpeed = FindViewById<TextView>(Resource.Id.txtSpeed));
      public TextView TxtHeading => _txtHeading ?? (_txtHeading = FindViewById<TextView>(Resource.Id.txtHeading));

      public Button BtnListen => _btnListen ?? (_btnListen = FindViewById<Button>(Resource.Id.btnListen));
   }
}