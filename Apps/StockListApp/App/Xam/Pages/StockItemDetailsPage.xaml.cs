using System;
using System.Collections.Generic;
using StockList.Core.UI;
using StockList.Core.ViewModels;
using StockListApp.Xam.UI;
using Xamarin.Forms.Xaml;

namespace StockListApp.Xam.Pages
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class StockItemDetailsPage : INavigableXamarinFormsPage
   {
      public StockItemDetailsPage() => InitializeComponent();

      public StockItemDetailsPage(Func<StockItemDetailsPageViewModel> modelFactory)
         : this() => BindingContext = modelFactory();

      public void OnNavigatedTo(IDictionary<string, object> navigationParameters) => this.Show(navigationParameters);
   }
}