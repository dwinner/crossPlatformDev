using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using InfiniteScrollDemo.Models;
using InfiniteScrollDemo.Views;
using Xamarin.Forms;

namespace InfiniteScrollDemo.ViewModels
{
   public class ItemsViewModel : BaseViewModel
   {
      public const string ScrollToPreviousLastItem = "Scroll_ToPrevious";
      private bool _isRefreshing;
      private int _itemTreshold;

      public ItemsViewModel()
      {
         ItemTreshold = 1;
         Title = "Browse";
         Items = new ObservableCollection<Item>();
         LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand()
            .ConfigureAwait(true));
         ItemTresholdReachedCommand = new Command(async () => await ItemsTresholdReached()
            .ConfigureAwait(true));
         RefreshItemsCommand = new Command(async () =>
         {
            await ExecuteLoadItemsCommand().ConfigureAwait(true);
            IsRefreshing = false;
         });

         MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
         {
            Items.Add(item);
            await DataStore.AddItemAsync(item).ConfigureAwait(true);
         });
      }

      public ObservableCollection<Item> Items { get; }

      public Command LoadItemsCommand { get; }

      public Command ItemTresholdReachedCommand { get; }

      public Command RefreshItemsCommand { get; }

      public bool IsRefreshing
      {
         get => _isRefreshing;
         set => SetProperty(ref _isRefreshing, value);
      }

      public int ItemTreshold
      {
         get => _itemTreshold;
         set => SetProperty(ref _itemTreshold, value);
      }

      private async Task ItemsTresholdReached()
      {
         if (IsBusy)
         {
            return;
         }

         IsBusy = true;

         try
         {
            var items = await DataStore.GetItemsAsync(true, Items.Count)
               .ConfigureAwait(true);
            //var previousLastItem = Items.Last();
            var deferredItems = items as Item[] ?? items.ToArray();
            foreach (var item in deferredItems)
            {
               Items.Add(item);
            }

            Debug.WriteLine($"{deferredItems.Count()} {Items.Count} ");
            if (!deferredItems.Any())
            {
               ItemTreshold = -1;
            }

            //MessagingCenter.Send<object, Item>(this, ScrollToPreviousLastItem, previousLastItem);
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

      private async Task ExecuteLoadItemsCommand()
      {
         if (IsBusy)
         {
            return;
         }

         IsBusy = true;

         try
         {
            ItemTreshold = 1;
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