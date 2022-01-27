using System;
using NetflixRouletteApp.Models;
using NetflixRouletteApp.NetworkSvc;
using Xamarin.Forms.Xaml;

namespace NetflixRouletteApp
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class MovieDetailPage
   {
      private readonly Movie _movie;
      private readonly MovieService _movieService = new MovieService();

      public MovieDetailPage(Movie movie)
      {
         _movie = movie ?? throw new ArgumentNullException(nameof(movie));
         InitializeComponent();
      }

      protected override async void OnAppearing()
      {
         BindingContext = await _movieService.GetMovieAsync(_movie.Title)
            .ConfigureAwait(true);
         base.OnAppearing();
      }
   }
}