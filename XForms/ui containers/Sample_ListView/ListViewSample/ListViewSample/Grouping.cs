using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ListViewSample
{
   public class Grouping<TKey, TItem> : ObservableCollection<TItem>
   {
      public Grouping(TKey name, IEnumerable<TItem> items)
      {
         Name = name;
         foreach (var item in items)
         {
            Items.Add(item);
         }
      }

      public TKey Name { get; }
   }
}