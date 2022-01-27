using NativeCustomDialogs.ViewModels;
using Xamarin.Forms;

namespace NativeCustomDialogs
{
   public partial class MainPage : ContentPage
   {
      public MainPage()
      {
         InitializeComponent();
      }

      public MainViewModel ViewModel => BindingContext as MainViewModel;

      protected override void OnAppearing()
      {
         //Initialize the viewmodel
         ViewModel.Initialize();
         base.OnAppearing();
      }

      protected override void OnDisappearing()
      {
         ViewModel.Stop();
         base.OnDisappearing();
      }
   }
}