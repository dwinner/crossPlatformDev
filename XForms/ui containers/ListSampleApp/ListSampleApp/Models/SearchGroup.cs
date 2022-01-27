using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ListSampleApp.Models
{
   public class SearchGroup : ObservableCollection<Search>
   {
      public SearchGroup(string title, IEnumerable<Search> searches)
         : base(searches)
         => Title = title;

      public string Title { get; set; }
   }
}