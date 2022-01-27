using System.ComponentModel;
using ContextMenuMvvmApp.Models;

namespace ContextMenuMvvmApp.ViewModels
{
   public class PhoneViewModel : ViewModelBase
   {
      public PhoneViewModel() => Phone = new Phone();

      public Phone Phone { get; set; }

      public PhoneListViewModel ListViewModel { get; set; }

      public string Title
      {
         get => Phone.Title;
         set
         {
            if (Phone.Title != value)
            {
               Phone.Title = value;
               OnPropertyChanged();
            }
         }
      }

      public string Company
      {
         get => Phone.Company;
         set
         {
            if (Phone.Company != value)
            {
               Phone.Company = value;
               OnPropertyChanged();
            }
         }
      }

      public int Price
      {
         get => Phone.Price;
         set
         {
            if (Phone.Price != value)
            {
               Phone.Price = value;
               OnPropertyChanged();
            }
         }
      }

      public override event PropertyChangedEventHandler PropertyChanged;
   }
}