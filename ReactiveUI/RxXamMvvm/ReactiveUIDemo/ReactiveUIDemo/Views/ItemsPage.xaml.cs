using System.Collections.ObjectModel;
using Xamarin.Forms.Xaml;

namespace ReactiveUIDemo.Views
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class ItemsPage
   {
      public ItemsPage()
      {
         InitializeComponent();
      }

      public ObservableCollection<string> Items { get; set; }
   }
}