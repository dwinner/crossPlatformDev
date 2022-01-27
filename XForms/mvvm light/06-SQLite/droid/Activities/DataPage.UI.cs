using Android.Widget;

namespace SQLiteExample.Droid.Activities
{
   public partial class DataPage
   {
      private ListView _lstView;

      public ListView LstView => _lstView ?? (_lstView = View.FindViewById<ListView>(Resource.Id.listData));
   }
}