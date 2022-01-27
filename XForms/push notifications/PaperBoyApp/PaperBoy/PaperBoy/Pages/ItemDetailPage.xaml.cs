using PaperBoy.Models.News;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PaperBoy.Pages
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class ItemDetailPage : ContentPage
   {
      public ItemDetailPage()
      {
         InitializeComponent();
      }

      public ItemDetailPage(NewsInformation article)
      {
         InitializeComponent();
         CurrentArticle = article;
      }

      public NewsInformation CurrentArticle { get; set; }

      protected override void OnAppearing()
      {
         BindingContext = CurrentArticle;
         base.OnAppearing();
      }
   }
}