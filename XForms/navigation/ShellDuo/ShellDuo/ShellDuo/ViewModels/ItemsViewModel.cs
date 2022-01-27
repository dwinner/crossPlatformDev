using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using ShellDuo.Models;
using ShellDuo.Views;
using Xamarin.Forms;

namespace ShellDuo.ViewModels
{
   [QueryProperty(nameof(ItemId), nameof(ItemId))]
   public class ItemsViewModel : BaseViewModel
   {
      private Item _selectedItem;
      private string _itemId;

      public ItemsViewModel()
      {
         Title = "Browse";
         Items = new ObservableCollection<Item>();
         LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand().ConfigureAwait(true));
         AddItemCommand = new Command(OnAddItem);
      }

      public ObservableCollection<Item> Items { get; }
      public Command LoadItemsCommand { get; }
      public Command AddItemCommand { get; }

      public Item SelectedItem
      {
         get => _selectedItem;
         set
         {
            SetProperty(ref _selectedItem, value);
            OnItemSelected(value);
         }
      }

      public string ItemId
      {
         get => _itemId;
         set
         {
            _itemId = value;
            LoadItemId(value);
         }
      }

      private async Task ExecuteLoadItemsCommand()
      {
         IsBusy = true;

         try
         {
            Items.Clear();
            var items = await DataStore.GetItemsAsync(true)
               .ConfigureAwait(true);
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

      public new void OnAppearing(bool isDetail = false)
      {
         base.OnAppearing(isDetail);
         if (!IsDetail)
         {
            SelectedItem = null;
         }
      }

      private async void OnAddItem(object obj) =>
         await Shell.Current.GoToAsync(nameof(NewItemPage)).ConfigureAwait(true);

      private async void OnItemSelected(Item item)
      {
         if (item == null)
         {
            return;
         }

         if (!DeviceIsSpanned && !DeviceIsBigScreen && string.IsNullOrEmpty(ItemId))
         {
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemId)}={item.Id}")
               .ConfigureAwait(true);
         }
      }

      private async void LoadItemId(string itemId)
      {
         IsBusy = true;

         try
         {
            var item = await DataStore.GetItemAsync(itemId).ConfigureAwait(true);
            SelectedItem = item;
         }
         catch (Exception)
         {
            Debug.WriteLine("Failed to Load Item");
         }
         finally
         {
            IsBusy = false;
         }
      }

      protected override void UpdateLayouts() =>
         UpdateLayouts(SelectedItem != null, $"{nameof(ItemDetailPage)}?{nameof(ItemId)}={SelectedItem?.Id}");
   }
}