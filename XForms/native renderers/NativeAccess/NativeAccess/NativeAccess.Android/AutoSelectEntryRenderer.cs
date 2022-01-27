using Android.Widget;
using NativeAccess;
using NativeAccess.Droid;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(AutoSelectEntry), typeof(AutoSelectEntryRenderer))]

namespace NativeAccess.Droid
{
   public class AutoSelectEntryRenderer : EntryRenderer
   {
      protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
      {
         base.OnElementChanged(e);
         if (e.OldElement == null)
         {
            var nativeEditText = (EditText) Control;
            nativeEditText.SetSelectAllOnFocus(true);
         }
      }
   }
}