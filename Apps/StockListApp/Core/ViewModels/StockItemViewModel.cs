using System.Collections.Generic;
using System.Threading.Tasks;
using StockList.Core.Models;
using StockList.Core.UI;
using StockList.Core.WebServices;

namespace StockList.Core.ViewModels
{
   public class StockItemViewModel : ViewModelBase
   {
      private string _category;
      private int _id;
      private string _name;
      private decimal _price;

      public StockItemViewModel(INavigationService navigation)
         : base(navigation)
      {
      }

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

      public void Apply(StockItemContract contract)
      {
         Id = contract.Id;
         Name = contract.Name;
         Category = contract.Category;
         Price = contract.Price;
      }

      protected override Task LoadAsync(IDictionary<string, object> parameters) => Task.FromResult(true);
   }
}