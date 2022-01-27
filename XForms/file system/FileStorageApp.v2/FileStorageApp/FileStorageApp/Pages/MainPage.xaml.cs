using System.Collections.Generic;
using System.ComponentModel;
using FileStorageApp.UI;
using FileStorageApp.ViewModels;

namespace FileStorageApp.Pages
{
   // Learn more about making custom code visible in the Xamarin.Forms previewer
   // by visiting https://aka.ms/xamarinforms-previewer
   [DesignTimeVisible(false)]
   public partial class MainPage : INavigableXamarinFormsPage
   {
      public MainPage(MainPageViewModel viewModel) : base(viewModel)
      {
         BindingContext = viewModel;
         InitializeComponent();
      }

      public void OnNavigatedTo(IDictionary<string, object> navigationParameters) => this.Show(navigationParameters);
   }
}