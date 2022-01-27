using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace ListViewSample
{
   public partial class MainPage
   {
      public MainPage()
      {
         InitializeComponent();

         // Начальные данные
         var phones = new List<Phone>
         {
            new Phone {Title = "Galaxy S8", Company = "Samsung", Price = 60000, ImagePath = "galaxys6.png"},
            new Phone {Title = "Galaxy S7 Edge", Company = "Samsung", Price = 50000, ImagePath = "elite.png"},
            new Phone {Title = "Huawei P10", Company = "Huawei", Price = 10000, ImagePath = "huawei-p10.png"},
            new Phone {Title = "Huawe Mate 8", Company = "Huawei", Price = 29000, ImagePath = "lg-g6.png"},
            new Phone {Title = "Mi6", Company = "Xiaomi", Price = 55000, ImagePath = "iphone_7.png"},
            new Phone {Title = "iPhone 7", Company = "Apple", Price = 38000},
            new Phone {Title = "iPhone 6S", Company = "Apple", Price = 50000}
         };

         // Получаем группы
         var groups = phones.GroupBy(phone => phone.Company)
            .Select(grouping => new Grouping<string, Phone>(grouping.Key, grouping));

         // Передаем группы в PhoneGroups
         PhoneGroups = new ObservableCollection<Grouping<string, Phone>>(groups);

         BindingContext = this;
      }

      /// <summary>
      ///    Список групп, к которым идёт привязка
      /// </summary>
      public ObservableCollection<Grouping<string, Phone>> PhoneGroups { get; }

      private void PhoneList_OnItemSelected(object sender, SelectedItemChangedEventArgs e) =>
         SelectItem(e.SelectedItem);

      private async void PhoneList_OnItemTapped(object sender, ItemTappedEventArgs e)
      {
         if (e.Item is Phone selectedPhone)
         {
            await DisplayAlert("Selected model",
                  $"{selectedPhone.Company} - {selectedPhone.Title}", "OK")
               .ConfigureAwait(true);
         }
      }

      private void SelectItem(object selectedItem)
      {
         if (selectedItem != null)
         {
            selectedLabel.Text = selectedItem.ToString();
            phoneList.SelectedItem = null;
         }
      }

      private void OnAdd(object sender, EventArgs e)
      {
      }

      private void OnRemove(object sender, EventArgs e)
      {
      }
   }
}