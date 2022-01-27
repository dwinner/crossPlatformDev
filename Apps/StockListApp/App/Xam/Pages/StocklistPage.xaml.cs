using System.Collections.Generic;
using StockList.Core.UI;
using StockList.Core.ViewModels;
using StockListApp.Xam.UI;
using Xamarin.Forms.Xaml;

namespace StockListApp.Xam.Pages
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class StocklistPage : INavigableXamarinFormsPage
   {
      public StocklistPage() => InitializeComponent();

      public StocklistPage(StockListPageViewModel model)
         : this() => BindingContext = model;

      public void OnNavigatedTo(IDictionary<string, object> navigationParameters) => this.Show(navigationParameters);
   }
}