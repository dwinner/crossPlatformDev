using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace NavigationSampleApp
{
   public partial class MainPage
   {
      public MainPage()
      {
         InitializeComponent();

         Phones = new ObservableCollection<Phone>
         {
            new Phone {Name = "iPhone 7", Company = "Apple", Price = 52000},
            new Phone {Name = "Galaxy S8", Company = "Samsung", Price = 50000},
            new Phone {Name = "LG G6", Company = "LG", Price = 45000},
            new Phone {Name = "Huawei P10", Company = "Huawei", Price = 35000}
         };

         phonesList.ItemsSource = Phones;
         phonesList.BindingContext = Phones;
      }

      public ObservableCollection<Phone> Phones { get; set; }

      protected internal void AddPhone(Phone phone) => Phones.Add(phone);

      private void OnAdd(object sender, EventArgs e) =>
         Navigation.PushAsync(new PhonePage(null)).ConfigureAwait(true);

      private async void OnSelectPhone(object sender, SelectedItemChangedEventArgs e)
      {
         if (e.SelectedItem is Phone selectedPhone)
         {
            phonesList.SelectedItem = null;
            await Navigation.PushAsync(new PhonePage(selectedPhone)).ConfigureAwait(true);
         }
      }
   }
}