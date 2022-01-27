using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using PaperBoy.Common;
using PaperBoy.Data;

namespace PaperBoy.Models
{
   public class FavoriteInfromation : ObservableBase
   {
      private DateTime _articleDate;

      private string _categoryTitle;

      private string _description;
      private int _id;

      private string _imageUrl;

      private string _title;

      public int Id
      {
         get => _id;
         set => SetProperty(ref _id, value);
      }

      public string Title
      {
         get => _title;
         set => SetProperty(ref _title, value);
      }

      public string CategoryTitle
      {
         get => _categoryTitle;
         set => SetProperty(ref _categoryTitle, value);
      }

      public string Description
      {
         get => _description;
         set => SetProperty(ref _description, value);
      }

      public string ImageUrl
      {
         get => _imageUrl;
         set => SetProperty(ref _imageUrl, value);
      }

      public DateTime ArticleDate
      {
         get => _articleDate;
         set => SetProperty(ref _articleDate, value);
      }
   }

   public class FavoritesCollection : ObservableCollection<FavoriteInfromation>
   {
      public async Task AddAsync(FavoriteInfromation articel)
      {
         var favorite = new Favorite
         {
            ArticleDate = articel.ArticleDate,
            Description = articel.Description,
            ImageUrl = articel.ImageUrl,
            Title = articel.Title
         };

         await FavoriteManager.DefaultManager.SaveFavoriteAsync(favorite);

         Add(articel);
      }
   }
}