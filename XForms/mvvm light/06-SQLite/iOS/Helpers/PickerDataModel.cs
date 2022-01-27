using System;
using System.Collections.Generic;
using UIKit;

namespace SQLiteExample.iOS.Helpers
{
   public class PickerDataModel : UIPickerViewModel
   {
      private int _selectedIndex;

      public PickerDataModel() => Items = new List<string>();

      /// <summary>
      ///    The items to show up in the picker
      /// </summary>
      public List<string> Items { get; }

      /// <summary>
      ///    The current selected item
      /// </summary>
      public string SelectedItem => Items[_selectedIndex];

      public event EventHandler<EventArgs> ValueChanged;

      /// <summary>
      ///    Called by the picker to determine how many rows are in a given spinner item
      /// </summary>
      public override nint GetRowsInComponent(UIPickerView picker, nint component) => Items.Count;

      /// <summary>
      ///    called by the picker to get the text for a particular row in a particular
      ///    spinner item
      /// </summary>
      public override string GetTitle(UIPickerView picker, nint row, nint component) => Items[(int) row];

      /// <summary>
      ///    called by the picker to get the number of spinner items
      /// </summary>
      public override nint GetComponentCount(UIPickerView picker) => 1;

      /// <summary>
      ///    called when a row is selected in the spinner
      /// </summary>
      public override void Selected(UIPickerView picker, nint row, nint component)
      {
         _selectedIndex = (int) row;
         ValueChanged?.Invoke(this, new EventArgs());
      }
   }
}