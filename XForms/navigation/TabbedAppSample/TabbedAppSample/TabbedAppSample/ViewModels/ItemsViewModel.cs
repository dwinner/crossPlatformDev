using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using TabbedAppSample.Models;
using TabbedAppSample.Views;
using Xamarin.Forms;

namespace TabbedAppSample.ViewModels
{
   public class ItemsViewModel : BaseViewModel
   {
      public ItemsViewModel()
      {
         Title = "Browse";
         Items = new ObservableCollection<Item>();
         LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommandAsync());

         MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
         {
            var newItem = item;
            Items.Add(newItem);
            await DataStore.AddItemAsync(newItem).ConfigureAwait(true);
         });
      }

      public ObservableCollection<Item> Items { get; }
      public Command LoadItemsCommand { get; }

      private async Task ExecuteLoadItemsCommandAsync()
      {
         if (IsBusy)
         {
            return;
         }

         IsBusy = true;

         try
         {
            Items.Clear();
            var items = await DataStore.GetItemsAsync(true).ConfigureAwait(true);
            foreach (var item in items)
            {
               Items.Add(item);
            }
         }
         catch (Exception ex)
         {
            Debug.WriteLine(ex);
         }
         finally
         {
            IsBusy = false;
         }
      }
   }
}