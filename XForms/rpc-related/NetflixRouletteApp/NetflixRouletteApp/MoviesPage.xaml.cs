using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using NetflixRouletteApp.Models;
using NetflixRouletteApp.NetworkSvc;
using Xamarin.Forms;

namespace NetflixRouletteApp
{
   // Learn more about making custom code visible in the Xamarin.Forms previewer
   // by visiting https://aka.ms/xamarinforms-previewer
   [DesignTimeVisible(false)]
   public partial class MoviesPage
   {
      private readonly MovieService _service = new MovieService();

      private readonly BindableProperty IsSearchingProperty =
         BindableProperty.Create(nameof(IsSearching), typeof(bool), typeof(MoviesPage), false);

      public MoviesPage()
      {
         BindingContext = this;
         InitializeComponent();
      }

      public bool IsSearching
      {
         get => (bool) GetValue(IsSearchingProperty);
         set => SetValue(IsSearchingProperty, value);
      }

      private async void OnTextChanged(object sender, TextChangedEventArgs e)
      {
         if (e.NewTextValue == null || e.NewTextValue.Length < MovieService.MinSearchLength)
         {
            return;
         }

         await FindMoviesAsync(e.NewTextValue).ConfigureAwait(false);
      }

      private async Task FindMoviesAsync(string actor)
      {
         try
         {
            IsSearching = true;
            var movies = (await _service.FindMoviesByActorAsync(actor).ConfigureAwait(true)).ToArray();
            moviesListView.ItemsSource = movies;
            moviesListView.IsVisible = movies.Length > 0;
            notFound.IsVisible = !moviesListView.IsVisible;
         }
         catch
         {
            await DisplayAlert("Error", "Could not retrieve the list of movies.", "OK")
               .ConfigureAwait(true);
         }
         finally
         {
            IsSearching = false;
         }
      }

      private async void OnMovieSelected(object sender, SelectedItemChangedEventArgs e)
      {
         if (e.SelectedItem == null)
         {
            return;
         }

         var movie = e.SelectedItem as Movie;
         moviesListView.SelectedItem = null;

         await Navigation.PushAsync(new MovieDetailPage(movie))
            .ConfigureAwait(true);
      }
   }
}