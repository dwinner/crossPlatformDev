using System;
using System.Collections.Generic;
using FileStorageApp.UI;
using FileStorageApp.ViewModels;
using Xamarin.Forms.Xaml;

namespace FileStorageApp.Pages
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class EditFilePage : INavigableXamarinFormsPage
   {
      private readonly EditFilePageViewModel _viewModel;

      public EditFilePage(EditFilePageViewModel viewModel) : base(viewModel)
      {
         _viewModel = viewModel;
         BindingContext = viewModel;

         InitializeComponent();

         Appearing += HandleAppearing;
         Disappearing += HandleDisappearing;
      }

      public void OnNavigatedTo(IDictionary<string, object> navigationParameters) => this.Show(navigationParameters);

      private void HandleDisappearing(object sender, EventArgs e) => _viewModel.OnDisppear();

      private void HandleAppearing(object sender, EventArgs e) => _viewModel.OnAppear();
   }
}