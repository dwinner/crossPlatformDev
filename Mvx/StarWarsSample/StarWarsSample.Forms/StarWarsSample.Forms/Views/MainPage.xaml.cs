using MvvmCross.Forms.Presenters.Attributes;

namespace StarWarsSample.Forms.UI.Views
{
   [MvxMasterDetailPagePresentation(
      MasterDetailPosition.Root,
      WrapInNavigationPage = false,
      NoHistory = true)]
   public partial class MainPage
   {
      private bool _firstTime = true;

      public MainPage()
      {
         InitializeComponent();
      }

      protected override void OnAppearing()
      {
         if (_firstTime)
         {
            ViewModel.ShowMenuViewModelCommand.Execute(null);
            ViewModel.ShowPlanetsViewModelCommand.Execute(null);

            _firstTime = false;
         }

         base.OnAppearing();
      }
   }
}