using System.Collections.Generic;
using System.Linq;

namespace ContentViewSample
{
   public partial class MainPage
   {
      private static readonly List<string> _Users = new List<string>
      {
         "Иван Иванов",
         "Олег Кузнецов",
         "Денис Петров",
         "Иван Сидоров",
         "Петр Денисов"
      };

      public MainPage()
      {
         InitializeComponent();
         userList.ItemsSource = _Users;
      }

      private void SearchView_OnSearch(object sender, string text) =>
         userList.ItemsSource = !string.IsNullOrEmpty(text)
            ? _Users.Where(user => user.Contains(text))
            : _Users;
   }
}