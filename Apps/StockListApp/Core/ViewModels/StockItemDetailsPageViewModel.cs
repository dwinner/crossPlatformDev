using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using StockList.Core.Models;
using StockList.Core.UI;
using StockList.Core.WebServices;

namespace StockList.Core.ViewModels
{
   public class StockItemDetailsPageViewModel : ViewModelBase
   {
      private string _category;
      private readonly IStocklistWebServiceController _httpController;
      private int _id;
      private bool _inProgress;
      private string _name;
      private decimal _price;

      public StockItemDetailsPageViewModel(INavigationService navigation, IStocklistWebServiceController controller)
         : base(navigation) =>
         _httpController = controller;

      public int Id
      {
         get => _id;
         set
         {
            if (value.Equals(_id))
            {
               return;
            }

            _id = value;
            OnPropertyChanged();
         }
      }

      public string Name
      {
         get => _name;
         set
         {
            if (value.Equals(_name))
            {
               return;
            }

            _name = value;
            OnPropertyChanged();
         }
      }

      public string Category
      {
         get => _category;
         set
         {
            if (value.Equals(_category))
            {
               return;
            }

            _category = value;
            OnPropertyChanged();
         }
      }

      public decimal Price
      {
         get => _price;
         set
         {
            if (value.Equals(_price))
            {
               return;
            }

            _price = value;
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
         const string idKey = "id";

         try
         {
            InProgress = true;
            if (parameters.ContainsKey(idKey))
            {
               Id = (int) parameters[idKey];
            }

            var contract = await _httpController.GetStockItem(Id);

            if (contract != null)
            {
               Name = contract.Name;
               Category = contract.Category;
               Price = contract.Price;
            }
         }
         finally
         {
            InProgress = false;
         }
      }
   }
}