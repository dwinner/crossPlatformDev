using System;
using System.Collections.Generic;
using System.ComponentModel;
using ListSampleApp.Models;
using ListSampleApp.Services;
using Xamarin.Forms;

namespace ListSampleApp
{
   // Learn more about making custom code visible in the Xamarin.Forms previewer
   // by visiting https://aka.ms/xamarinforms-previewer
   [DesignTimeVisible(false)]
   public partial class ListPage
   {
      private List<SearchGroup> _searchGroups;
      private readonly SearchService _searchService;

      public ListPage()
      {
         InitializeComponent();
         _searchService = new SearchService();
         PopulateListView(_searchService.GetRecentSearches());
      }

      private void PopulateListView(IEnumerable<Search> searches)
      {
         _searchGroups = new List<SearchGroup> {new SearchGroup("Recent Searches", searches)};
         listView.ItemsSource = _searchGroups;
      }

      private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e) =>
         PopulateListView(_searchService.GetRecentSearches(e.NewTextValue));

      private void SearchBar_OnSearchButtonPressed(object sender, EventArgs e) =>
         PopulateListView(_searchService.GetRecentSearches(searchBar.Text));

      private void ListView_OnRefreshing(object sender, EventArgs e)
      {
         PopulateListView(_searchService.GetRecentSearches(searchBar.Text));
         listView.EndRefresh();
      }

      private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
      {
         if (e.SelectedItem is Search search)
         {
            DisplayAlert("Selected", search.Location, "Ok");
         }
      }

      private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
      {
         if (e.Item is Search search)
         {
            DisplayAlert("Selected", search.Location, "Ok");
         }
      }

      private void OnDeleted(object sender, EventArgs e)
      {
         if (!((sender as MenuItem)?.CommandParameter is Search search))
         {
            return;
         }

         _searchGroups[0].Remove(search);
         _searchService.DeleteSearch(search.Id);
      }
   }
}