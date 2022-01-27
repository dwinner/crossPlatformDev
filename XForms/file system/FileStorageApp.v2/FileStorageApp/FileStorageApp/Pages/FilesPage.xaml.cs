using System;
using System.Collections.Generic;
using FileStorageApp.UI;
using FileStorageApp.ViewModels;
using Xamarin.Forms.Xaml;

namespace FileStorageApp.Pages
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class FilesPage : INavigableXamarinFormsPage
   {
      private readonly FilesPageViewModel _viewModel;

      public FilesPage(FilesPageViewModel viewModel) : base(viewModel)
      {
         _viewModel = viewModel;
         BindingContext = viewModel;

         InitializeComponent();

         Appearing += HandleAppearing;
         Disappearing += HandleDisappearing;
      }

      public void OnNavigatedTo(IDictionary<string, object> navigationParameters) => this.Show(navigationParameters);

      private void HandleDisappearing(object sender, EventArgs e) => _viewModel.OnDisappear();

      private void HandleAppearing(object sender, EventArgs e) => _viewModel.OnAppear();
   }
}