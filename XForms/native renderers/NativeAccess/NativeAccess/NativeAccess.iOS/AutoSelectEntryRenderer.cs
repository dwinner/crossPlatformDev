using NativeAccess;
using NativeAccess.iOS;
using ObjCRuntime;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(AutoSelectEntry),
   typeof(AutoSelectEntryRenderer))]

namespace NativeAccess.iOS
{
   public class AutoSelectEntryRenderer : EntryRenderer
   {
      protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
      {
         base.OnElementChanged(e);
         var nativeTextField = Control;
         nativeTextField.EditingDidBegin += (sender, eIos) =>
         {
            nativeTextField.PerformSelector(new Selector("selectAll"),
               null, 0.0f);
         };
      }
   }
}