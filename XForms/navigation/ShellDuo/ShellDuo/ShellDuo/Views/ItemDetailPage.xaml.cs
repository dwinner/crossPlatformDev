using ShellDuo.ViewModels;

namespace ShellDuo.Views
{
   public partial class ItemDetailPage
   {
      private readonly ItemsViewModel _viewModel;

      public ItemDetailPage()
      {
         InitializeComponent();
         BindingContext = _viewModel = new ItemsViewModel();
         _viewModel.Navigation = Navigation;
      }

      protected override void OnAppearing()
      {
         base.OnAppearing();
         _viewModel.OnAppearing(true);
      }

      protected override void OnDisappearing()
      {
         base.OnDisappearing();
         _viewModel.OnDisappearing();
      }
   }
}