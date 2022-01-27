using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using StockList.Core.Enums;
using StockList.Core.Models;
using StockList.Core.UI;
using StockList.Core.WebServices;

namespace StockList.Core.ViewModels
{
   public class StockListPageViewModel : ViewModelBase
   {
      private readonly IStocklistWebServiceController _httpController;
      private readonly Func<StockItemViewModel> _stockItemFactory;
      private bool _inProgress;
      private StockItemViewModel _selected;

      public StockListPageViewModel(INavigationService navigation, IStocklistWebServiceController wsController,
         Func<StockItemViewModel> stockItemFactory)
         : base(navigation)
      {
         _stockItemFactory = stockItemFactory;
         _httpController = wsController;
         StockItems = new ObservableCollection<StockItemViewModel>();
      }

      public ObservableCollection<StockItemViewModel> StockItems { get; }

      public StockItemViewModel Selected
      {
         get => _selected;
         set
         {
            if (value.Equals(_selected))
            {
               return;
            }

            Navigation.NavigateAsync(PageNames.StockItemDetailsPage,
               new Dictionary<string, object> {{"id", value.Id}}).ConfigureAwait(false);

            _selected = value;
            OnPropertyChanged();
         }
      }

      public bool InProgress
      {
         get => _inProgress;
         set
         {
            if (value.Equals(_inProgress))
            {
               return;
            }

            _inProgress = value;
            OnPropertyChanged();
         }
      }

      protected override async Task LoadAsync(IDictionary<string, object> parameters)
      {
         try
         {
            InProgress = true;

            // reset the list everytime we load the page
            StockItems.Clear();

            var stockItems = await _httpController.GetAllStockItems()/*.ConfigureAwait(false)*/;

            // for all contracts build stock item view model and add to the observable collection
            foreach (var vm in stockItems.Select(contract =>
            {
               var model = _stockItemFactory();
               model.Apply(contract);
               return model;
            }))
            {
               StockItems.Add(vm);
            }
         }
         catch (Exception ex)
         {
            Debug.WriteLine(ex);
         }
         finally
         {
            InProgress = false;
         }
      }
   }
}