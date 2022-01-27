using ShellDuo.ViewModels;

namespace ShellDuo.Views
{
   public partial class ItemsPage
   {
      private readonly ItemsViewModel _viewModel;

      public ItemsPage()
      {
         InitializeComponent();
         BindingContext = _viewModel = new ItemsViewModel();
      }

      protected override void OnAppearing()
      {
         base.OnAppearing();
         _viewModel.OnAppearing();
      }

      protected override void OnDisappearing()
      {
         base.OnDisappearing();
         _viewModel.OnDisappearing();
      }
   }
}