using System.Collections.ObjectModel;
using System.Threading.Tasks;
using PaperBoy.Common;
using PaperBoy.Data;
using PaperBoy.Extensions;
using PaperBoy.Helpers;
using PaperBoy.Interfaces;
using PaperBoy.Models;
using PaperBoy.Models.News;

namespace PaperBoy.ViewModels
{
   public class MainViewModel : ObservableBase
   {
      private UserInformation _currentUser;

      private DeviceOrientations _deviceOrientation;

      private string _extendedPlatformLabel;

      private FavoritesCollection _favorites;

      private bool _isBusy;

      private string _platformLabel;

      private string _searchQuery;
      private ObservableCollection<NewsInformation> _searchResult;

      private ObservableCollection<NewsInformation> _technologyNews;

      private ObservableCollection<NewsInformation> _trendingNews;

      public MainViewModel()
      {
         SearchResult = new ObservableCollection<NewsInformation>();
         TechnologyNews = new ObservableCollection<NewsInformation>();
         TrendingNews = new ObservableCollection<NewsInformation>();

         Favorites = new FavoritesCollection();
         CurrentUser = new UserInformation
         {
            DisplayName = "Ata Mahmoud",
            BioContent = "Egyptian Computer scienece student and Xamarin developer"
         };
      }

      public ObservableCollection<NewsInformation> SearchResult
      {
         get => _searchResult;
         set => SetProperty(ref _searchResult, value);
      }

      public ObservableCollection<NewsInformation> TechnologyNews
      {
         get => _technologyNews;
         set => SetProperty(ref _technologyNews, value);
      }

      public ObservableCollection<NewsInformation> TrendingNews
      {
         get => _trendingNews;
         set => SetProperty(ref _trendingNews, value);
      }

      public UserInformation CurrentUser
      {
         get => _currentUser;
         set => SetProperty(ref _currentUser, value);
      }

      public string SearchQuery
      {
         get => _searchQuery;
         set => SetProperty(ref _searchQuery, value);
      }

      public bool IsBusy
      {
         get => _isBusy;
         set => SetProperty(ref _isBusy, value);
      }

      public string PlatformLabel
      {
         get => _platformLabel;
         set => SetProperty(ref _platformLabel, value);
      }

      public string ExtendedPlatformLabel
      {
         get => _extendedPlatformLabel;
         set => SetProperty(ref _extendedPlatformLabel, value);
      }

      public DeviceOrientations DeviceOrientation
      {
         get => _deviceOrientation;
         set => SetProperty(ref _deviceOrientation, value);
      }

      public FavoritesCollection Favorites
      {
         get => _favorites;
         set => SetProperty(ref _favorites, value);
      }

      public async void RefreshNewsAsync()
      {
         IsBusy = true;

         await RefreshTrendingNewsAsync();
         await RefreshTechnologyNewsAsync();

         IsBusy = false;
      }

      public async Task RefreshFavoritesAsync()
      {
         IsBusy = true;

         Favorites.Clear();

         var favorites = await FavoriteManager.DefaultManager.GetFavoritesAsync();

         foreach (var favorite in favorites)
         {
            Favorites.Add(favorite.AsFavorite("Technology"));
         }

         IsBusy = false;
      }

      public async Task RefreshTrendingNewsAsync()
      {
         TrendingNews.Clear();

         var trendingNews = await NewsHelper.GetTrendingAsync();

         foreach (var item in trendingNews)
         {
            TrendingNews.Add(item);
         }
      }

      public async Task RefreshTechnologyNewsAsync()
      {
         TechnologyNews.Clear();

         var technologyNews = await NewsHelper.GetByCategoryAsync(NewsCategoryType.ScienceAndTechnology);

         foreach (var item in technologyNews)
         {
            TechnologyNews.Add(item);
         }
      }

      public async Task RefreshSearchResultAsync()
      {
         IsBusy = true;

         SearchResult.Clear();

         var query = SearchQuery;
         var searchResults = await NewsHelper.GetSearchAsync(query);

         foreach (var item in searchResults)
         {
            SearchResult.Add(item);
         }

         IsBusy = false;
      }
   }
}