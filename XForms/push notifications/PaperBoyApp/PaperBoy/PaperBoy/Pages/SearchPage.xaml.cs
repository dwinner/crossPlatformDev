using PaperBoy.Common.Commands;
using PaperBoy.Models.News;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PaperBoy.Pages
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class SearchPage : ContentPage
   {
      public SearchPage()
      {
         InitializeComponent();
      }

      protected override void OnAppearing()
      {
         LoadDefaultSearchResults();
         base.OnAppearing();
      }

      private async void LoadDefaultSearchResults()
      {
         BindingContext = App.ViewModel;

         if (string.IsNullOrWhiteSpace(App.ViewModel.SearchQuery))
         {
            App.ViewModel.SearchQuery = "Microsoft";
         }

         await App.ViewModel.RefreshSearchResultAsync();
      }

      public void OnItemTapped(object sender, ItemTappedEventArgs e)
      {
         new NavigateToDetailCommand().Execute(e.Item as NewsInformation);
      }
   }
}