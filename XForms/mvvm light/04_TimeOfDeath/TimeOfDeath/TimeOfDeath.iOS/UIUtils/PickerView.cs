using System;
using System.Collections.Generic;
using UIKit;

namespace TimeOfDeath.iOS
{
   public class PickerChangedEventArgs : EventArgs
   {
      public string SelectedValue { get; set; }

      public int SelectedRow { get; set; }
   }

   public class PickerModel : UIPickerViewModel
   {
      private readonly IList<string> myValues;

      public PickerModel(IList<string> values) => myValues = values;

      public event EventHandler<PickerChangedEventArgs> PickerChanged;

      public override nint GetComponentCount(UIPickerView picker) => 1;

      public override nint GetRowsInComponent(UIPickerView picker, nint component) => myValues.Count;

      public override string GetTitle(UIPickerView picker, nint row, nint component) => myValues[(int) row];

      public override nfloat GetRowHeight(UIPickerView picker, nint component) => 40f;

      public override void Selected(UIPickerView picker, nint row, nint component)
      {
         if (PickerChanged != null)
         {
            PickerChanged(this,
               new PickerChangedEventArgs {SelectedValue = myValues[(int) row], SelectedRow = (int) row});
         }
      }
   }
}