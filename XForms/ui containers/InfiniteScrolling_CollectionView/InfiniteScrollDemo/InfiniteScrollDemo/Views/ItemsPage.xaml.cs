using System;
using System.ComponentModel;
using InfiniteScrollDemo.Models;
using InfiniteScrollDemo.ViewModels;
using Xamarin.Forms;

namespace InfiniteScrollDemo.Views
{
   [DesignTimeVisible(false)]
   public partial class ItemsPage
   {
      private readonly ItemsViewModel _viewModel;

      public ItemsPage()
      {
         InitializeComponent();
         BindingContext = _viewModel = new ItemsViewModel();
      }

      private void AddItem_Clicked(object sender, EventArgs e) =>
         Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));

      protected override void OnAppearing()
      {
         base.OnAppearing();

         if (_viewModel.Items.Count == 0)
         {
            _viewModel.LoadItemsCommand.Execute(null);
         }

         MessagingCenter.Subscribe<object, Item>(this, ItemsViewModel.ScrollToPreviousLastItem,
            (sender, item) => { itemsCollectionView.ScrollTo(item, ScrollToPosition.End); });
      }

      private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
      {
         var currentSelection = e.CurrentSelection;
         if (currentSelection.Count == 0)
         {
            return;
         }

         var item = currentSelection[0] as Item;
         if (item == null)
         {
            return;
         }

         await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)))
            .ConfigureAwait(true);

         // Manually deselect item.
         itemsCollectionView.SelectedItem = null;
      }
   }
}