using System.Threading.Tasks;
using PaperBoy.Data;
using PaperBoy.Models;
using PaperBoy.Models.News;

namespace PaperBoy.Extensions
{
   public static class ListExtensions
   {
      public static NewsInformation AsArticle(this FavoriteInfromation favorite) =>
         new NewsInformation
         {
            CreatedDate = favorite.ArticleDate,
            Description = favorite.Description,
            ImageUrl = favorite.ImageUrl,
            Title = favorite.Title
         };

      public static async Task<FavoriteInfromation> AsFavorite(this NewsInformation article, string categoryTitle) =>
         new FavoriteInfromation
         {
            ArticleDate = article.CreatedDate,
            Description = article.Description,
            ImageUrl = article.ImageUrl,
            Title = article.Title,
            CategoryTitle = categoryTitle
         };

      public static FavoriteInfromation AsFavorite(this Favorite favorite, string categoryTitle) =>
         new FavoriteInfromation
         {
            ArticleDate = favorite.ArticleDate,
            Description = favorite.Description,
            ImageUrl = favorite.ImageUrl,
            Title = favorite.Title,
            CategoryTitle = categoryTitle
         };
   }
}