using PaperBoy.Common.Commands;
using PaperBoy.Models.News;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PaperBoy.Pages
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class TechnologyPage : ContentPage
   {
      public TechnologyPage()
      {
         InitializeComponent();
      }

      protected override void OnAppearing()
      {
         BindingContext = App.ViewModel;
         base.OnAppearing();
      }

      public void OnItemTapped(object sender, ItemTappedEventArgs e)
      {
         new NavigateToDetailCommand().Execute(e.Item as NewsInformation);
      }
   }
}