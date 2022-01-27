using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace ContextMenuMvvmApp.ViewModels
{
   public class PhoneListViewModel : ViewModelBase
   {
      private ICommand _moveToBottom;
      private ICommand _moveToTop;
      private ICommand _remove;

      public PhoneListViewModel() =>
         Phones = new ObservableCollection<PhoneViewModel>
         {
            new PhoneViewModel {Title = "HP Elite z3", Price = 55000, Company = "HP", ListViewModel = this},
            new PhoneViewModel {Title = "Honor 8", Price = 28000, Company = "Huawei", ListViewModel = this},
            new PhoneViewModel {Title = "iPhone SE", Price = 30000, Company = "Apple", ListViewModel = this},
            new PhoneViewModel {Title = "Galaxy Note 7", Price = 60000, Company = "Samsung", ListViewModel = this},
            new PhoneViewModel {Title = "Lumia 950 XL", Price = 36000, Company = "Microsoft", ListViewModel = this}
         };

      public ObservableCollection<PhoneViewModel> Phones { get; set; }

      public ICommand MoveToTop =>
         _moveToTop ?? (_moveToTop = new Command(phoneObj =>
         {
            if (phoneObj is PhoneViewModel phone)
            {
               var oldIndex = Phones.IndexOf(phone);
               if (oldIndex > 0)
               {
                  Phones.Move(oldIndex, oldIndex - 1);
               }
            }
         }));

      public ICommand MoveToBottom =>
         _moveToBottom ?? (_moveToBottom = new Command(phoneObj =>
         {
            if (phoneObj is PhoneViewModel phone)
            {
               var oldIndex = Phones.IndexOf(phone);
               if (oldIndex < Phones.Count - 1)
               {
                  Phones.Move(oldIndex, oldIndex + 1);
               }
            }
         }));

      public ICommand Remove =>
         _remove ?? (_remove = new Command(phoneObj =>
         {
            if (phoneObj is PhoneViewModel phone)
            {
               Phones.Remove(phone);
            }
         }));
   }
}